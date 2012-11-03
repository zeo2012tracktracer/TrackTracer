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
    public partial class EdycjaUzytkownika : System.Web.UI.Page
    {
        private int user_id;
        private string mod_user;
        private SqlConnection conn;
        private string mod_imie;
        private string mod_nazwisko;
        private string mod_status;
        private string mod_id;
        private string mod_haslo;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                user_id = (int)Session["user_id"];
                conn = (SqlConnection)Session["connection"];
                mod_user = (string)Session["mod_user"];
                mod_imie = (string)Session["mod_imie"];
                mod_nazwisko = (string)Session["mod_nazwisko"];
                mod_status = (string)Session["mod_status"];
            }
            catch
            {
                Server.Transfer("Index.aspx");
            }
            if (user_id != 1) Server.Transfer("Index.aspx");
            
            if (!IsPostBack)
            {
                login_TextBox.Text = mod_user;
                imie_TextBox.Text = mod_imie;
                nazwisko_TextBox.Text = mod_nazwisko;
            }

            if (mod_status.CompareTo("aktywne") == 0)
            {
                status_Label.Text = "Konto aktywne. ";
                blokuj_Button.Text = "Zablokuj konto";
            }
            else
            {
                status_Label.Text = "Konto zablokowane. ";
                blokuj_Button.Text = "Przywróć konto";
            }
        }

        // Aktualizacja danych użytkownika
        protected void Button1_Click(object sender, EventArgs e)
        {
            kom_Label.Visible = false;
            string login = login_TextBox.Text;            
            string imie = imie_TextBox.Text;
            string nazwisko = nazwisko_TextBox.Text;
            string sql = "UPDATE Uzytkownicy SET ";
            int pierwszy = 1;            

            if (login.CompareTo(mod_user) != 0)
            {
                sql += "login='";
                sql += login;
                sql += "'";
                pierwszy = 0;                                
            }

            if (imie.CompareTo(mod_imie) != 0)
            {
                if (pierwszy != 1) sql += ", ";
                sql += "imie='";
                sql += imie;
                sql += "'";
                pierwszy = 0;
            }

            if (nazwisko.CompareTo(mod_nazwisko) != 0)
            {
                if (pierwszy != 1) sql += ", ";
                sql += "nazwisko='";
                sql += nazwisko;
                sql += "'";
                pierwszy = 0;
            }

            sql += " WHERE login='" + mod_user + "';";

            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = sql;

            kom_Label.Text = "Zaktualizowano dane.";

            try
            {
                zapytanie.ExecuteNonQuery();
                Session["mod_user"] = login;
            }
            catch
            {
                kom_Label.Text = "Wybierz inny login.";                                
            }
            
            kom_Label.Visible = true;            
        }

        // Zablokowanie bądź przywrócenie konta użytkownika
        protected void blokuj_Button_Click(object sender, EventArgs e)
        {
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;           


            if (mod_status.CompareTo("aktywne") == 0)
            {
                zapytanie.CommandText = "UPDATE Uzytkownicy SET status_konta='zablokowane' WHERE login='" + mod_user + "';";
            }
            else
            {

                zapytanie.CommandText = "UPDATE Uzytkownicy SET status_konta='aktywne' WHERE login='" + mod_user + "';";
            }

            try
            {
                zapytanie.ExecuteNonQuery();
                
                if (mod_status.CompareTo("aktywne") == 0)
                {
                    status_Label.Text = "Konto zablokowane. ";
                    mod_status = "zablokowane";                    
                    blokuj_Button.Text = "Przywróć konto";
                    Session["mod_status"] = mod_status;
                }
                else
                {
                    status_Label.Text = "Konto aktywne. ";
                    mod_status = "aktywne";
                    blokuj_Button.Text = "Zablokuj konto";
                    Session["mod_status"] = mod_status;
                }
            }
            catch
            {}
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Server.Transfer("ZarzadzanieUzytkownikami.aspx");
        }

        protected void password_Button_Click(object sender, EventArgs e)
        {
            string new_pass = pass_TextBox.Text;
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;

            zapytanie.CommandText = "UPDATE Uzytkownicy SET haslo='" + new_pass + "' WHERE login='" + mod_user + "';";
            haslo_Label.Text = "Hasło zostało zmienione";
            haslo_Label.Visible = true;            
            try
            {
                zapytanie.ExecuteNonQuery();
            }
            catch 
            {
                haslo_Label.Text = "Wprowadzone hasło jest niepoprawne.";
            }
        }
    }
}