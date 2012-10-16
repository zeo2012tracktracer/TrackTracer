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
    public partial class HistoryjkiUzytkownika : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;
        private int projekt_id;
        private string nazwa;
        List<string> zaznaczone_id;
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

            if (powroty == null)
            {
                powroty = new List<string>();
                powroty_id = new List<int>();
            }
            if (!IsPostBack)
            {
                powroty.Add("HistoryjkiUzytkownika.aspx");
                powroty_id.Add(0);
            }
            Session["powroty"] = powroty;
            Session["powroty_id"] = powroty_id;

            projekt_Label.Text = "Projekt: " + nazwa;
            zaznaczone_id = new List<string>();

            if (usuniete_CheckBox.Checked) SqlDataSource1.SelectCommand = "SELECT hu.id, hu.nazwa, hu.nr_wydania, hu.nr_iteracji FROM Historyjki_uzytkownikow hu WHERE hu.Projekty_id = @projekt_id;";
            else SqlDataSource1.SelectCommand = "SELECT hu.id, hu.nazwa, hu.nr_wydania, hu.nr_iteracji FROM Historyjki_uzytkownikow hu WHERE hu.Projekty_id = @projekt_id AND hu.status != 'Usunięta';";

            if (usuniete2_CheckBox.Checked) SqlDataSource4.SelectCommand = "SELECT nazwa, id, status FROM Zadania_programistyczne WHERE Projekty_id = @projekt_id";
            else SqlDataSource4.SelectCommand = "SELECT nazwa, id, status FROM Zadania_programistyczne WHERE Projekty_id = @projekt_id AND status != 'Usunięte';";
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            cel_iteracji();
            ustaw_checkboxy();
            base.OnPreRenderComplete(e);
        }

        protected void przypisz_Button_Click(object sender, EventArgs e)
        {
            if (sprawdz_zaznaczone() != 0)
            {
                string iteracja = iteracja_DropDownList.SelectedValue;
                string wydanie = wydanie_DropDownList.SelectedValue;
                int i_id;
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    foreach (string i in zaznaczone_id)
                    {
                        SqlCommand zapytanie = new SqlCommand();
                        zapytanie.Connection = conn;
                        zapytanie.Transaction = trans;
                        zapytanie.CommandType = CommandType.Text;
                        zapytanie.CommandText = "SELECT i.id FROM Wydania w, Iteracje i WHERE w.Projekty_id='" + projekt_id + "' AND w.nr_wydania='" + wydanie + "' AND i.Wydania_id=w.id AND i.nr_iteracji ='" + iteracja + "';";

                        SqlDataReader reader = zapytanie.ExecuteReader();
                        try
                        {
                            reader.Read();
                            i_id = reader.GetInt32(0);
                            reader.Close();
                        }
                        catch
                        {
                            reader.Dispose();
                            i_id = -1;
                        }

                        zapytanie.CommandText = "UPDATE Historyjki_uzytkownikow SET Iteracje_id='" + i_id + "', nr_iteracji='" + iteracja + "', nr_wydania='" + wydanie + "' WHERE id='" + i + "'";
                        zapytanie.ExecuteNonQuery();
                    }
                    trans.Commit();
                    GridView1.DataBind();
                    ustaw_checkboxy();
                }
                catch
                {
                    trans.Rollback();
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }

        protected void ustaw_checkboxy()
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.Cells[3].Text.CompareTo("&nbsp;") != 0)
                {
                    ((CheckBox)row.FindControl("CheckBox1")).Enabled = false;
                }
            }
        }

        protected int sprawdz_zaznaczone()
        {
            zaznaczone_id.Clear();

            int sa = 0;
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (((CheckBox)row.FindControl("CheckBox1")).Checked == true)
                {
                    zaznaczone_id.Add(row.Cells[1].Text);
                    sa = 1;
                }
            }
            return sa;
        }

        protected void iteracja_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            cel_iteracji();
        }

        protected void cel_iteracji()
        {
            if (iteracja_DropDownList.Items.Count != 0)
            {
                string iteracja = iteracja_DropDownList.SelectedValue;
                string wydanie = wydanie_DropDownList.SelectedValue;

                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "SELECT i.cel_iteracji FROM Iteracje i, Wydania w WHERE w.Projekty_id = '" + projekt_id + "' AND w.nr_wydania = '" + wydanie + "' AND i.nr_iteracji ='" + iteracja + "' AND i.Wydania_id=w.id;";

                SqlDataReader reader;
                reader = zapytanie.ExecuteReader();
                try
                {
                    reader.Read();
                    celIt_Label.Text = reader.GetString(0);
                    reader.Close();
                }
                catch
                {
                    reader.Dispose();
                }

                cel_Label.Visible = true;
                celIt_Label.Visible = true;
            }
            else
            {
                cel_Label.Visible = false;
                celIt_Label.Visible = false;
            }
        }

        protected void wydanie_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            iteracja_DropDownList.DataBind();
            cel_iteracji();
        }

        protected void usuniete_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void usuniete2_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GridView2.DataBind();
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int no = 0;
            if (e.CommandName.CompareTo("Historyjka") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                Session["historyjka_id"] = no;
                Session["back"] = "HistoryjkiUzytkownika.aspx";
                Server.Transfer("Historyjka.aspx");
            }
            else if (e.CommandName.CompareTo("Zadanie") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                Session["zadanie_id"] = no.ToString();
                Session["back"] = "HistoryjkiUzytkownika.aspx";
                Server.Transfer("ZadanieProgramistyczne.aspx");
            }
        }

        protected void powrot_Button_Click(object sender, EventArgs e)
        {
            Server.Transfer("ProjektXP.aspx");
        }

        protected void historyjka_Button_Click(object sender, EventArgs e)
        {
            Server.Transfer("NowaHistoryjka.aspx");
        }
    }
}