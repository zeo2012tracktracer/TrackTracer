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
    public partial class PrzypadekTestowy : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;
        private int projekt_id;
        private string nazwa;
        private int przypadek_id;
        private string status;
        private string metodyka;
        private int przedmiot_id;
        private string przypadek_nazwa;
        private string opis;
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
                powroty.Add("PrzypadekTestowy.aspx");
                powroty_id.Add(przypadek_id);
            }
            Session["powroty"] = powroty;
            Session["powroty_id"] = powroty_id;
            
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "SELECT pt.nazwa, pt.status, pt.opis, pt.Wymaganie_id, pt.Zadanie_programistyczne_id, u.imie, u.nazwisko, u.login FROM Przypadki_testowe pt, Uzytkownicy u WHERE pt.id='" + przypadek_id + "' AND u.id = pt.Uzytkownik_id";
            SqlDataReader reader = zapytanie.ExecuteReader();
            try
            {
                reader.Read();
                przypadek_nazwa = reader.GetString(0);
                Session["przypadek_nazwa"] = przypadek_nazwa;
                przypadek_Label.Text = przypadek_nazwa;
                status = reader.GetString(1);
                opis = reader.GetString(2);                
                string sql;
                if (reader.IsDBNull(3))
                {
                    metodyka = "XP";
                    przedmiot_id = reader.GetInt32(4);
                    sql = "SELECT nazwa FROM Zadania_programistyczne WHERE id = '" + przedmiot_id + "'";
                }
                else
                {
                    metodyka = "Scrum";
                    przedmiot_id = reader.GetInt32(3);
                    sql = "SELECT nazwa FROM Wymagania WHERE id = '" + przedmiot_id + "'";
                }
                autor_Label.Text = reader.GetString(5) + " " + reader.GetString(6) + " (login: " + reader.GetString(7) + ")";
                
                reader.Close();

                zapytanie.CommandText = sql;
                if (metodyka.CompareTo("Scrum") == 0)
                {
                    przedmiot_Label.Text = "Testowane wymaganie: ";
                    przedmiot_Link.Text = (string)zapytanie.ExecuteScalar();
                }
                else
                {
                    przedmiot_Label.Text = "Testowane zadanie programistyczne: ";
                    przedmiot_Link.Text = (string)zapytanie.ExecuteScalar();
                }
            }
            catch 
            {
                reader.Dispose();
                Server.Transfer((string)Session["back"]);
            }

            projekt_Label.Text = nazwa;

            if (!IsPostBack)
            {
                opis_TextBox.Text = opis;
                if (status.CompareTo("Zaliczony") == 0)
                {
                    status_DropDownList.SelectedIndex = 0;
                }
                else if (status.CompareTo("Nie zaliczony") == 0)
                {
                    status_DropDownList.SelectedIndex = 1;
                }
                else
                {
                    status_DropDownList.Items[2].Enabled = true;
                    status_DropDownList.SelectedIndex = 2;
                }                
            }            
        }

        protected void status_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (status_DropDownList.SelectedValue.CompareTo(status) == 0)
            {
                status_Button.Visible = false;
            }
            else
            {
                status_Button.Visible = true;
            }
        }

        protected void status_Button_Click(object sender, EventArgs e)
        {
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "UPDATE Przypadki_testowe SET status = '" + status_DropDownList.SelectedValue.ToString() + "' WHERE id = '" + przypadek_id + "'";
            try
            {
                zapytanie.ExecuteNonQuery();
                status = status_DropDownList.SelectedValue.ToString();
                status_Button.Visible = false;
                status_DropDownList.Items[2].Enabled = false;
            }
            catch { }
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

        protected void przedmiot_Link_Click(object sender, EventArgs e)
        {
            if (metodyka.CompareTo("Scrum") == 0)
            {
                Session["back"] = "PrzypadekTestowy.aspx";
                Session["wymaganie_id"] = przedmiot_id.ToString();
                Server.Transfer("Wymaganie.aspx");
            }
            else
            {
                Session["back"] = "PrzypadekTestowy.aspx";
                Session["zadanie_id"] = przedmiot_id.ToString();
                Server.Transfer("ZadanieProgramistyczne.aspx");
            }
        }

        protected void wykonanie_Button_Click(object sender, EventArgs e)
        {
            Server.Transfer("NoweWykonaniePrzypadku.aspx");
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int no = 0;
            if (e.CommandName.CompareTo("Wykonanie") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                Session["wykonanie_id"] = no;
                Session["back"] = "PrzypadekTestowy.aspx";
                Server.Transfer("WykonaniePrzypadku.aspx");
            }
        }

        protected void zmiana_Button_Click(object sender, EventArgs e)
        {
            if (opis.CompareTo(opis_TextBox.Text) != 0)
            {
                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "UPDATE Przypadki_testowe SET opis = '" + opis_TextBox.Text + "', status = 'Do weryfikacji' WHERE id = '" + przypadek_id + "'";
                try
                {
                    zapytanie.ExecuteNonQuery();
                    opis = opis_TextBox.Text;
                    status_DropDownList.Items[2].Enabled = true;
                    status_DropDownList.SelectedIndex = 2;
                    status = status_DropDownList.Items[2].Value.ToString();
                }
                catch { }
            }
        }

        protected void usun_Button_Click(object sender, EventArgs e)
        {
            potwierdz_Button.Visible = true;
            anuluj_Button.Visible = true;
            usun_Button.Visible = false;
        }

        protected void anuluj_Button_Click(object sender, EventArgs e)
        {
            potwierdz_Button.Visible = false;
            anuluj_Button.Visible = false;
            usun_Button.Visible = true;
        }

        protected void potwierdz_Button_Click(object sender, EventArgs e)
        {
            SqlTransaction trans = conn.BeginTransaction(IsolationLevel.Serializable);
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.Transaction = trans;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "DELETE FROM Wykonanie_przypadku WHERE Przypadek_testowy_id = '" + przypadek_id + "'";
            
            try
            {
                zapytanie.ExecuteNonQuery();
                zapytanie.CommandText = "DELETE FROM Przypadki_testowe WHERE id = '" + przypadek_id + "'";    
                zapytanie.ExecuteNonQuery();
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
            }
            finally
            {
                trans.Dispose();
                Server.Transfer((string)Session["back"]);
            }
        }         
    }
}