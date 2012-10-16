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
        private SqlConnection conn2;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                user_id = (int)Session["user_id"];
                conn = (SqlConnection)Session["connection"];
                conn2 = (SqlConnection)Session["connection2"];
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
            int id;

            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "INSERT INTO Uzytkownicy (login, haslo, imie, nazwisko, status_konta) VALUES ('" + login + "', '" + haslo + "', '" + imie + "', '" + nazwisko + "', 'aktywne');";

            string sql2 = string.Empty;
            SqlCommand zapytanie2 = new SqlCommand();
            zapytanie2.Connection = conn2;
            zapytanie2.CommandType = CommandType.Text;            

            try
            {
                zapytanie.ExecuteNonQuery();
                zapytanie.CommandText = "SELECT id FROM Uzytkownicy WHERE login='" + login + "'";
                id = (int)zapytanie.ExecuteScalar();

                sql2 = "SET IDENTITY_INSERT pracownicy ON;";
                sql2 += "INSERT INTO pracownicy (id, imie, nazwisko, login, haslo) VALUES (" + id + ", '" + imie + "', '" + nazwisko + "', '" + login + "', '" + haslo + "');";
                sql2 += "SET IDENTITY_INSERT pracownicy OFF;";
                zapytanie2.CommandText = sql2;
                zapytanie2.ExecuteNonQuery();

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