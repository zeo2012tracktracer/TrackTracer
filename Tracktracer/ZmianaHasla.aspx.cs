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
    public partial class ZmianaHasla : System.Web.UI.Page
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
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Server.Transfer("Administracja.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string haslo = pass_TextBox.Text;

            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "UPDATE Uzytkownicy SET haslo=@haslo WHERE id=@user_id ;";
            zapytanie.Parameters.AddWithValue("@haslo", haslo);
            zapytanie.Parameters.AddWithValue("@user_id", user_id);
            
            try
            {
                zapytanie.ExecuteNonQuery();
            }
            catch { }
            Server.Transfer("Administracja.aspx");
        }
    }
}