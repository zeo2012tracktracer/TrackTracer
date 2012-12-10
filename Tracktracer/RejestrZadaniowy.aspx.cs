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
    public partial class RejestrZadaniowy : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;
        private int projekt_id;
        private string nazwa;
        private string wydanie;
        private string iteracja;
        private List<string> powroty;
        private List<int> powroty_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                user_id = (int)Session["user_id"];
                conn = (SqlConnection)Session["connection"];
                projekt_id = (int)Session["projekt_id"];
                nazwa = (string)Session["nazwa"];
                wydanie = (string)Session["wydanie"];
                iteracja = (string)Session["iteracja"];
            }
            catch
            {
                Server.Transfer("Index.aspx");
            }

            powroty = (List<string>)Session["powroty"];
            powroty_id = (List<int>)Session["powroty_id"];

            if (powroty == null)
            {
                powroty = new List<string>();
                powroty_id = new List<int>();
            }
            if (!IsPostBack)
            {
                powroty.Add("RejestrZadaniowy.aspx");
                powroty_id.Add(0);
            }
            Session["powroty"] = powroty;
            Session["powroty_id"] = powroty_id;

            nazwa_Label.Text = "Projekt: " + nazwa;
            rejestr_Label.Text = "Wydanie: " + wydanie + "   Iteracja: " + iteracja;
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int no = 0;
            if (e.CommandName.CompareTo("Wymaganie") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                Session["wymaganie_id"] = no.ToString();
                Session["back"] = "RejestrZadaniowy.aspx";                
                Server.Transfer("Wymaganie.aspx");
            }
            else if (e.CommandName.CompareTo("Pobierz") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "UPDATE Wymagania SET Uzytkownik_id =@user_id WHERE id =@no ";
                zapytanie.Parameters.AddWithValue("@user_id", user_id);
                zapytanie.Parameters.AddWithValue("@no", no);
                try
                {
                    zapytanie.ExecuteNonQuery();
                    GridView1.DataBind();
                    GridView2.DataBind();
                }
                catch { }
            }
            else if (e.CommandName.CompareTo("Oddaj") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "UPDATE Wymagania SET Uzytkownik_id = NULL WHERE id =@no";
                zapytanie.Parameters.AddWithValue("@no", no);
                try
                {
                    zapytanie.ExecuteNonQuery();
                    GridView1.DataBind();
                    GridView2.DataBind();
                }
                catch { }
            }
            
        }

        protected void powrot_Button_Click(object sender, EventArgs e)
        {
            Response.Redirect("SzczegolyProjektu.aspx?id=" + projekt_id);
        }
    }
}