using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Tracktracer
{
    public partial class Historyjka : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;
        private int projekt_id;
        private string nazwa;
        private int historyjka_id;
        private string status;
        private string tresc1;
        private string uwagi1;
        private string udzialowcy1;
        private DateTime data;
        private int akt_rewizja;
        private List<string> powroty;
        private List<int> powroty_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                user_id = (int)Session["user_id"];
                conn = (SqlConnection)Session["connection"];
                projekt_id = (int)Session["projekt_id"];
                nazwa = (string)Session["nazwa"];
                historyjka_id = (int)Session["historyjka_id"];
            }
            catch
            {
                Server.Transfer("Index.aspx");
            }

            powroty = (List<string>)Session["powroty"];
            powroty_id = (List<int>)Session["powroty_id"];

            if (powroty == null)
            {
                powroty = new List<string>();
                powroty_id = new List<int>();
            }
            if (!IsPostBack)
            {
                powroty.Add("Historyjka.aspx");
                powroty_id.Add(Convert.ToInt32(historyjka_id));
            }
            Session["powroty"] = powroty;
            Session["powroty_id"] = powroty_id;

            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "SELECT hu.nazwa, hu.status, hu.nr_rewizji, hu.nr_wydania, hu.nr_iteracji, wer.tresc, wer.uwagi, wer.udzialowcy, wer.data FROM Historyjki_uzytkownikow hu, Wersje_historyjek wer WHERE hu.id='" + historyjka_id + "' AND wer.Historyjka_uzytkownika_id = hu.id ORDER BY wer.data DESC;";

            SqlDataReader reader;
            reader = zapytanie.ExecuteReader();
            try
            {
                reader.Read();
                historyjka_Label.Text = reader.GetString(0);                
                status = reader.GetString(1);                                
                akt_rewizja = reader.GetInt32(2);

                if (!reader.IsDBNull(3) && !reader.IsDBNull(4))
                {
                    wydanie_Label.Text = "Wydanie: " + reader.GetInt32(3);
                    iteracja_Label.Text = "Iteracja: " + reader.GetInt32(4);
                }
                else
                {
                    brak_przypisania_Label.Visible = true;
                    wydanie_Label.Visible = false;
                    iteracja_Label.Visible = false;
                    przypisanie_Button.Visible = false;
                }

                tresc1 = reader.GetString(5);
                uwagi1 = reader.GetString(6);
                udzialowcy1 = reader.GetString(7);
                
                if (!IsPostBack)
                {
                    status_DropDownList.SelectedValue = status;
                    tresc_TextBox.Text = tresc1;
                    uwagi_TextBox.Text = uwagi1;
                    udzialowcy_TextBox.Text = udzialowcy1;
                }

                reader.Close();
            }
            catch
            {
                reader.Dispose();
            }

            projekt_Label.Text = nazwa;
            if (!IsPostBack) zaladuj_wersje_DropDownList();
            opisR_Label.Visible = false;
            uwagiR_Label.Visible = false;
            udzialowcyR_Label.Visible = false;            
        }

        // Załadowanie rozwijalnej listy z dostępnymi wersjami historyjki użytkownika
        protected void zaladuj_wersje_DropDownList()
        {
            wersje_DropDownList.Items.Clear();
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "SELECT wer.id, wer.data, u.imie, u.nazwisko, u.login FROM Wersje_historyjek wer, Uzytkownicy u WHERE wer.Historyjka_uzytkownika_id='" + historyjka_id + "' AND u.id = wer.Uzytkownik_id ORDER BY wer.data DESC";
            SqlDataReader reader = zapytanie.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    data = reader.GetDateTime(1);
                    wersje_DropDownList.Items.Add(new ListItem(data.ToString() + " " + reader.GetString(2) + " " + reader.GetString(3) + " (login: " + reader.GetString(4) + ") ", reader.GetInt32(0).ToString()));
                }
                reader.Close();
            }
            catch
            {
                reader.Dispose();
            }
        }

        // Wczytanie wersji historyjki wybranej z listy
        protected void wersje_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "SELECT wer.tresc, wer.uwagi, wer.udzialowcy FROM Wersje_historyjek wer WHERE wer.id='" + wersje_DropDownList.SelectedValue + "'";

            SqlDataReader reader;
            reader = zapytanie.ExecuteReader();
            try
            {
                reader.Read();
                tresc_TextBox.Text = reader.GetString(0);
                uwagi_TextBox.Text = reader.GetString(1);
                udzialowcy_TextBox.Text = reader.GetString(2);
                reader.Close();
            }
            catch
            {
                reader.Dispose();
            }
        }

        // Usunięcie przypisania historyjki do iteracji
        protected void przypisanie_Button_Click(object sender, EventArgs e)
        {
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "UPDATE Historyjki_uzytkownikow SET Iteracje_id = NULL, nr_iteracji = NULL, nr_wydania = NULL WHERE id='" + historyjka_id + "';";
            try
            {
                zapytanie.ExecuteNonQuery();
                przypisanie_Button.Visible = false;
                wydanie_Label.Visible = false;
                iteracja_Label.Visible = false;
                brak_przypisania_Label.Visible = true;
            }
            catch { }
        }

        // Zapisanie nowej wersji historyjki użytkownika
        protected void nowaWersja_Button_Click(object sender, EventArgs e)
        {
            string tresc = tresc_TextBox.Text;
            string uwagi = uwagi_TextBox.Text;
            string udzialowcy = udzialowcy_TextBox.Text;

            if (tresc.CompareTo(tresc1) != 0 || uwagi.CompareTo(uwagi1) != 0 || udzialowcy.CompareTo(udzialowcy1) != 0)
            {
                int poprawne_dl = 1;
                if (tresc_TextBox.Text.Length > 500)
                {
                    opisR_Label.Visible = true;
                    poprawne_dl = 0;
                }
                else
                {
                    opisR_Label.Visible = false;
                }

                if (uwagi_TextBox.Text.Length > 255)
                {
                    uwagiR_Label.Visible = true;
                    poprawne_dl = 0;
                }
                else
                {
                    uwagiR_Label.Visible = false;
                }

                if (udzialowcy_TextBox.Text.Length > 255)
                {
                    udzialowcyR_Label.Visible = true;
                    poprawne_dl = 0;
                }
                else
                {
                    udzialowcyR_Label.Visible = false;
                }

                if (poprawne_dl == 1)
                {
                    try
                    {
                        SqlCommand zapytanie = new SqlCommand();
                        zapytanie.Connection = conn;
                        zapytanie.CommandType = CommandType.Text;
                        zapytanie.CommandText = "INSERT INTO Wersje_historyjek (Historyjka_uzytkownika_id, tresc, uwagi, udzialowcy, Uzytkownik_id, nr_rewizji) VALUES ('" + historyjka_id + "', '" + tresc + "','" + uwagi + "', '" + udzialowcy + "', '" + user_id + "', '" + akt_rewizja + "');";
                        zapytanie.ExecuteNonQuery();

                        zapytanie.CommandText = "UPDATE Zadania_programistyczne SET status='Do weryfikacji' WHERE Historyjka_uzytkownika_id='" + historyjka_id + "';";
                        zapytanie.ExecuteNonQuery();

                        zapytanie.CommandText = "UPDATE Przypadki_testowe SET status='Do weryfikacji' WHERE Zadanie_programistyczne_id IN (SELECT z.id FROM Zadania_programistyczne z WHERE z.Historyjka_uzytkownika_id='" + historyjka_id + "');";
                        zapytanie.ExecuteNonQuery();

                        GridView1.DataBind();
                        zaladuj_wersje_DropDownList();
                    }
                    catch { }
                }
            }
        }

        // Powrót do poprzedniej strony
        protected void powrot_Button_Click(object sender, EventArgs e)
        {
            if (powroty.Count > 1)
            {
                // zdjęcie elementów dodanych na tej stronie
                powroty.RemoveAt(powroty.Count - 1);
                powroty_id.RemoveAt(powroty_id.Count - 1);

                string strona = powroty[powroty.Count - 1];
                int elem_id = powroty_id[powroty_id.Count - 1];
                powroty.RemoveAt(powroty.Count - 1);
                powroty_id.RemoveAt(powroty_id.Count - 1);

                if (strona.CompareTo("Wymaganie.aspx") == 0)
                {
                    Session["wymaganie_id"] = elem_id.ToString();
                }
                else if (strona.CompareTo("Plik.aspx") == 0)
                {
                    Session["id_pliku"] = elem_id;
                }
                else if (strona.CompareTo("PrzypadekTestowy.aspx") == 0)
                {
                    Session["przypadek_id"] = elem_id;
                }
                else if (strona.CompareTo("WykonaniePrzypadku.aspx") == 0)
                {
                    Session["wykonanie_id"] = elem_id;
                }
                else if (strona.CompareTo("ZadanieProgramistyczne.aspx") == 0)
                {
                    Session["zadanie_id"] = elem_id.ToString();
                }
                else if (strona.CompareTo("Historyjka.aspx") == 0)
                {
                    Session["historyjka_id"] = elem_id;
                }

                Session["powroty"] = powroty;
                Session["powroty_id"] = powroty_id;

                Server.Transfer(strona);
            }
        }
        
        // Przejście do tworzenia nowego zadanias
        protected void zadanie_Button_Click(object sender, EventArgs e)
        {
            Session["back"] = "Historyjka.aspx";
            Server.Transfer("NoweZadanie.aspx");
        }

        // Zmiana wybranej pozycji na liście statusu
        protected void status_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (status_DropDownList.SelectedValue.ToString().CompareTo(status) != 0)
            {
                status_Button.Visible = true;
            }
            else
            {
                status_Button.Visible = false;
            }
        }
        
        // Zmiana statusu wymagania - operacja po wciśnięciu przycisku zmiany statusu
        protected void status_Button_Click(object sender, EventArgs e)
        {
            string operacja = status_DropDownList.SelectedValue;
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "UPDATE Historyjki_uzytkownikow SET status = '" + operacja + "', Iteracje_id = NULL, nr_wydania = NULL, nr_iteracji = NULL WHERE id = '" + historyjka_id + "'";
            try
            {
                zapytanie.ExecuteNonQuery();

                zapytanie.CommandText = "UPDATE Zadania_programistyczne SET status = 'Do weryfikacji', Realizator1_id = NULL, Realizator2_id = NULL WHERE Historyjka_uzytkownika_id = '" + historyjka_id + "'";
                zapytanie.ExecuteNonQuery();

                zapytanie.CommandText = "UPDATE Przypadki_testowe SET status = 'Do weryfikacji' WHERE Zadanie_programistyczne_id IN (SELECT id FROM Zadania_programistyczne WHERE Historyjka_uzytkownika_id = '" + historyjka_id + "') ";
                zapytanie.ExecuteNonQuery();

                status = operacja;                
                status_Button.Visible = false;
                GridView1.DataBind();

                if (status.CompareTo("Usunięta") == 0)
                {
                    wydanie_Label.Visible = false;
                    iteracja_Label.Visible = false;
                    brak_przypisania_Label.Visible = true;
                    przypisanie_Button.Visible = false;
                }
            }
            catch { }                        
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int no = 0;
            // Przejście do strony wybranego zadania programistycznego
            if (e.CommandName.CompareTo("Zadanie") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                Session["zadanie_id"] = no.ToString();
                Session["back"] = "Historyjka.aspx";
                Server.Transfer("ZadanieProgramistyczne.aspx");
            }
        }
    }
}