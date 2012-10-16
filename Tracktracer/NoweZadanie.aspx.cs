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
    public partial class NoweZadanie : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;
        private SqlConnection conn2;
        private int akt_rewizja;
        private int projekt_id;
        private int historyjka_id;
        private List<string> powroty;
        private List<int> powroty_id;

        protected void Page_Load(object sender, EventArgs e)
        {            
            try
            {
                user_id = (int)Session["user_id"];
                conn = (SqlConnection)Session["connection"];
                conn2 = (SqlConnection)Session["connection2"];
                projekt_id = (int)Session["projekt_id"];
                historyjka_id = (int)Session["historyjka_id"];
            }
            catch
            {
                Server.Transfer("Index.aspx");
            }

            powroty = (List<string>)Session["powroty"];
            powroty_id = (List<int>)Session["powroty_id"];  

            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "SELECT p.rewizja FROM Projekty p WHERE p.id = '" + projekt_id + "'";
            try
            {
                akt_rewizja = (Int32)zapytanie.ExecuteScalar();                
            }
            catch 
            {
                akt_rewizja = 0;
            }
        
        }

        protected void zadanie_Button_Click(object sender, EventArgs e)
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
                        
            if (poprawne_dl == 1)
            {
                string nazwa_zadania = nazwa_TextBox.Text;
                string tresc = tresc_TextBox.Text;
                int zad_id;

                SqlDataReader reader;
                SqlTransaction trans = conn.BeginTransaction(IsolationLevel.Serializable);
                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "INSERT INTO Zadania_programistyczne (nazwa, status, nr_rewizji, Projekty_id, Historyjka_uzytkownika_id, Realizator1_id) VALUES ('" + nazwa_zadania + "','Aktywne','" + akt_rewizja + "','" + projekt_id + "', '" + historyjka_id + "', '" + user_id + "');";
                zapytanie.Transaction = trans;

                try
                {
                    zapytanie.ExecuteNonQuery();
                    
                    zapytanie.CommandText = "SELECT zp.id, wh.udzialowcy FROM Zadania_programistyczne zp, Wersje_historyjek wh, Historyjki_uzytkownikow hu WHERE zp.Historyjka_uzytkownika_id =hu.id AND hu.id ='" + historyjka_id + "' AND wh.Historyjka_uzytkownika_id = hu.id ORDER BY zp.id DESC;";
                    reader = zapytanie.ExecuteReader();
                    reader.Read();
                    zad_id = reader.GetInt32(0);
                    string udzialowcy = reader.GetString(1);
                    reader.Close();

                    zapytanie.CommandText = "INSERT INTO Wersje_zadan_programistycznych (tresc_Zadania, nr_rewizji, Zadanie_programistyczne_id, Uzytkownik_id) VALUES ('" + tresc + "', '" + akt_rewizja + "', '" + zad_id + "', '" + user_id + "');";
                    zapytanie.ExecuteNonQuery();

                    // Dodanie wpisów do bazy danych pluginu
                    SqlCommand zapytanie2 = new SqlCommand();
                    zapytanie2.Connection = conn2;
                    zapytanie2.CommandType = CommandType.Text;
                    try
                    {
                        string sql2 = "SET IDENTITY_INSERT wymagania ON;";
                        sql2 += "INSERT INTO wymagania (id, FK_id_projektu, FK_id_nadrzednego_wymagania) VALUES ('" + zad_id + "', '" + projekt_id + "', NULL);";
                        sql2 += "SET IDENTITY_INSERT wymagania OFF;";
                        zapytanie2.CommandText = sql2;
                        zapytanie2.ExecuteNonQuery();

                        sql2 = "SET IDENTITY_INSERT historiawymagan ON;";
                        sql2 += "INSERT INTO historiawymagan (id, nazwa, opis, zrodlo, priorytet, status, FK_id_wymagania, FK_id_pracownika) VALUES ('" + zad_id + "', '" + nazwa_zadania + "', '" + tresc + "', '" + udzialowcy + "', 'Normalny', 'Aktywne','" + zad_id + "', '" + user_id + "');";
                        sql2 += "SET IDENTITY_INSERT historiawymagan OFF;";
                        zapytanie2.CommandText = sql2;
                        zapytanie2.ExecuteNonQuery();
                    }
                    catch { }

                    trans.Commit();
                    info_Label.Text = "Dodano zadanie";
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
            }
        }

        protected void powrot_Button_Click(object sender, EventArgs e)
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