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
    public partial class NowyUzytkownik : System.Web.UI.Page
    {        
        private int user_id;
        private SqlConnection conn;
        
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

            if (user_id != 1) Server.Transfer("Index.aspx");            

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string login = login_TextBox.Text;
            string haslo = haslo_TextBox.Text;
            string imie = Imie_TextBox.Text;
            string nazwisko = Nazwisko_TextBox.Text;

            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "INSERT INTO Uzytkownicy (login, haslo, imie, nazwisko, status_konta) VALUES ('" + login + "', '" + haslo + "', '" + imie + "', '" + nazwisko + "', 'aktywne');";           

            try
            {
                zapytanie.ExecuteNonQuery();
                Server.Transfer("ZarzadzanieUzytkownikami.aspx");
            }
            catch 
            {
                nowyUzytkownik_Label.Text = "Wybrany login jest już zajęty";
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Server.Transfer("ZarzadzanieUzytkownikami.aspx");
        }
    }
}