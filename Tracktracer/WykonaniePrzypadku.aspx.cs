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
    public partial class WykonaniePrzypadku1 : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;
        private int projekt_id;
        private string nazwa;
        private string status;
        private string przypadek_nazwa;
        private int wykonanie_id;
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
                przypadek_nazwa = (string)Session["przypadek_nazwa"];
                wykonanie_id = (int)Session["wykonanie_id"];
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
                powroty.Add("WykonaniePrzypadku.aspx");
                powroty_id.Add(wykonanie_id);
            }
            Session["powroty"] = powroty;
            Session["powroty_id"] = powroty_id;

            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "SELECT wyk.wynik, wyk.data, wyk.komentarz, u.imie, u.nazwisko, u.login FROM Wykonanie_przypadku wyk, Uzytkownicy u WHERE wyk.id='"+wykonanie_id+"' AND u.id = wyk.Uzytkownik_id";
            SqlDataReader reader = zapytanie.ExecuteReader();
            try
            {
                reader.Read();
                wynik_Label.Text += reader.GetString(0);
                data_Label.Text += reader.GetDateTime(1).ToString();
                komentarz_TextBox.Text += reader.GetString(2);
                tester_Label.Text += reader.GetString(3) + " " + reader.GetString(4) + " (login: " + reader.GetString(5) + " )";
                reader.Close();
            }
            catch 
            {
                reader.Dispose();
                Server.Transfer((string)Session["back"]);
            }

            projekt_Label.Text += nazwa;
            przypadek_Label.Text += przypadek_nazwa;
        }

        protected void powrot_Button_Click(object sender, EventArgs e)
        {
            if (powroty.Count > 1)
            {
                // zdjęcie elementów dodanych na tej stronie
                powroty.RemoveAt(powroty.Count - 1);
                powroty_id.RemoveAt(powroty_id.Count - 1);

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