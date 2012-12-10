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
    public partial class Rejestruj : System.Web.UI.Page
    {
        private SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            Server.Transfer("Index.aspx");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=.\SQLSERVER;Initial Catalog=tracktracer; User ID=tracktracer; Integrated Security=True";
            

            string haslo = haslo_TextBox.Text;
            string imie = Imie_TextBox.Text;
            string nazwisko = Nazwisko_TextBox.Text;
            string login = login_TextBox.Text;

            if (login.Length == 0)
            {
                RequiredFieldValidator1.ErrorMessage = "Musisz podać login.";
                RequiredFieldValidator1.IsValid = false;
            }

            conn.Open();
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "INSERT INTO Uzytkownicy (login, haslo, imie, nazwisko, status_konta) VALUES (@login ,@haslo ,@imie ,@nazwisko ,'zablokowane');";
            zapytanie.Parameters.AddWithValue("@login", login);
            zapytanie.Parameters.AddWithValue("@haslo", haslo);
            zapytanie.Parameters.AddWithValue("@imie", imie);
            zapytanie.Parameters.AddWithValue("@nazwisko", nazwisko);
            


            try
            {
                zapytanie.ExecuteNonQuery();
                Server.Transfer("Index.aspx");
            }
            catch (SqlException ex)
            {
                RequiredFieldValidator1.ErrorMessage = "User istnieje";
                RequiredFieldValidator1.IsValid = false;
            }
            finally
            {
                conn.Close();
            }
        }

    }
}