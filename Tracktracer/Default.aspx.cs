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
    public partial class _Default : System.Web.UI.Page
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
            Session.Remove("powroty");
            Session.Remove("powroty_id");
            try
            {
                aktywny_projekt = (int)Session["aktywny_projekt"];

                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "SELECT p.nazwa FROM Projekty p WHERE p.id=@aktywny_projekt;";
                zapytanie.Parameters.AddWithValue("@aktywny_projekt", aktywny_projekt);
                string nazwa_proj = (string)zapytanie.ExecuteScalar();
                aktywny_projekt_Label.Text = "Twój aktywny projekt to: ";
                akt_projekt_Link.Text = nazwa_proj;
            }
            catch
            {                
            }                        
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int no = 0;
            
            // Przejście do strony projektu
            if (e.CommandName.CompareTo("Projekt") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                Server.Transfer("SzczegolyProjektu.aspx?id=" + no);
            } 

            // Ustawienie aktywnego projektu
            else if (e.CommandName.CompareTo("Aktywny") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                aktywny_projekt = no;
                Session["aktywny_projekt"] = aktywny_projekt;

                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "UPDATE Uzytkownicy SET aktywny_projekt=@aktywny_projekt, aktywne_wymaganie=NULL, aktywne_zadanie=NULL WHERE id=@user_id + ;";
                zapytanie.Parameters.AddWithValue("@aktywny_projekt", aktywny_projekt);
                zapytanie.Parameters.AddWithValue("@user_id", user_id);
                try
                {
                    zapytanie.ExecuteNonQuery();

                    zapytanie.CommandText = "SELECT p.nazwa FROM Projekty p WHERE p.id=@aktywny_projekt;";
                    zapytanie.Parameters.AddWithValue("@aktywny_projekt", aktywny_projekt);
                    string nazwa_proj = (string)zapytanie.ExecuteScalar();
                    aktywny_projekt_Label.Text = "Twój aktywny projekt to: ";
                    akt_projekt_Link.Text = nazwa_proj;
                }
                catch
                { }                
            }
        }

        protected void akt_projekt_Link_Click(object sender, EventArgs e)
        {
            Session["back"] = "Default.aspx";
            Server.Transfer("SzczegolyProjektu.aspx?id=" + aktywny_projekt);
        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            Server.Transfer("NowyProjekt.aspx");
        }
    }
}
