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
    public partial class ZmianaDanych : System.Web.UI.Page
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

                if (!IsPostBack)
                {
                    SqlCommand zapytanie = new SqlCommand();
                    zapytanie.Connection = conn;
                    zapytanie.CommandType = CommandType.Text;
                    zapytanie.CommandText = "SELECT imie, nazwisko FROM Uzytkownicy WHERE id ='" + user_id + "'";
                    SqlDataReader reader = zapytanie.ExecuteReader();
                    try
                    {
                        reader.Read();
                        imie_TextBox.Text = reader.GetString(0);
                        nazwisko_TextBox.Text = reader.GetString(1);
                        reader.Close();
                    }
                    catch
                    { 
                        reader.Dispose();
                    }
                }
            }
            catch
            {
                Server.Transfer("Index.aspx");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Server.Transfer("Administracja.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string imie = imie_TextBox.Text;
            string nazwisko = nazwisko_TextBox.Text;

            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "UPDATE Uzytkownicy SET imie='" + imie + "', nazwisko='" + nazwisko + "' WHERE id='" + user_id + "';";            

            SqlCommand zapytanie2 = new SqlCommand();
            zapytanie2.Connection = conn2;
            zapytanie2.CommandType = CommandType.Text;
            zapytanie2.CommandText = "UPDATE pracownicy SET imie='" + imie + "', nazwisko='" + nazwisko + "' WHERE id='" + user_id + "'";

            try
            {
                zapytanie.ExecuteNonQuery();
                zapytanie2.ExecuteNonQuery();
            }
            catch { }
            Server.Transfer("Administracja.aspx");            
        }
    }
}