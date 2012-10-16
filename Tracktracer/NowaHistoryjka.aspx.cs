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
    public partial class NowaHistoryjka : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;
        private int akt_rewizja;
        private int projekt_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                user_id = (int)Session["user_id"];
                conn = (SqlConnection)Session["connection"];
                projekt_id = (int)Session["projekt_id"];
            }
            catch
            {
                Server.Transfer("Index.aspx");
            }

            try
            {
                akt_rewizja = (int)Session["akt_rewizja"];
            }
            catch
            {
                akt_rewizja = 0;
            }
        }

        protected void historyjka_Button_Click(object sender, EventArgs e)
        {
            int poprawne_dl = 1;
            if (tresc_TextBox.Text.Length > 500)
            {
                trescR_Label.Visible = true;
                poprawne_dl = 0;
            }
            else
            {
                trescR_Label.Visible = false;
            }

            if (uwagi_TextBox.Text.Length > 255)
            {
                uwagiR_Label.Visible = true;
                poprawne_dl = 0;
            }
            else
            {
                uwagiR_Label.Visible = false;
            }

            if (udzialowcy_TextBox.Text.Length > 255)
            {
                udzialowcyR_Label.Visible = true;
                poprawne_dl = 0;
            }
            else
            {
                udzialowcyR_Label.Visible = false;
            }

            if (poprawne_dl == 1)
            {
                string nazwa = nazwa_TextBox.Text;
                string tresc = tresc_TextBox.Text;
                string uwagi = uwagi_TextBox.Text;
                string udzialowcy = udzialowcy_TextBox.Text;
                Int32 historyjka_id;

                SqlTransaction trans = conn.BeginTransaction(IsolationLevel.Serializable);

                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "INSERT INTO Historyjki_uzytkownikow (nazwa, status, nr_rewizji, Projekty_id) VALUES ('" + nazwa + "','Aktywna','" + akt_rewizja + "','" + projekt_id + "');";
                zapytanie.Transaction = trans;

                try
                {
                    zapytanie.ExecuteNonQuery();

                    zapytanie.CommandType = CommandType.Text;
                    zapytanie.CommandText = "SELECT hu.id FROM Historyjki_uzytkownikow hu WHERE hu.Projekty_id ='" + projekt_id + "' ORDER BY hu.id DESC;";
                    historyjka_id = (Int32)zapytanie.ExecuteScalar();                    

                    zapytanie.CommandText = "INSERT INTO Wersje_historyjek (Historyjka_uzytkownika_id, tresc, uwagi, udzialowcy, Uzytkownik_id, nr_rewizji) VALUES ('" + historyjka_id + "', '" + tresc + "','" + uwagi + "', '" + udzialowcy + "', '" + user_id + "', '" + akt_rewizja + "');";
                    zapytanie.ExecuteNonQuery();

                    trans.Commit();
                    info_Label.Text = "Dodano historyjkę użytkownika.";
                    info_Label.Visible = true;
                }
                catch
                {
                    trans.Rollback();
                    info_Label.Text = "Wystąpił błąd";
                    info_Label.Visible = true;
                }
                finally
                {
                    trans.Dispose();
                }

                nazwa_TextBox.Text = "";
                tresc_TextBox.Text = "";
                uwagi_TextBox.Text = "";
                udzialowcy_TextBox.Text = "";
            }
        }

        protected void anuluj_Button_Click(object sender, EventArgs e)
        {
            Server.Transfer("HistoryjkiUzytkownika.aspx");
        }
    }
}