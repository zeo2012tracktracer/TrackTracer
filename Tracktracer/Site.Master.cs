using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tracktracer
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        int aktywny_projekt = 0;
        protected void Page_Load(object sender, EventArgs e)
        {            
            try
            {
                Label1.Text = (string)Session["user_login"];
                aktywny_projekt = (int)Session["aktywny_projekt"];
            }
            catch { }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {            
            Session.Clear();
            Server.Transfer("Index.aspx");
        }

        protected void NavigationMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (e.Item.Value.ToString().CompareTo("Projekt") == 0)
            {
                if (aktywny_projekt != 0)
                {
                    Response.Redirect("SzczegolyProjektu.aspx?id=" + aktywny_projekt);
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
            else if (e.Item.Value.ToString().CompareTo("Realizacje") == 0)
            {
                if (aktywny_projekt != 0)
                {
                    Response.Redirect("Aktualne.aspx");
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
    }
}
