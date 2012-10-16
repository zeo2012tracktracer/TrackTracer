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
    public partial class NoweWymaganie : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;
        private SqlConnection conn2;
        private int akt_rewizja;
        private int projekt_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                user_id = (int)Session["user_id"];
                conn = (SqlConnection)Session["connection"];
                conn2 = (SqlConnection)Session["connection2"];
                projekt_id = (int)Session["projekt_id"];
            }
            catch
            {
                Server.Transfer("Index.aspx");
            }

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

        protected void wymaganie_Button_Click(object sender, EventArgs e)
        {
            int poprawne_dl = 1;
            if (opis_TextBox.Text.Length > 500) 
            {
                opisR_Label.Visible = true;
                poprawne_dl = 0;
            } else 
            {
                opisR_Label.Visible = false;
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
                string opis = opis_TextBox.Text;
                string uwagi = uwagi_TextBox.Text;
                string udzialowcy = udzialowcy_TextBox.Text;
                int wym_id;

                SqlDataReader reader;

                SqlTransaction trans = conn.BeginTransaction(IsolationLevel.Serializable);

                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "INSERT INTO Wymagania (nazwa, status, nr_rewizji, Projekty_id) VALUES ('" + nazwa + "','aktywne','" + akt_rewizja + "','" + projekt_id + "');";
                zapytanie.Transaction = trans;
                
                try
                {                    
                    zapytanie.ExecuteNonQuery();

                    zapytanie.CommandType = CommandType.Text;
                    zapytanie.CommandText = "SELECT w.id FROM Wymagania w WHERE w.Projekty_id ='" + projekt_id + "' ORDER BY w.id DESC;";
                    reader = zapytanie.ExecuteReader();
                    reader.Read();
                    wym_id = reader.GetInt32(0);
                    reader.Close();
                                        
                    zapytanie.CommandText = "INSERT INTO Wersje_wymagan (Wymaganie_id, opis, uwagi, udzialowcy, Uzytkownik_id, nr_rewizji) VALUES ('" + wym_id + "', '" + opis + "','" + uwagi + "', '" + udzialowcy + "', '" + user_id + "', '" + akt_rewizja + "');";                    
                    zapytanie.ExecuteNonQuery();
                                                            
                    // Dodanie wpisów do bazy danych pluginu
                    SqlCommand zapytanie2 = new SqlCommand();
                    zapytanie2.Connection = conn2;
                    zapytanie2.CommandType = CommandType.Text;
                    
                        string sql2 = "SET IDENTITY_INSERT wymagania ON;";
                        sql2 += "INSERT INTO wymagania (id, FK_id_projektu, FK_id_nadrzednego_wymagania) VALUES ('" + wym_id + "', '" + projekt_id + "', NULL);";
                        sql2 += "SET IDENTITY_INSERT wymagania OFF;";
                        zapytanie2.CommandText = sql2;
                        zapytanie2.ExecuteNonQuery();

                        sql2 = "SET IDENTITY_INSERT historiawymagan ON;";
                        sql2 += "INSERT INTO historiawymagan (id, nazwa, opis, zrodlo, priorytet, status, FK_id_wymagania, FK_id_pracownika) VALUES ('" + wym_id + "', '" + nazwa + "', '" + opis + "', '" + udzialowcy + "', 'Normalny', 'Aktywne','" + wym_id + "', '" + user_id + "');";
                        sql2 += "SET IDENTITY_INSERT historiawymagan OFF;";
                        zapytanie2.CommandText = sql2;
                        zapytanie2.ExecuteNonQuery();
                    
                    trans.Commit();

                    info_Label.Text = "Dodano wymaganie";
                    info_Label.Visible = true;
                    anuluj_Button.Text = "Powrót";
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
                opis_TextBox.Text = "";
                uwagi_TextBox.Text = "";
                udzialowcy_TextBox.Text = "";
            }
        }

        protected void opis_TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        protected void anuluj_Button_Click(object sender, EventArgs e)
        {
            Server.Transfer("RejestrProduktowy.aspx");
        }
  
    }
}