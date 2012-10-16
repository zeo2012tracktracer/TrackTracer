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
    public partial class AktualneXP : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;
        private int aktywny_projekt;
        private int aktywne_zadanie;
        private string metodyka;
        private string nazwa;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                user_id = (int)Session["user_id"];
                conn = (SqlConnection)Session["connection"];
                aktywny_projekt = (int)Session["aktywny_projekt"];
                nazwa = (string)Session["nazwa"];
            }
            catch
            {
                Server.Transfer("Default.aspx");
            }
                                        
            projekt_Link.Text = nazwa;

            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "SELECT z.id, z.nazwa FROM Uzytkownicy u, Zadania_programistyczne z WHERE u.id='" + user_id + "' AND z.id = u.aktywne_zadanie;";
            SqlDataReader reader = zapytanie.ExecuteReader();
            try
            {
                reader.Read();
                aktywne_zadanie = reader.GetInt32(0);
                zadanie_Link.Text = reader.GetString(1);
                brak_zadania.Visible = false;
                reader.Close();
            }
            catch
            {
                reader.Dispose();
                brak_zadania.Visible = true;
            }
        }

        // Przejście do aktualnego projektu
        protected void projekt_Link_Click(object sender, EventArgs e)
        {
            Session["back"] = "AktualneXP.aspx";
            Response.Redirect("SzczegolyProjektu.aspx?id=" + aktywny_projekt);
        }

        // Przejście do strony realizowanego zadania
        protected void zadanie_Link_Click(object sender, EventArgs e)
        {
            Session["back"] = "AktualneXP.aspx";
            Session["projekt_id"] = aktywny_projekt;
            Session["zadanie_id"] = aktywne_zadanie.ToString();
            Server.Transfer("ZadanieProgramistyczne.aspx");
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int no = 0;
            if (e.CommandName.CompareTo("Zadanie") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                Session["zadanie_id"] = no.ToString();
                Session["back"] = "AktualneXP.aspx";
                Session["projekt_id"] = aktywny_projekt;
                Server.Transfer("ZadanieProgramistyczne.aspx");
            }
            else if (e.CommandName.CompareTo("Realizuj") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);

                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "UPDATE Uzytkownicy SET aktywne_zadanie ='" + no + "' WHERE id='" + user_id + "'";
                try
                {
                    zapytanie.ExecuteNonQuery();
                }
                catch { }

                zapytanie.CommandText = "SELECT z.id, z.nazwa FROM Uzytkownicy u, Zadania_programistyczne z WHERE u.id='" + user_id + "' AND z.id = u.aktywne_zadanie;";
                SqlDataReader reader = zapytanie.ExecuteReader();
                try
                {
                    reader.Read();
                    aktywne_zadanie = reader.GetInt32(0);
                    zadanie_Link.Text = reader.GetString(1);
                    brak_zadania.Visible = false;
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
                Session["back"] = "AktualneXP.aspx";
                Session["projekt_id"] = aktywny_projekt;
                Session["id_pliku"] = no;
                Server.Transfer("Plik.aspx");
            }
        }

    }
}