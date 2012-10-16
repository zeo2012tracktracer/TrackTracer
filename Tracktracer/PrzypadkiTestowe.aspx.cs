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

    public partial class PrzypadkiTestowe : System.Web.UI.Page
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
            if (powroty == null)
            {
                powroty = new List<string>();
                powroty_id = new List<int>();
            }
            if (!IsPostBack)
            {
                powroty.Add("PrzypadkiTestowe.aspx");
                powroty_id.Add(0);
            }
            Session["powroty"] = powroty;
            Session["powroty_id"] = powroty_id;

            nazwa_Label.Text = "Projekt: " + nazwa;
            
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (GridView1.Rows[i].Cells[1].Text.CompareTo("Zaliczony") == 0)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromArgb(179, 255, 102);
                }
                else
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromArgb(255, 255, 102);
                }
            }
        }

        protected void powrot_Button_Click(object sender, EventArgs e)
        {            
            Server.Transfer("SzczegolyProjektu.aspx?id=" + projekt_id);
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int no = 0;
            if (e.CommandName.CompareTo("PrzypadekTestowy") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                Session["przypadek_id"] = no;
                Session["back"] = "PrzypadkiTestowe.aspx";                
                Server.Transfer("PrzypadekTestowy.aspx");
            }
        }
    }
}