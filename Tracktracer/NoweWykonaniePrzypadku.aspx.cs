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
    public partial class WykonaniePrzypadku : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;
        private int projekt_id;
        private string nazwa;
        private int przypadek_id;
        private string przypadek_nazwa;
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
                przypadek_id = (int)Session["przypadek_id"];
                przypadek_nazwa = (string)Session["przypadek_nazwa"];
            }
            catch
            {
                Server.Transfer("Index.aspx");
            }

            powroty = (List<string>)Session["powroty"];
            powroty_id = (List<int>)Session["powroty_id"];   

            projekt_Label.Text = "Projekt: " + nazwa;
            przypadek_Label.Text = "Przypadek testowy: " + przypadek_nazwa;

        }

        protected void anuluj_Button_Click(object sender, EventArgs e)
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

        protected void wykonanie_Button_Click(object sender, EventArgs e)
        {
            if (komentarz_TextBox.Text.Length > 255)
            {
                komentarzR_Label.Visible = true;
            }
            else
            {
                string wynik = wynik_DropDownList.SelectedValue.ToString();
                string komentarz = komentarz_TextBox.Text;

                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "INSERT INTO Wykonanie_przypadku (wynik, komentarz, Przypadek_testowy_id, Uzytkownik_id) VALUES ('" + wynik + "', '" + komentarz + "', '" + przypadek_id + "', '" + user_id + "')";
                try
                {
                    zapytanie.ExecuteNonQuery();
                    zapytanie.CommandText = "UPDATE Przypadki_testowe SET status='" + wynik + "' WHERE id = '" + przypadek_id + "'";
                    zapytanie.ExecuteNonQuery();
                }
                catch { }            

                Server.Transfer("PrzypadekTestowy.aspx");
            }
        }
    }
}