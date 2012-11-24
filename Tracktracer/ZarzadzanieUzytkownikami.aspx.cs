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
    public partial class ZarzadzanieUzytkownikami : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                user_id = (int)Session["user_id"];
                conn = (SqlConnection)Session["connection"];
            }
            catch
            {
                Server.Transfer("Index.aspx");
            }

            if (user_id != 1) Server.Transfer("Index.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Server.Transfer("NowyUzytkownik.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["mod_user"] = GridView1.SelectedRow.Cells[0].Text;
            Session["mod_imie"] = GridView1.SelectedRow.Cells[1].Text;
            Session["mod_nazwisko"] = GridView1.SelectedRow.Cells[2].Text;
            Session["mod_status"] = "aktywne";
            
            Server.Transfer("EdycjaUzytkownika.aspx");
        }

        protected void GridView2_SelectedIndexChanged1(object sender, EventArgs e)
        {
            Session["mod_user"] = zabl_GridView.SelectedRow.Cells[0].Text;
            Session["mod_imie"] = zabl_GridView.SelectedRow.Cells[1].Text;
            Session["mod_nazwisko"] = zabl_GridView.SelectedRow.Cells[2].Text;
            Session["mod_status"] = "zablokowane";

            Server.Transfer("EdycjaUzytkownika.aspx");
        }
   }
}