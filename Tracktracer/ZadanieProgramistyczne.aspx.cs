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
    public partial class ZadanieProgramistyczne : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;
        private int projekt_id;
        private string nazwa;
        private string tresc1;        
        private string zadanie_id;
        private string status;
        private DateTime data;
        private int akt_rewizja;
        private int historyjka_id;
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
                zadanie_id = (string)Session["zadanie_id"];
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
                powroty.Add("ZadanieProgramistyczne.aspx");
                powroty_id.Add(Convert.ToInt32(zadanie_id));
            }
            Session["powroty"] = powroty;
            Session["powroty_id"] = powroty_id;

            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "SELECT z.nazwa, z.status, z.nr_rewizji, h.nr_wydania, h.nr_iteracji, wer.tresc_Zadania, wer.data, h.id, h.nazwa FROM Zadania_programistyczne z, Wersje_zadan_programistycznych wer, Historyjki_uzytkownikow h WHERE z.id=@zadanie_id AND h.id = z.Historyjka_uzytkownika_id AND wer.Zadanie_programistyczne_id = z.id ORDER BY wer.data DESC;";
            zapytanie.Parameters.AddWithValue("@zadanie_id", zadanie_id);

            SqlDataReader reader;
            reader = zapytanie.ExecuteReader();
            try
            {
                reader.Read();
                zadNazwa_Label.Text = reader.GetString(0);
                status = reader.GetString(1);
                akt_rewizja = reader.GetInt32(2);
                historyjka_id = reader.GetInt32(7);
                Session["historyjka_id"] = historyjka_id;
                historyjka_Link.Text = reader.GetString(8);
                
                tresc1 = reader.GetString(5);

                if (!IsPostBack)
                {
                    tresc_TextBox.Text = tresc1;
                    ustaw_DropDownList(status);
                }

                if (!reader.IsDBNull(3) && !reader.IsDBNull(4))
                {
                    wydanie_Label.Text = "Wydanie: " + reader.GetInt32(3);
                    iteracja_Label.Text = "Iteracja: " + reader.GetInt32(4);
                }
                else
                {
                    przypisanie_Label.Text = "Zadanie nie zostało jeszcze przypisane do żadnej iteracji";
                    wydanie_Label.Visible = false;
                    iteracja_Label.Visible = false;                    
                }
                
                reader.Close();
            }
            catch
            {
                reader.Dispose();
            }

            ustaw_realizatorow();
            projNazwa_Label.Text = nazwa;
            if (!IsPostBack) zaladuj_wersje_DropDownList();
            trescR_Label.Visible = false;                        
        }

        protected void ustaw_DropDownList(string status)
        {
            if (status.CompareTo("Aktywne") == 0)
            {
                DropDownList1.SelectedIndex = 0;
            }
            else if (status.CompareTo("Zakończone") == 0)
            {
                DropDownList1.SelectedIndex = 1;
            }
            else if (status.CompareTo("Do weryfikacji") == 0)
            {
                DropDownList1.Items[3].Enabled = true;
                DropDownList1.SelectedIndex = 3;
            }
            else
            {
                DropDownList1.Items[2].Enabled = true;
                DropDownList1.SelectedIndex = 2;
                AccordionPane4.Visible = false;
            }
        }

        protected void zaladuj_wersje_DropDownList()
        {
            wersje_DropDownList.Items.Clear();
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "SELECT wer.id, wer.data, u.imie, u.nazwisko, u.login FROM Wersje_zadan_programistycznych wer, Uzytkownicy u WHERE wer.Zadanie_programistyczne_id=@zadanie_id AND u.id = wer.Uzytkownik_id ORDER BY wer.data DESC";
            zapytanie.Parameters.AddWithValue("@zadanie_id", zadanie_id);
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

        // Zmiana statusu zadania (Aktywne / Zakończone / Usunięte / Do weryfikacji)
        protected void status_Button_Click(object sender, EventArgs e)
        {
            string operacja = DropDownList1.SelectedValue;
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "UPDATE Zadania_programistyczne SET status = '" + operacja + "' WHERE id = '" + zadanie_id + "'";
            try
            {
                zapytanie.ExecuteNonQuery();
                if (operacja.CompareTo("Aktywne") == 0 && status.CompareTo("Usunięte") == 0)
                {
                    zapytanie.CommandText = "SELECT wh.udzialowcy FROM Wersje_historyjek wh WHERE wh.Historyjka_uzytkownika_id=@historyjka_id ";
                    zapytanie.Parameters.AddWithValue("@historyjka_id", historyjka_id);
                      
                }
                status = operacja;
                DropDownList1.Items[2].Enabled = false;
                status_Button.Visible = false;
                AccordionPane4.Visible = true;
            }
            catch { }

            ustaw_DropDownList(operacja);
        }

        // Wczytanie wskazanej wersji danego zadania
        protected void wersje_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "SELECT wer.tresc_Zadania FROM Wersje_zadan_programistycznych wer WHERE wer.id=@wersje_DropDownList";
            zapytanie.Parameters.AddWithValue("@wersje_DropDownList", wersje_DropDownList.SelectedValue);
                                  
            
            try
            {                
                tresc_TextBox.Text = (string)zapytanie.ExecuteScalar();                
            }
            catch
            {                
            }
        }
        
        // Zapisanie nowej wersji zadania
        protected void nowaWersja_Button_Click(object sender, EventArgs e)
        {
            string tresc = tresc_TextBox.Text;

            if (tresc.CompareTo(tresc1) != 0)
            {
                int poprawne_dl = 1;
                if (tresc_TextBox.Text.Length > 500)
                {
                    trescR_Label.Visible = true;
                    poprawne_dl = 0;
                }
                else
                {
                    trescR_Label.Visible = false;
                }

                if (poprawne_dl == 1)
                {
                    try
                    {
                        SqlCommand zapytanie = new SqlCommand();
                        zapytanie.Connection = conn;
                        zapytanie.CommandType = CommandType.Text;
                        zapytanie.CommandText = "INSERT INTO Wersje_zadan_programistycznych (Zadanie_programistyczne_id, tresc_Zadania, Uzytkownik_id, nr_rewizji) VALUES (@zadanie_id ,@tresc ,@user_id ,@akt_rewizja );";
                        zapytanie.Parameters.AddWithValue("@zadanie_id", zadanie_id);
                        zapytanie.Parameters.AddWithValue("@tresc", tresc);
                        zapytanie.Parameters.AddWithValue("@user_id", user_id);
                        zapytanie.Parameters.AddWithValue("@akt_rewizja", akt_rewizja);
                        zapytanie.ExecuteNonQuery();
                        zapytanie.CommandText = "UPDATE Zadania_programistyczne SET wersja = wersja + 1 WHERE id =@zadanie_id ";
                        zapytanie.Parameters.AddWithValue("@zadanie_id", zadanie_id);
                        zapytanie.ExecuteNonQuery();
                        zapytanie.CommandText = "UPDATE Przypadki_testowe SET status = 'Do weryfikacji' WHERE Zadanie_programistyczne_id =@zadanie_id ";
                        zapytanie.Parameters.AddWithValue("@zadanie_id", zadanie_id);
                        zapytanie.ExecuteNonQuery();

                        
                        zaladuj_wersje_DropDownList();
                        GridView5.DataBind();
                    }
                    catch { }
                }
            }
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int no = 0;
            if (e.CommandName.CompareTo("Zadanie") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                Session["zadanie_id"] = no.ToString();
                Session["back"] = "Historyjka.aspx";
                Server.Transfer("ZadanieProgramistyczne.aspx");
            }
            else if (e.CommandName.CompareTo("usunPowiazaneZadanie") == 0)
            // Usunięcie powiązań z innymi zadaniami.
            {
                no = Convert.ToInt32(e.CommandArgument);
                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "DELETE FROM Powiazane_zadania WHERE (Zadanie1_id =@zadanie_id AND Zadanie2_id=@no ) OR (Zadanie1_id =@no AND Zadanie2_id=@zadanie_id )";
                zapytanie.Parameters.AddWithValue("@zadanie_id", zadanie_id);
                zapytanie.Parameters.AddWithValue("@no", no);
                try
                {
                    zapytanie.ExecuteNonQuery();
                    GridView1.DataBind();
                    GridView2.DataBind();
                }
                catch { }
            }
            else if (e.CommandName.CompareTo("Powiaz") == 0)
            {
                Powiaz_plik(Convert.ToString(e.CommandArgument));
            }
            else if (e.CommandName.CompareTo("Plik") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                Session["id_pliku"] = no;
                Session["back"] = "ZadanieProgramistyczne.aspx";
                Session["back_id"] = zadanie_id;
                Server.Transfer("Plik.aspx");
            }
            else if (e.CommandName.CompareTo("PowiazZadanie") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                powiaz_zadanie(no.ToString());            
            }
            else if (e.CommandName.CompareTo("UsunPlik") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                usun_powiazanie_pliku(no.ToString());                
            }
            else if (e.CommandName.CompareTo("PrzypadekTestowy") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                Session["przypadek_id"] = no;
                Session["back"] = "ZadanieProgramistyczne.aspx";
                Session["back_id"] = zadanie_id;
                Server.Transfer("PrzypadekTestowy.aspx");
            }

            // Dodanie realizatora do zadania (pierwszego lub drugiego, jeśli jest już pierwszy)
            else if (e.CommandName.CompareTo("DodajRealizatora") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);                                                
                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "SELECT Realizator1_id FROM Zadania_programistyczne WHERE id =@zadanie_id ";
                zapytanie.Parameters.AddWithValue("@zadanie_id", zadanie_id);
                SqlDataReader reader = zapytanie.ExecuteReader();
                try
                {
                    string sql;
                    reader.Read();
                    if (reader.IsDBNull(0))
                    {
                        sql = "UPDATE Zadania_programistyczne SET Realizator1_id =@no WHERE id =@zadanie_id ";
                    }
                    else
                    {
                        sql = "UPDATE Zadania_programistyczne SET Realizator2_id =@no WHERE id =@zadanie_id ";
                    }
                    reader.Close();
                    zapytanie.CommandText = sql;
                    zapytanie.Parameters.AddWithValue("@zadanie_id", zadanie_id);
                    zapytanie.Parameters.AddWithValue("@no", no);
                    zapytanie.ExecuteNonQuery();
                    ustaw_realizatorow();
                    GridView7.DataBind();
                }
                catch 
                {
                    reader.Dispose();
                }
            }


        }

        // Powiązanie z innym zadaniem programistycznym. Z dodaniem komentarza lub bez niego.
        protected void powiaz_zadanie(string do_powiazania)
        {
            string komentarz = komentarz_TextBox.Text;          
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "SELECT wersja FROM Zadania_programistyczne WHERE id =@zadanie_id ";
            zapytanie.Parameters.AddWithValue("@zadanie_id", zadanie_id);
            try
            {
                Int32 wym_wersja = (Int32)zapytanie.ExecuteScalar();
                zapytanie.CommandText = "SELECT wersja FROM Zadania_programistyczne WHERE id =@do_powiazania ";
                zapytanie.Parameters.AddWithValue("@do_powiazania", do_powiazania);
                Int32 do_pow_wersja = (Int32)zapytanie.ExecuteScalar();
                if (string.IsNullOrEmpty(komentarz))
                {
                    zapytanie.CommandText = "INSERT INTO Powiazane_zadania (Zadanie1_id, wersja1, Zadanie2_id, wersja2, opis) VALUES (@zadanie_id ,@wym_wersja ,@do_powiazania ,@do_pow_wersja , NULL)";
                    zapytanie.Parameters.AddWithValue("@zadanie_id", zadanie_id);
                    zapytanie.Parameters.AddWithValue("@wym_wersja", wym_wersja);
                    zapytanie.Parameters.AddWithValue("@do_powiazania", do_powiazania);
                    zapytanie.Parameters.AddWithValue("@do_pow_wersja", do_pow_wersja);
                }
                else
                {
                    zapytanie.CommandText = "INSERT INTO Powiazane_zadania (Zadanie1_id, wersja1, Zadanie2_id, wersja2, opis) VALUES (@zadanie_id ,@wym_wersja ,@do_powiazania ,@do_pow_wersja ,@komentarz )";
                    zapytanie.Parameters.AddWithValue("@zadanie_id", zadanie_id);
                    zapytanie.Parameters.AddWithValue("@wym_wersja", wym_wersja);
                    zapytanie.Parameters.AddWithValue("@do_powiazania", do_powiazania);
                    zapytanie.Parameters.AddWithValue("@do_pow_wersja", do_pow_wersja);
                    zapytanie.Parameters.AddWithValue("@komentarz", komentarz);
                }

                zapytanie.ExecuteNonQuery();
                GridView1.DataBind();
                GridView2.DataBind();
                komentarz_TextBox.Text = string.Empty;
            }
            catch
            {
            }
        }
        
        // Usunięcie powiązań z plikiem        
        protected void usun_powiazanie_pliku(string plik_do_usuniecia)
        {            
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "DELETE FROM Pliki_zadania_programistyczne WHERE Plik_id =@plik_do_usuniecia AND Zadanie_programistyczne_id=@zadanie_id ";
            zapytanie.Parameters.AddWithValue("@plik_do_usuniecia", plik_do_usuniecia);
            zapytanie.Parameters.AddWithValue("@zadanie_id", zadanie_id);
            try
            {
                zapytanie.ExecuteNonQuery();
                GridView3.DataBind();
                GridView4.DataBind();
            }
            catch { }
        }

        // Dodanie nowego przypadku testowego.
        protected void nowyPT_Button_Click(object sender, EventArgs e)
        {
            Session["back"] = "ZadanieProgramistyczne.aspx";
            Server.Transfer("NowyTest.aspx");
        }

        // Oznaczenie zadania jako usuniętego.
        protected void usun_Button_Click(object sender, EventArgs e)
        {
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "UPDATE Zadania_programistyczne SET status = 'Usunięte', Realizator1_id = NULL, Realizator2_id = NULL WHERE id =@zadanie_id ";
            zapytanie.Parameters.AddWithValue("@zadanie_id", zadanie_id);
            try
            {
                zapytanie.ExecuteNonQuery();

                zapytanie.CommandText = "UPDATE Przypadki_testowe SET status = 'Do weryfikacji' WHERE Zadanie_programistyczne_id =@zadanie_id ";
                zapytanie.Parameters.AddWithValue("@zadanie_id", zadanie_id);
                zapytanie.ExecuteNonQuery();
                status = "Usunięte";
            }
            catch { }

            realizator_Label.Visible = false;
            realizator2_Label.Visible = false;

            ustaw_DropDownList("Usunięte");
        }

        // Anulowanie chęci usunięcia.
        protected void nie_usuwaj_Button_Click(object sender, EventArgs e)
        {
            Accordion1.SelectedIndex = 0;
        }

        // Powrót do poprzedniej strony.
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

        // Przejście do strony historyjki, z której wywodzi się wymaganie.
        protected void historyjka_Link_Click(object sender, EventArgs e)
        {
            Session["back"] = "ZadanieProgramistyczne.aspx";
            Session["historyjka_id"] = historyjka_id;
            Server.Transfer("Historyjka.aspx");
        }
        
        // Usunięcie realizatora 1
        protected void realizator1_Link_Click(object sender, EventArgs e)
        {
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "UPDATE Zadania_programistyczne SET Realizator1_id = NULL WHERE id =@zadanie_id ";
            zapytanie.Parameters.AddWithValue("@zadanie_id", zadanie_id);
            try
            {
                zapytanie.ExecuteNonQuery();
                ustaw_realizatorow();
                GridView7.DataBind();
            }
            catch { }            
        }

        //Usunięcie realizatora 2
        protected void realizator2_Link_Click(object sender, EventArgs e)
        {
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "UPDATE Zadania_programistyczne SET Realizator2_id = NULL WHERE id =@zadanie_id ";
            zapytanie.Parameters.AddWithValue("@zadanie_id", zadanie_id);
            try
            {
                zapytanie.ExecuteNonQuery();
                ustaw_realizatorow();
                GridView7.DataBind();
            }
            catch { }
        }

        // Powiązanie zadania z wybranym plikiem.
        protected void Powiaz_plik(string plik_do_powiazania)
        {
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "INSERT INTO Pliki_Zadania_programistyczne VALUES (@plik_do_powiazania ,@zadanie_id )";
            zapytanie.Parameters.AddWithValue("@plik_do_powiazania", plik_do_powiazania);
            zapytanie.Parameters.AddWithValue("@zadanie_id", zadanie_id);
            try
            {
                zapytanie.ExecuteNonQuery();
                GridView3.DataBind();
                GridView4.DataBind();
            }
            catch { }
        }

        // Ustawienie kontrolek odpowiadających za wyświetlanie realizatorów
        protected void ustaw_realizatorow()
        {
            SqlDataReader reader;

            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "SELECT u.imie, u.nazwisko, u.login FROM Zadania_programistyczne z, Uzytkownicy u WHERE z.id=@zadanie_id AND u.id = z.Realizator1_id;";
            zapytanie.Parameters.AddWithValue("@zadanie_id", zadanie_id);
            reader = zapytanie.ExecuteReader();
            try
            {
                if (reader.Read())
                {
                    realizator_Label.Visible = true;
                    realizator_Label.Text = "<br />Osoba realizująca wymaganie: " + reader.GetString(0) + " " + reader.GetString(1) + " (login: " + reader.GetString(2) + " ) ";
                    realizator1_LinkButton.Visible = true;
                }
                else
                {
                    realizator_Label.Visible = false;
                    realizator1_LinkButton.Visible = false;
                }
                reader.Close();
                zapytanie.CommandText = "SELECT u.imie, u.nazwisko, u.login FROM Zadania_programistyczne z, Uzytkownicy u WHERE z.id=@zadanie_id AND u.id = z.Realizator2_id;";
                zapytanie.Parameters.AddWithValue("@zadanie_id", zadanie_id);
                reader = zapytanie.ExecuteReader();
                if (reader.Read())
                {
                    realizator2_Label.Visible = true;
                    realizator2_Label.Text = "<br />Osoba realizująca wymaganie: " + reader.GetString(0) + " " + reader.GetString(1) + " (login: " + reader.GetString(2) + " ) ";
                    realizator2_LinkButton.Visible = true;
                }
                else
                {
                    realizator2_Label.Visible = false;
                    realizator2_LinkButton.Visible = false;
                }
                reader.Close();

                if (realizator_Label.Visible == true && realizator2_Label.Visible == true) GridView7.Visible = false;
                else GridView7.Visible = true;
            }
            catch
            {
                reader.Dispose();
            }
        }
    }
}