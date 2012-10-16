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
    public partial class PrzypisaniUzytkownicy : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;
        private int proj_id;
        private int wlasciciel;
        private string nazwa;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                user_id = (int)Session["user_id"];
                conn = (SqlConnection)Session["connection"];
                proj_id = (int)Session["projekt_id"];
                wlasciciel = (int)Session["wlasciciel"];
                nazwa = (string)Session["nazwa"];
            }
            catch
            {
                Server.Transfer("Index.aspx");
            }
            
            // Strona przeznaczona tylko dla właściciela projektu
            if (wlasciciel != user_id) Server.Transfer("Default.aspx");
                                   
            nazwa_Label.Text = nazwa;             
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string login = GridView1.SelectedRow.Cells[0].Text;

            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "DELETE FROM Uzytkownicy_Projekty WHERE Projekt_id = " + proj_id + " AND Uzytkownik_id IN ( SELECT id FROM Uzytkownicy WHERE login ='" + login + "');";

            try
            {
                zapytanie.ExecuteNonQuery();
            }
            catch
            { }

            SqlCommand zapytanie2 = new SqlCommand();
            zapytanie2.Connection = conn;
            zapytanie2.CommandType = CommandType.Text;
            zapytanie2.CommandText = "UPDATE Uzytkownicy SET aktywny_projekt = NULL WHERE login='" + login + "' AND aktywny_projekt=" + proj_id + ";";

            try
            {
                zapytanie2.ExecuteNonQuery();
            }
            catch { }

            GridView1.DataBind();
            GridView2.DataBind();
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string login = GridView2.SelectedRow.Cells[0].Text;

            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "INSERT INTO Uzytkownicy_Projekty (Uzytkownik_id, Projekt_id) SELECT u.id, p.id FROM Uzytkownicy u, Projekty p WHERE u.login ='" + login + "' AND p.id='" + proj_id + "';";

            try
            {
                zapytanie.ExecuteNonQuery();
            }
            catch
            { }

            GridView1.DataBind();
            GridView2.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Server.Transfer("SzczegolyProjektu.aspx?id="+proj_id);
        }
    }
}