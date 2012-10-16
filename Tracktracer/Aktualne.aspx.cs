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
    public partial class Aktualne : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;        
        private int aktywny_projekt;
        private int aktywne_wymaganie;
        private string metodyka;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                user_id = (int)Session["user_id"];
                conn = (SqlConnection)Session["connection"];
                aktywny_projekt = (int)Session["aktywny_projekt"];
            }
            catch
            {
                Server.Transfer("Default.aspx");
            }                        

            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "SELECT p.nazwa, p.metodyka FROM Projekty p WHERE p.id='" + aktywny_projekt + "';";
            SqlDataReader reader = zapytanie.ExecuteReader();
            try
            {
                reader.Read();
                string nazwa_proj = reader.GetString(0);
                projekt_Link.Text = nazwa_proj;
                Session["nazwa"] = nazwa_proj;
                metodyka = reader.GetString(1);
                reader.Close();
            }
            catch 
            {
                reader.Dispose();
            }

            if (metodyka.CompareTo("XP") == 0)
            {
                Session["metodyka"] = "XP";
                Server.Transfer("AktualneXP.aspx");
            }
            Session["metodyka"] = "Scrum";

            zapytanie.CommandText = "SELECT w.id, w.nazwa FROM Uzytkownicy u, Wymagania w WHERE u.id='" + user_id + "' AND w.id = u.aktywne_wymaganie;";
            reader = zapytanie.ExecuteReader();
            try
            {
                reader.Read();
                aktywne_wymaganie = reader.GetInt32(0);
                wymaganie_Link.Text = reader.GetString(1);
                brak_wymagania.Visible = false;
                reader.Close();
            }
            catch 
            {
                reader.Dispose();
                brak_wymagania.Visible = true;
            }

        }

        // Przejście do aktualnego projektu
        protected void projekt_Link_Click(object sender, EventArgs e)
        {
            Session["back"] = "Aktualne.aspx";
            Response.Redirect("SzczegolyProjektu.aspx?id=" + aktywny_projekt);
        }

        // Przejście do strony realizowanego wymagania
        protected void wymaganie_Link_Click(object sender, EventArgs e)
        {
            Session["back"] = "Aktualne.aspx";
            Session["projekt_id"] = aktywny_projekt;
            Session["wymaganie_id"] = aktywne_wymaganie.ToString();            
            Server.Transfer("Wymaganie.aspx");            
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int no = 0;
            if (e.CommandName.CompareTo("Wymaganie") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                Session["wymaganie_id"] = no.ToString();
                Session["back"] = "Aktualne.aspx";
                Session["projekt_id"] = aktywny_projekt;
                Server.Transfer("Wymaganie.aspx");
            }
            else if (e.CommandName.CompareTo("Realizuj") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);

                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "UPDATE Uzytkownicy SET aktywne_wymaganie ='" + no + "' WHERE id='" + user_id + "'";
                try
                {
                    zapytanie.ExecuteNonQuery();
                }
                catch { }

                zapytanie.CommandText = "SELECT w.id, w.nazwa FROM Uzytkownicy u, Wymagania w WHERE u.id='" + user_id + "' AND w.id = u.aktywne_wymaganie;";
                SqlDataReader reader = zapytanie.ExecuteReader();
                try
                {
                    reader.Read();
                    aktywne_wymaganie = reader.GetInt32(0);
                    wymaganie_Link.Text = reader.GetString(1);
                    brak_wymagania.Visible = false;
                    reader.Close();
                }
                catch
                {
                    reader.Dispose();                    
                }
            }
            else if (e.CommandName.CompareTo("Plik") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);               
                Session["back"] = "Aktualne.aspx";
                Session["projekt_id"] = aktywny_projekt;                                
                Session["id_pliku"] = no;                                
                Server.Transfer("Plik.aspx");
            }
        }
    }
}