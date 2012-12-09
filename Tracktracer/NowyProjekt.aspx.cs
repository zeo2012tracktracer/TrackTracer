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
            zapytanie.CommandText = "INSERT INTO Projekty (nazwa, opis, metodyka, wlasciciel) VALUES (@nazwa , @opis , @metodyka , @user_id );";
            zapytanie.Parameters.AddWithValue("@nazwa", nazwa);
            zapytanie.Parameters.AddWithValue("@opis", opis);
            zapytanie.Parameters.AddWithValue("@metodyka", metodyka);
            zapytanie.Parameters.AddWithValue("@user_id", user_id);
            
            try
            {
                zapytanie.ExecuteNonQuery();                

                zapytanie.CommandText = "SELECT id FROM Projekty WHERE wlasciciel=@user_id ORDER BY id DESC;";
                zapytanie.Parameters.AddWithValue("@user_id", user_id);
            
                SqlDataReader reader = zapytanie.ExecuteReader();
                reader.Read();
                int proj_id = reader.GetInt32(0);
                reader.Close();

                zapytanie.CommandText = "INSERT INTO Uzytkownicy_Projekty (Uzytkownik_id, Projekt_id) VALUES (@user_id , @proj_id );";
                zapytanie.Parameters.AddWithValue("@user_id", user_id);
                zapytanie.Parameters.AddWithValue("@proj_id", proj_id);
            
                zapytanie.ExecuteNonQuery();                 
            }
            catch
            {
            }

            Server.Transfer("Default.aspx");
        }
    }
}