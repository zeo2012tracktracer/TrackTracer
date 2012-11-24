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
    public partial class SzczegolyProjektu : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;
        private int wlasciciel;
        private int projekt_id;
        private string metodyka;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                user_id = (int)Session["user_id"];
                conn = (SqlConnection)Session["connection"];
            }
            catch
            {
                Server.Transfer("Index.aspx");
            }
            Session.Remove("powroty");
            Session.Remove("powroty_id");
            projekt_id = Convert.ToInt32(Request.QueryString["id"]);
            Session["projekt_id"] = projekt_id;

            SqlCommand zap = new SqlCommand();
            zap.Connection = conn;
            zap.CommandType = CommandType.Text;
            zap.CommandText = "SELECT p.metodyka FROM Projekty p WHERE p.id = '" + projekt_id + "';";

            string met;
            int redir = 0;
            
            SqlDataReader reader1;
            try
            {
                reader1 = zap.ExecuteReader();
                reader1.Read();
                met = reader1.GetString(0);
                reader1.Close();

                if (met.CompareTo("XP") == 0)
                {
                    redir = 1;
                    Session["metodyka"] = "XP";
                }
                else
                {
                    Session["metodyka"] = "Scrum";
                }
            }
            catch 
            {
                Server.Transfer("Default.aspx");
            }

            if (redir == 1) Server.Transfer("ProjektXP.aspx");

            zap.CommandText = "SELECT p.nazwa, p.opis, p.metodyka, u.imie, u.nazwisko, u.login, p.wlasciciel, p.rewizja, p.svn_url FROM Projekty p, Uzytkownicy_Projekty up, Uzytkownicy u WHERE up.Uzytkownik_id='" + user_id + "' AND p.id = up.Projekt_id AND p.id = '" + projekt_id + "' AND u.id=p.wlasciciel;";
            
            SqlDataReader reader;
            reader = zap.ExecuteReader();
            try
            {                
                reader.Read();
                                
                Table1.Rows[0].Cells[1].Text = reader.GetString(0);
                metodyka = reader.GetString(2);
                Table1.Rows[1].Cells[1].Text = metodyka;                    
                Table1.Rows[2].Cells[1].Text = reader.GetString(3) + " " + reader.GetString(4) + " ( login: " + reader.GetString(5) + " )";
                Table1.Rows[3].Cells[1].Text = reader.GetString(1);
                try
                {
                    string url = reader.GetString(8);
                    svn_Link.Text = url;
                    svn_Link.NavigateUrl = url;                    
                }
                catch
                {
                    Table1.Rows[4].Cells[1].Text = "Brak ustawień SVN.";
                }
                wlasciciel = reader.GetInt32(6);
                Session["wlasciciel"] = wlasciciel;
                Session["nazwa"] = reader.GetString(0);
                Session["akt_rewizja"] = reader.GetInt32(7);
                reader.Close();

                if (wlasciciel == user_id)
                {
                    Button1.Visible = true;
                    svn_Button.Visible = true;
                    usuniecie_AccordionPane.Visible = true;
                }
                else
                {
                    usuniecie_AccordionPane.Visible = false;
                }
            }
            catch
            {
                reader.Dispose();
                Server.Transfer("Default.aspx");
            }

            Session["back"] = "SzczegolyProjektu.aspx";
                                              
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            if(!IsPostBack) cel_iteracji();            
            base.OnPreRenderComplete(e);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Server.Transfer("PrzypisaniUzytkownicy.aspx");
        }
        
        protected void iteracja_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            cel_iteracji();            
        }

        protected void produkt_Button_Click(object sender, EventArgs e)
        {
            Server.Transfer("RejestrProduktowy.aspx");
        }

        // Dodanie nowego wydania do projektu
        protected void wydanie_Button_Click(object sender, EventArgs e)
        {            
            int akt_wyd;

            SqlTransaction trans = conn.BeginTransaction();

            try
            {
                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "SELECT aktualne_wydanie FROM Projekty WHERE id='" + projekt_id + "';";
                zapytanie.Transaction = trans;

                SqlDataReader reader = zapytanie.ExecuteReader();
                reader.Read();
                akt_wyd = reader.GetInt32(0);
                reader.Close();
                akt_wyd++;

                SqlCommand zapytanie2 = new SqlCommand();
                zapytanie2.Connection = conn;
                zapytanie2.CommandType = CommandType.Text;
                zapytanie2.CommandText = "INSERT INTO Wydania (nr_wydania, Projekty_id) VALUES ('" + akt_wyd + "', '" + projekt_id + "');";
                zapytanie2.Transaction = trans;
                zapytanie2.ExecuteNonQuery();

                SqlCommand zapytanie3 = new SqlCommand();
                zapytanie3.Connection = conn;
                zapytanie3.CommandType = CommandType.Text;
                zapytanie3.CommandText = "UPDATE Projekty SET aktualne_wydanie='" + akt_wyd + "' WHERE id='" + projekt_id + "';";
                zapytanie3.Transaction = trans;
                zapytanie3.ExecuteNonQuery();
                trans.Commit();

                wydanie_DropDownList.DataBind();
                wydanie2_DropDownList.DataBind();

                dodanoWydanie_Label.Text = "Dodano nowe wydanie do projektu.";
                dodanoWydanie_Label.Visible = true;
                dodanoIteracje_Label.Visible = false;
            }
            catch 
            {
                trans.Rollback();
                dodanoWydanie_Label.Text = "Wystąpił błąd";
                dodanoWydanie_Label.Visible = true;
                dodanoIteracje_Label.Visible = false;
            }
            finally
            {
                trans.Dispose();
            }
        }

        // Dodanie nowej iteracji do projektu
        protected void iteracja_Button_Click(object sender, EventArgs e)
        {
            string akt_wyd = wydanie2_DropDownList.SelectedValue;
            string cel = cel_TextBox.Text;
            int l_iteracji=0;
            int wyd_id=0;

            SqlTransaction trans = conn.BeginTransaction();
            SqlDataReader reader;

            try
            {
                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "UPDATE Wydania SET liczba_iteracji = liczba_iteracji + 1 WHERE nr_wydania = '" + akt_wyd + "' AND Projekty_id = '" + projekt_id + "';";
                zapytanie.Transaction = trans;
                zapytanie.ExecuteNonQuery();
                

                SqlCommand zapytanie2 = new SqlCommand();
                zapytanie2.Connection = conn;
                zapytanie2.CommandType = CommandType.Text;
                zapytanie2.CommandText = "SELECT id, liczba_iteracji FROM Wydania WHERE nr_wydania='" + akt_wyd + "' AND Projekty_id='" + projekt_id + "';";
                zapytanie2.Transaction = trans;
                reader = zapytanie2.ExecuteReader();
                try
                {
                    reader.Read();
                    wyd_id = reader.GetInt32(0);
                    l_iteracji = reader.GetInt32(1);
                    reader.Close();
                }
                catch
                {
                    reader.Dispose();
                }
                SqlCommand zapytanie3 = new SqlCommand();
                zapytanie3.Connection = conn;
                zapytanie3.CommandType = CommandType.Text;
                zapytanie3.CommandText = "INSERT INTO Iteracje (nr_iteracji, cel_iteracji, Wydania_id) VALUES ('" + l_iteracji + "','" + cel + "', '" + wyd_id + "');";
                zapytanie3.Transaction = trans;
                zapytanie3.ExecuteNonQuery();
                trans.Commit();

                iteracja_DropDownList.DataBind();
                dodanoIteracje_Label.Text = "Dodano nową iterację.";
                dodanoIteracje_Label.Visible = true;
                dodanoWydanie_Label.Visible = false;
                cel_TextBox.Text = "";
                cel_iteracji();
            }
            catch
            {                
                trans.Rollback();
                dodanoIteracje_Label.Text = "Wystąpił błąd.";
                dodanoIteracje_Label.Visible = true;
                dodanoWydanie_Label.Visible = false;
            }
            finally
            {
                trans.Dispose();
            }
        }

        // Przejście do rejestru zadaniowego dla wybranego z wydania i iteracji rejestru zadaniowego
        protected void zadaniowy_Button_Click(object sender, EventArgs e)
        {
            string wydanie = wydanie_DropDownList.SelectedValue;
            string iteracja = iteracja_DropDownList.SelectedValue;
            Session["wydanie"] = wydanie;
            Session["iteracja"] = iteracja;            
            Server.Transfer("RejestrZadaniowy.aspx");            
        }

        protected void wydanie_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            iteracja_DropDownList.DataBind();
            cel_iteracji();
        }

        protected void pliki_Button_Click(object sender, EventArgs e)
        {
            Server.Transfer("Pliki.aspx");
        }

        protected void svn_Button_Click(object sender, EventArgs e)
        {
            Server.Transfer("UstawieniaSVN.aspx");
        }

        protected void testy_Button_Click(object sender, EventArgs e)
        {
            Server.Transfer("PrzypadkiTestowe.aspx");
        }

        protected void cel_iteracji()
        {
            if (iteracja_DropDownList.Items.Count != 0)
            {
                string iteracja = iteracja_DropDownList.SelectedValue;
                string wydanie = wydanie_DropDownList.SelectedValue;

                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "SELECT i.cel_iteracji FROM Iteracje i, Wydania w WHERE w.Projekty_id = '" + projekt_id + "' AND w.nr_wydania = '" + wydanie + "' AND i.nr_iteracji ='" + iteracja + "' AND i.Wydania_id=w.id;";

                SqlDataReader reader;
                reader = zapytanie.ExecuteReader();
                try
                {
                    reader.Read();
                    celIt_Label.Text = reader.GetString(0);
                    reader.Close();
                }
                catch
                {
                    reader.Dispose();
                }

                cel_Label.Visible = true;
                celIt_Label.Visible = true;
            }
            else
            {
                cel_Label.Visible = false;
                celIt_Label.Visible = false;
            }
        }        

        protected void nieUsuwaj_Button_Click(object sender, EventArgs e)
        {
            Accordion1.SelectedIndex = 0;
        }

        protected void usunProjekt_Button_Click(object sender, EventArgs e)
        {
            SqlTransaction trans = conn.BeginTransaction();            

            try
            {                                
                SqlCommand zap = new SqlCommand();
                zap.Connection = conn;
                zap.Transaction = trans;
                zap.CommandType = CommandType.Text;
                zap.CommandText = "UPDATE Uzytkownicy SET aktywny_projekt=NULL, aktywne_wymaganie=NULL, aktywne_zadanie=NULL WHERE aktywny_projekt='" + projekt_id + "' ";
                zap.ExecuteNonQuery();
                zap.CommandText = "DELETE FROM Uzytkownicy_Projekty WHERE Projekt_id='" + projekt_id + "'";
                zap.ExecuteNonQuery();
                zap.CommandText = "DELETE FROM Pliki_wymagania WHERE Wymaganie_id IN (SELECT id FROM Wymagania WHERE Projekty_id='" + projekt_id + "')";
                zap.ExecuteNonQuery();
                zap.CommandText = "DELETE FROM Historia_plikow WHERE Pliki_id IN (SELECT id FROM Pliki WHERE Projekty_id='" + projekt_id + "')";
                zap.ExecuteNonQuery();
                zap.CommandText = "DELETE FROM Pliki WHERE Projekty_id='" + projekt_id + "'";
                zap.ExecuteNonQuery();
                zap.CommandText = "DELETE FROM Wykonanie_przypadku WHERE Przypadek_testowy_id IN (SELECT id FROM Przypadki_testowe WHERE Projekty_id='" + projekt_id + "')";
                zap.ExecuteNonQuery();
                zap.CommandText = "DELETE FROM Przypadki_testowe WHERE Projekty_id='" + projekt_id + "'";
                zap.ExecuteNonQuery();
                zap.CommandText = "DELETE FROM Powiazane_wymagania WHERE Wymaganie1_id IN (SELECT id FROM Wymagania WHERE Projekty_id='" + projekt_id + "')";
                zap.ExecuteNonQuery();
                zap.CommandText = "DELETE FROM Wersje_wymagan WHERE Wymaganie_id IN (SELECT id FROM Wymagania WHERE Projekty_id ='" + projekt_id + "')";
                zap.ExecuteNonQuery();
                zap.CommandText = "DELETE FROM Wymagania WHERE Projekty_id='" + projekt_id + "'";
                zap.ExecuteNonQuery();                
                zap.CommandText = "DELETE FROM Iteracje WHERE Wydania_id IN (SELECT id FROM Wydania WHERE Projekty_id='" + projekt_id + "')";
                zap.ExecuteNonQuery();
                zap.CommandText = "DELETE FROM Wydania WHERE Projekty_id='" + projekt_id + "'";
                zap.ExecuteNonQuery();
                zap.CommandText = "DELETE FROM Projekty WHERE id='" + projekt_id + "'";
                zap.ExecuteNonQuery();
                trans.Commit();                
            }
            catch
            {
                trans.Rollback();
            }            

            if (projekt_id == (int)Session["aktywny_projekt"])
                Session.Remove("aktywny_projekt");

            Server.Transfer("Default.aspx");
        }

    }
}