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
    public partial class UstawieniaSVN : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;
        private int projekt_id;
        private int wlasciciel;
        private string nazwa;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                user_id = (int)Session["user_id"];
                conn = (SqlConnection)Session["connection"];
                projekt_id = (int)Session["projekt_id"];
                wlasciciel = (int)Session["wlasciciel"];
                nazwa = (string)Session["nazwa"];
            }
            catch
            {
                Server.Transfer("Index.aspx");
            }

            // Strona przeznaczona tylko dla właściciela projektu
            if (wlasciciel != user_id) Server.Transfer("Default.aspx");

            if (!IsPostBack)
            {
                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "SELECT p.svn_url, p.svn_user FROM Projekty p WHERE p.id ='" + projekt_id + "';";

                SqlDataReader reader = zapytanie.ExecuteReader();
                try
                {
                    reader.Read();
                    url_TextBox.Text = reader.GetString(0);
                    username_TextBox.Text = reader.GetString(1);                    
                    reader.Close();
                }
                catch
                {
                    reader.Dispose();
                }
            }
        }

        protected void aktualizuj_Button_Click(object sender, EventArgs e)
        {
            string url = url_TextBox.Text;            
            string username = username_TextBox.Text;
            string pass = pass_TextBox.Text;
                        
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "UPDATE Projekty SET svn_url='" + url + "', svn_user='" + username + "', svn_pass='" + pass + "' WHERE id='" + projekt_id + "';";
            zapytanie.ExecuteNonQuery();            
        }

        protected void powrot_Button_Click(object sender, EventArgs e)
        {
            Server.Transfer("SzczegolyProjektu.aspx?id=" + projekt_id);
        }
    }
}