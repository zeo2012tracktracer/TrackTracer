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
    public partial class Wymaganie : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;
        private SqlConnection conn2;
        private int projekt_id;
        private string nazwa;
        private string nazwa_wymagania;
        private string wymaganie_id;
        private string opis1;
        private string uwagi1;
        private string udzialowcy1;
        private string status;
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
                conn2 = (SqlConnection)Session["connection2"];
                projekt_id = (int)Session["projekt_id"];
                nazwa = (string)Session["nazwa"];
                wymaganie_id = (string)Session["wymaganie_id"];
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
                powroty.Add("Wymaganie.aspx");
                powroty_id.Add(Convert.ToInt32(wymaganie_id));
            }
            Session["powroty"] = powroty;
            Session["powroty_id"] = powroty_id;

            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "SELECT w.nazwa, w.status, w.nr_rewizji, w.nr_wydania, w.nr_iteracji, wer.opis, wer.uwagi, wer.udzialowcy, wer.data FROM Wymagania w, Wersje_wymagan wer WHERE w.id='" + wymaganie_id + "' AND wer.Wymaganie_id = w.id ORDER BY wer.data DESC;";

            SqlDataReader reader;
            reader = zapytanie.ExecuteReader();
            try
            {
                reader.Read();
                nazwa_wymagania = reader.GetString(0);
                wymNazwa_Label.Text = nazwa_wymagania;
                status = reader.GetString(1);
                opis1 = reader.GetString(5);
                uwagi1 = reader.GetString(6);
                udzialowcy1 = reader.GetString(7);
//                akt_rewizja = reader.GetInt32(2);                

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
                if (!IsPostBack)
                {
                    ustaw_DropDownList(status);                    
                    opis_TextBox.Text = opis1;                    
                    uwagi_TextBox.Text = uwagi1;                    
                    udzialowcy_TextBox.Text = udzialowcy1;
                }

                reader.Close();
            }
            catch
            {
                reader.Dispose();
            }

            zapytanie.CommandText = "SELECT p.rewizja FROM Projekty p WHERE p.id='" + projekt_id + "'";
            akt_rewizja = (int)zapytanie.ExecuteScalar();
            rew_Label.Text = akt_rewizja.ToString();

            zapytanie.CommandText = "SELECT u.imie, u.nazwisko, u.login FROM Wymagania w, Uzytkownicy u WHERE w.id='" + wymaganie_id + "' AND u.id = w.Uzytkownik_id;";
            reader = zapytanie.ExecuteReader();
            try
            {
                if (reader.Read())
                {
                    realizator_Label.Visible = true;
                    realizator_Label.Text = reader.GetString(0) + " " + reader.GetString(1) + " (login: " + reader.GetString(2) + " )";
                }
                else 
                {
                    realizator_Label.Visible = false;
                }
                

                reader.Close();
            }
            catch
            {
                reader.Dispose();
            }

            RangeValidator1.MaximumValue = akt_rewizja.ToString();
            projNazwa_Label.Text = nazwa;
            if(!IsPostBack) zaladuj_wersje_DropDownList();
            opisR_Label.Visible = false;
            uwagiR_Label.Visible = false;
            udzialowcyR_Label.Visible = false;
            TabContainer1.Visible = true;
            
        }

        // Usunięcie przypisania do iteracji. Usuwany jest również realizator wymagania.
        protected void przypisanie_Button_Click(object sender, EventArgs e)
        {
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "UPDATE Wymagania SET Iteracje_id = NULL, nr_iteracji = NULL, nr_wydania = NULL, Uzytkownik_id = NULL WHERE id='" + wymaganie_id + "';";
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
                
        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int no = 0;
            if (e.CommandName.CompareTo("Wymaganie") == 0)
            {                                
                no = Convert.ToInt32(e.CommandArgument);                
                Session["wymaganie_id"] = no.ToString();
                Session["back"] = "Wymaganie.aspx";
                Session["back_id"] = wymaganie_id;
                Server.Transfer("Wymaganie.aspx");
            }
            else if (e.CommandName.CompareTo("Plik") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                Session["id_pliku"] = no;
                Session["back"] = "Wymaganie.aspx";
                Session["back_id"] = wymaganie_id;
                Server.Transfer("Plik.aspx");
            }
            else if (e.CommandName.CompareTo("Powiaz") == 0)
            {                
                GridView4_Powiaz_plik(Convert.ToString(e.CommandArgument));
            }
            else if (e.CommandName.CompareTo("PrzypadekTestowy") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                Session["przypadek_id"] = no;
                Session["back"] = "Wymaganie.aspx";
                Session["back_id"] = wymaganie_id;
                Server.Transfer("PrzypadekTestowy.aspx");
            }
            else if (e.CommandName.CompareTo("Weryfikuj") == 0)
            {                
                oznacz_jako_zweryfikowane(Convert.ToString(e.CommandArgument));
            }
                // Nowe powiązanie z wymaganiem
            else if (e.CommandName.CompareTo("Powiaz_wym") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                string komentarz = komentarz_TextBox.Text;
                string do_powiazania = no.ToString();
                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "SELECT wersja FROM Wymagania WHERE id = '" + wymaganie_id + "'";
                try
                {
                    Int32 wym_wersja = (Int32)zapytanie.ExecuteScalar();
                    zapytanie.CommandText = "SELECT wersja FROM Wymagania WHERE id = '" + do_powiazania + "'";
                    Int32 do_pow_wersja = (Int32)zapytanie.ExecuteScalar();
                    if (string.IsNullOrEmpty(komentarz))
                    {
                        zapytanie.CommandText = "INSERT INTO Powiazane_wymagania (Wymaganie1_id, wersja1, Wymaganie2_id, wersja2, opis) VALUES ('" + wymaganie_id + "', '" + wym_wersja + "','" + do_powiazania + "', '" + do_pow_wersja + "', NULL)";
                    }
                    else
                    {
                        zapytanie.CommandText = "INSERT INTO Powiazane_wymagania (Wymaganie1_id, wersja1, Wymaganie2_id, wersja2, opis) VALUES ('" + wymaganie_id + "', '" + wym_wersja + "','" + do_powiazania + "', '" + do_pow_wersja + "', '" + komentarz + "')";
                    }

                    zapytanie.ExecuteNonQuery();
                    GridView1.DataBind();
                    GridView2.DataBind();
                    komentarz_TextBox.Text = string.Empty;
                }
                catch{}
            }
            // Usunięcie powiązań pomiędzy wymaganiami
            else if (e.CommandName.CompareTo("Usun_wym") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                string do_usuniecia = no.ToString();
                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "DELETE FROM Powiazane_wymagania WHERE (Wymaganie1_id = '" + wymaganie_id + "' AND Wymaganie2_id='" + do_usuniecia + "') OR (Wymaganie1_id = '" + do_usuniecia + "' AND Wymaganie2_id='" + wymaganie_id + "')";
                try
                {
                    zapytanie.ExecuteNonQuery();
                    GridView1.DataBind();
                    GridView2.DataBind();
                }
                catch { }
            }
            
        }
       
        // Usunięcie powiązań zadania z plikiem
        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string plik_do_usuniecia = GridView3.SelectedRow.Cells[0].Text;
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "DELETE FROM Pliki_wymagania WHERE Plik_id = '" + plik_do_usuniecia + "' AND Wymaganie_id='" + wymaganie_id + "'";
            try
            {
                zapytanie.ExecuteNonQuery();
                GridView3.DataBind();
                GridView4.DataBind();
            }
            catch { }
        }
      
        // Powiązanie z wybranym plikiem
        protected void GridView4_Powiaz_plik(string plik_do_powiazania)
        {   
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "INSERT INTO Pliki_wymagania VALUES ('" + plik_do_powiazania + "','" + wymaganie_id + "')";
            try
            {
                zapytanie.ExecuteNonQuery();
                GridView3.DataBind();
                GridView4.DataBind();
            }
            catch { }
        }

        protected void nowyPT_Button_Click(object sender, EventArgs e)
        {
            Session["back"] = "Wymaganie.aspx";
            Server.Transfer("NowyTest.aspx");
        }

        protected void usun_Button_Click(object sender, EventArgs e)
        {            
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "UPDATE Wymagania SET status = 'usunięte', nr_wydania = NULL, nr_iteracji = NULL, Iteracje_id = NULL, Uzytkownik_id = NULL WHERE id = '" + wymaganie_id + "'";

            SqlCommand zapytanie2 = new SqlCommand();
            zapytanie2.Connection = conn2;
            zapytanie2.CommandType = CommandType.Text;
            zapytanie2.CommandText = "DELETE FROM historiawymagan WHERE id = '" + wymaganie_id + "'";
            try
            {
                zapytanie.ExecuteNonQuery();
                zapytanie2.ExecuteNonQuery();
                zapytanie2.CommandText = "DELETE FROM wymagania WHERE id = '" + wymaganie_id + "'";
                zapytanie2.ExecuteNonQuery();
                status = "usunięte";
            }
            catch { }

            brak_przypisania_Label.Visible = true;
            wydanie_Label.Visible = false;
            iteracja_Label.Visible = false;
            przypisanie_Button.Visible = false;
            realizator_Label.Visible = false;
            
            ustaw_DropDownList("usunięte");
        }

        protected void nie_usuwaj_Button_Click(object sender, EventArgs e)
        {
            Accordion1.SelectedIndex = 0;
        }

        // Ustawienie odpowiedniego statusu wymagania na liście rozwijalnej
        protected void ustaw_DropDownList(string status)
        {
            if (status.CompareTo("aktywne") == 0)
            {
                DropDownList1.SelectedIndex = 0;
            }
            else if (status.CompareTo("zakończone") == 0)
            {
                DropDownList1.SelectedIndex = 1;
            }
            else
            {
                DropDownList1.Items[2].Enabled = true;
                DropDownList1.SelectedIndex = 2;
                AccordionPane4.Visible = false;
            }
        }

        // Zmiana statusu wymagania - operacja po wciśnięciu przycisku zmiany statusu
        protected void status_Button_Click(object sender, EventArgs e)
        {
            string operacja = DropDownList1.SelectedValue;
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "UPDATE Wymagania SET status = '" + operacja + "' WHERE id = '" + wymaganie_id + "'";
            try
            {
                zapytanie.ExecuteNonQuery();

                if (operacja.CompareTo("aktywne") == 0 && status.CompareTo("do weryfikacji") != 0)
                {
                    SqlCommand zapytanie2 = new SqlCommand();
                    zapytanie2.Connection = conn2;
                    zapytanie2.CommandType = CommandType.Text;
                    string sql2 = "SET IDENTITY_INSERT wymagania ON;";
                    sql2 += "INSERT INTO wymagania (id, FK_id_projektu, FK_id_nadrzednego_wymagania) VALUES ('" + wymaganie_id + "', '" + projekt_id + "', NULL);";
                    sql2 += "SET IDENTITY_INSERT wymagania OFF;";
                    zapytanie2.CommandText = sql2;
                    zapytanie2.ExecuteNonQuery();

                    sql2 = "SET IDENTITY_INSERT historiawymagan ON;";
                    sql2 += "INSERT INTO historiawymagan (id, nazwa, opis, zrodlo, priorytet, status, FK_id_wymagania, FK_id_pracownika) VALUES ('" + wymaganie_id + "', '" + nazwa_wymagania + "', '" + opis1 + "', '" + udzialowcy1 + "', 'Normalny', 'Aktywne','" + wymaganie_id + "', '" + user_id + "');";
                    sql2 += "SET IDENTITY_INSERT historiawymagan OFF;";
                    zapytanie2.CommandText = sql2;
                    zapytanie2.ExecuteNonQuery();
                }
                status = operacja;
                DropDownList1.Items[2].Enabled = false;
                status_Button.Visible = false;
                AccordionPane4.Visible = true;
            }
            catch { }

            ustaw_DropDownList(operacja);
            
        }

        // Zmiena wybranego statusu na liście rozwijalnej
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue.CompareTo(status) == 0)
            {
                status_Button.Visible = false;
            }
            else
            {
                status_Button.Visible = true;
            }
        }

        // Zapisanie nowej wersji wymagania
        protected void nowaWersja_Button_Click(object sender, EventArgs e)
        {
            string opis = opis_TextBox.Text;
            string uwagi = uwagi_TextBox.Text;
            string udzialowcy = udzialowcy_TextBox.Text;

            if (opis.CompareTo(opis1) != 0 || uwagi.CompareTo(uwagi1) != 0 || udzialowcy.CompareTo(udzialowcy1) != 0)
            {

                int poprawne_dl = 1;
                if (opis_TextBox.Text.Length > 500)
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
                        zapytanie.CommandText = "INSERT INTO Wersje_wymagan (Wymaganie_id, opis, uwagi, udzialowcy, Uzytkownik_id, nr_rewizji) VALUES ('" + wymaganie_id + "', '" + opis + "','" + uwagi + "', '" + udzialowcy + "', '" + user_id + "', '" + akt_rewizja + "');";

                        SqlCommand zapytanie2 = new SqlCommand();
                        zapytanie2.Connection = conn2;
                        zapytanie2.CommandType = CommandType.Text;
                        zapytanie2.CommandText = "UPDATE historiawymagan SET opis='" + opis + "', data_modyfikacji=GETDATE(), zrodlo='" + udzialowcy + "' WHERE id='" + wymaganie_id + "'";

                        zapytanie.ExecuteNonQuery();
                        zapytanie.CommandText = "UPDATE Wymagania SET wersja = wersja + 1 WHERE id = '" + wymaganie_id + "'";
                        zapytanie.ExecuteNonQuery();
                        zapytanie.CommandText = "UPDATE Przypadki_testowe SET status = 'Do weryfikacji' WHERE Wymaganie_id = '" + wymaganie_id + "'";
                        zapytanie.ExecuteNonQuery();
                        zaladuj_wersje_DropDownList();
                        GridView5.DataBind();
                    }
                    catch { }
                }
            }
        }

        protected void wersje_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "SELECT wer.opis, wer.uwagi, wer.udzialowcy FROM Wersje_wymagan wer WHERE wer.id='" + wersje_DropDownList.SelectedValue + "'";

            SqlDataReader reader;
            reader = zapytanie.ExecuteReader();
            try
            {
                reader.Read();                
                opis_TextBox.Text = reader.GetString(0);                               
                uwagi_TextBox.Text = reader.GetString(1);                                
                udzialowcy_TextBox.Text = reader.GetString(2);
                reader.Close();
            }
            catch 
            {
                reader.Dispose();
            }

        }

        protected void zaladuj_wersje_DropDownList()
        {
            wersje_DropDownList.Items.Clear();
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            if (rew_CheckBox.Checked == false)
            {
                zapytanie.CommandText = "SELECT wer.id, wer.data, u.imie, u.nazwisko, u.login FROM Wersje_wymagan wer, Uzytkownicy u WHERE wer.Wymaganie_id='" + wymaganie_id + "' AND u.id = wer.Uzytkownik_id ORDER BY wer.data DESC";
            }
            else
            {
                int rew = Convert.ToInt32(rew_TextBox.Text);
                rew++;
                zapytanie.CommandText = "SELECT wer.id, wer.data, u.imie, u.nazwisko, u.login FROM Wersje_wymagan wer, Uzytkownicy u WHERE wer.Wymaganie_id='" + wymaganie_id + "' AND wer.nr_rewizji < " + rew + " AND u.id = wer.Uzytkownik_id ORDER BY wer.data DESC";
            }
            SqlDataReader reader = zapytanie.ExecuteReader();
            try
            {                                
                while (reader.Read())
                {
                    data = reader.GetDateTime(1);
                    wersje_DropDownList.Items.Add(new ListItem(data.ToString()+ " " + reader.GetString(2) + " " + reader.GetString(3) + " (login: " + reader.GetString(4) + ") ", reader.GetInt32(0).ToString()));
                }
                reader.Close();                                                
            }
            catch 
            {
                reader.Dispose();
            }            
        }

        protected void oznacz_jako_zweryfikowane(string id)
        {
            int wersja;
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "SELECT wersja FROM Wymagania WHERE id = '" + id + "'";
            try
            {
                wersja = (int)zapytanie.ExecuteScalar();
                zapytanie.CommandText = "UPDATE Powiazane_wymagania SET wersja1 ='" + wersja + "' WHERE Wymaganie1_id = '" + id + "' AND Wymaganie2_id = '" + wymaganie_id + "'";
                zapytanie.ExecuteNonQuery();
                zapytanie.CommandText = "UPDATE Powiazane_wymagania SET wersja2 ='" + wersja + "' WHERE Wymaganie2_id = '" + id + "' AND Wymaganie1_id = '" + wymaganie_id + "'";
                zapytanie.ExecuteNonQuery();
                GridView2.DataBind();
            }
            catch { }

        }

        protected void rew_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            zaladuj_wersje_DropDownList();
            wersje_DropDownList_SelectedIndexChanged(null, null);           
        }
    }
}