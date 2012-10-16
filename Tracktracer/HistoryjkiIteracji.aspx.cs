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
    public partial class HistoryjkiIteracji : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;
        private int projekt_id;
        private string nazwa;
        private string wydanie_nr;
        private string iteracja_nr;
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
                wydanie_nr = (string)Session["wydanie_nr"];
                iteracja_nr = (string)Session["iteracja_nr"];
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
                powroty.Add("HistoryjkIteracji.aspx");
                powroty_id.Add(0);
            }
            Session["powroty"] = powroty;
            Session["powroty_id"] = powroty_id;

            projekt_Label.Text = "Projekt: " + nazwa;

        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int no = 0;
            if (e.CommandName.CompareTo("Historyjka") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                Session["historyjka_id"] = no;
                Session["back"] = "HistoryjkiIteracji.aspx";
                Server.Transfer("Historyjka.aspx");
            }
            else if (e.CommandName.CompareTo("Zadanie") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                Session["zadanie_id"] = no.ToString();
                Session["back"] = "HistoryjkiIteracji.aspx";
                Server.Transfer("ZadanieProgramistyczne.aspx");
            }
        }

        protected void powrot_Button_Click(object sender, EventArgs e)
        {
            Server.Transfer("ProjektXP.aspx");
        }
    }
}