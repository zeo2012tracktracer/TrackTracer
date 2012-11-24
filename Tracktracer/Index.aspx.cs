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
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String login = Login_textbox.Text;
            login = login.Replace("'", "");
            login = login.Replace("--", "");
            login = login.Replace(";", "");
            String haslo = Password_textbox.Text;
            haslo = haslo.Replace("'", "");
            haslo = haslo.Replace("--", "");
            haslo = haslo.Replace(";", "");
            zaloguj(login, haslo);
        }

        protected void zaloguj(String login, String haslo)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=.;Initial Catalog=tracktracer;User ID=user1;Password=zeo2012;Integrated Security=True";
            conn.Open();

            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "SELECT id, aktywny_projekt FROM Uzytkownicy WHERE login='"+login+"' AND haslo='"+haslo+"' AND status_konta='aktywne'";           
            
            SqlDataReader reader = zapytanie.ExecuteReader();

            if(reader.HasRows){
                reader.Read();
                int user_id = (int)reader.GetSqlInt32(0);
                              
                Session["user_id"] = user_id;
                Session["connection"] = conn;

                if (!reader.IsDBNull(1))
                {
                    int aktywny_projekt = (int)reader.GetSqlInt32(1);
                    Session["aktywny_projekt"] = aktywny_projekt;
                }
                else
                {
                    Session["aktywny_projekt"] = null;
                }

                reader.Close();
                Server.Transfer("Default.aspx");
            } else {
                reader.Close();
                conn.Close();
            }            
        }
    }
}