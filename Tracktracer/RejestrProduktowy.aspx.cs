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
    public partial class RejestrProduktowy : System.Web.UI.Page
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
                powroty.Add("RejestrProduktowy.aspx");
                powroty_id.Add(0);
            }
            Session["powroty"] = powroty;
            Session["powroty_id"] = powroty_id;

            nazwa_Label.Text = nazwa;            
            zaznaczone_id = new List<string>();

            if (usuniete_CheckBox.Checked) SqlDataSource1.SelectCommand = "SELECT w.id, w.nazwa, w.nr_wydania, w.nr_iteracji FROM Wymagania w WHERE w.Projekty_id = @proj_id;";
            else SqlDataSource1.SelectCommand = "SELECT w.id, w.nazwa, w.nr_wydania, w.nr_iteracji FROM Wymagania w WHERE w.Projekty_id = @proj_id AND w.status != 'usunięte';";            
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            cel_iteracji();
            ustaw_checkboxy();
            base.OnPreRenderComplete(e);
        }

        protected void wymaganie_Button_Click(object sender, EventArgs e)
        {
            Server.Transfer("NoweWymaganie.aspx");
        }

        protected void powrot_Button_Click(object sender, EventArgs e)
        {
            Response.Redirect("SzczegolyProjektu.aspx?id=" + projekt_id);
        }

        protected void wydanie_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            iteracja_DropDownList.DataBind();
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

        protected void iteracja_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            cel_iteracji();
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

                        SqlCommand zapytanie2 = new SqlCommand();
                        zapytanie2.Connection = conn;
                        zapytanie2.Transaction = trans;
                        zapytanie2.CommandType = CommandType.Text;
                        zapytanie2.CommandText = "UPDATE Wymagania SET Iteracje_id='" + i_id + "', nr_iteracji='" + iteracja + "', nr_wydania='" + wydanie + "' WHERE id='" + i + "'";
                        zapytanie2.ExecuteNonQuery();
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

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string wymaganie_id = GridView1.SelectedRow.Cells[1].Text;
            Session["wymaganie_id"] = wymaganie_id;
            Session["back"] = "RejestrProduktowy.aspx";
            Server.Transfer("Wymaganie.aspx");
        }

        protected void usuniete_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int no = 0;
            if (e.CommandName.CompareTo("Wymaganie") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                Session["wymaganie_id"] = no.ToString();
                Session["back"] = "RejestrProduktowy.aspx";
                Server.Transfer("Wymaganie.aspx");
            }
        }
    }
}