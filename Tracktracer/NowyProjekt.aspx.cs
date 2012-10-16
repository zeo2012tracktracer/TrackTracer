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
    public partial class NowyProjekt : System.Web.UI.Page
    {
        private int aktywny_projekt = 0;
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
            try
            {
                aktywny_projekt = (int)Session["aktywny_projekt"];             
            }
            catch
            {
            }
        }

        protected void Anuluj_Button_Click(object sender, EventArgs e)
        {
            Server.Transfer("Default.aspx");
        }

        protected void Dodaj_Button_Click(object sender, EventArgs e)
        {
            String nazwa = Nazwa_TextBox.Text;
            String opis = Opis_TextBox.Text;
            String metodyka = Metodyka_DropDownList.SelectedValue;

            SqlCommand zapytanie = new SqlCommand();            
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "INSERT INTO Projekty (nazwa, opis, metodyka, wlasciciel) VALUES ('" + nazwa + "', '" + opis + "', '" + metodyka + "', '" + user_id + "');";

            SqlCommand zapytanie2 = new SqlCommand();
            zapytanie2.Connection = conn2;
            zapytanie2.CommandType = CommandType.Text;            
            
            try
            {
                zapytanie.ExecuteNonQuery();                

                zapytanie.CommandText = "SELECT id FROM Projekty WHERE wlasciciel='" + user_id + "' ORDER BY id DESC;";
                SqlDataReader reader = zapytanie.ExecuteReader();
                reader.Read();
                int proj_id = reader.GetInt32(0);
                reader.Close();

                string sql2 = "SET IDENTITY_INSERT projekty ON;";
                sql2 += "INSERT INTO projekty (id, nazwa, opis) VALUES ('" + proj_id + "','" + nazwa + "', '" + opis + "');";
                sql2 += "SET IDENTITY_INSERT projekty OFF;";
                zapytanie2.CommandText = sql2;
                zapytanie2.ExecuteNonQuery();

                zapytanie.CommandText = "INSERT INTO Uzytkownicy_Projekty (Uzytkownik_id, Projekt_id) VALUES ('" + user_id + "', '" + proj_id + "');";
                zapytanie.ExecuteNonQuery();                 
            }
            catch
            {
            }

            Server.Transfer("Default.aspx");
        }
    }
}