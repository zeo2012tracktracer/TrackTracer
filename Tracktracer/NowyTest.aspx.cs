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
    public partial class NowyTest : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;
        private int projekt_id;
        private string nazwa;
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
            }
            catch
            {
                Server.Transfer("Index.aspx");
            }

            powroty = (List<string>)Session["powroty"];
            powroty_id = (List<int>)Session["powroty_id"];            

            projekt_Label.Text = "Projekt: " + nazwa;
        }

        protected void dodaj_Button_Click(object sender, EventArgs e)
        {
            if (opis_TextBox.Text.Length > 500)
            {
                opisR_Label.Visible = true;
            }
            else
            {
                opisR_Label.Visible = false;
                string nazwa_PT = nazwa_TextBox.Text;
                string opis_PT = opis_TextBox.Text;

                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                if (((string)Session["back"]).CompareTo("Wymaganie.aspx") == 0)
                {
                    zapytanie.CommandText = "INSERT INTO Przypadki_testowe (nazwa, opis, Uzytkownik_id, Wymaganie_id, Projekty_id) VALUES (@nazwa_PT , @opis_PT , @user_id , @stringSessionWymaganie , @projekt_id )";
                    zapytanie.Parameters.AddWithValue("@nazwa_PT", nazwa_PT);
                    zapytanie.Parameters.AddWithValue("@opis_PT", opis_PT);
                    zapytanie.Parameters.AddWithValue("@user_id", user_id);
                    zapytanie.Parameters.AddWithValue("@projekt_id", projekt_id);
                    zapytanie.Parameters.AddWithValue("@stringSessionWymaganie", (string)Session["wymaganie_id"]);
                }
                else
                {
                    zapytanie.CommandText = "INSERT INTO Przypadki_testowe (nazwa, opis, Uzytkownik_id, Zadanie_programistyczne_id, Projekty_id) VALUES (@nazwa_PT , @opis_PT , @user_id ,@stringSessionZadanie , @projekt_id )";
                    zapytanie.Parameters.AddWithValue("@nazwa_PT", nazwa_PT);
                    zapytanie.Parameters.AddWithValue("@opis_PT", opis_PT);
                    zapytanie.Parameters.AddWithValue("@user_id", user_id);
                    zapytanie.Parameters.AddWithValue("@projekt_id", projekt_id);
                    zapytanie.Parameters.AddWithValue("@stringSessionZadanie", (string)Session["zadanie_id"]);
                }

                try
                {
                    zapytanie.ExecuteNonQuery();
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Niepowodzenie: dodawanie przypadku testowego");
                }

                powrot();
            }
        }

        protected void powrot_Button_Click(object sender, EventArgs e)
        {
            powrot();
        }

        protected void powrot()
        {
            if (powroty.Count > 1)
            {                
                string strona = powroty[powroty.Count - 1];
                int elem_id = powroty_id[powroty_id.Count - 1];
                powroty.RemoveAt(powroty.Count - 1);
                powroty_id.RemoveAt(powroty_id.Count - 1);

                if (strona.CompareTo("Wymaganie.aspx") == 0)
                {
                    Session["wymaganie_id"] = elem_id.ToString();
                }
                else if (strona.CompareTo("Plik.aspx") == 0)
                {
                    Session["id_pliku"] = elem_id;
                }
                else if (strona.CompareTo("PrzypadekTestowy.aspx") == 0)
                {
                    Session["przypadek_id"] = elem_id;
                }
                else if (strona.CompareTo("WykonaniePrzypadku.aspx") == 0)
                {
                    Session["wykonanie_id"] = elem_id;
                }
                else if (strona.CompareTo("ZadanieProgramistyczne.aspx") == 0)
                {
                    Session["zadanie_id"] = elem_id.ToString();
                }
                else if (strona.CompareTo("Historyjka.aspx") == 0)
                {
                    Session["historyjka_id"] = elem_id;
                }

                Session["powroty"] = powroty;
                Session["powroty_id"] = powroty_id;

                Server.Transfer(strona);
            }
        }
    }
}