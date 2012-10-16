using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tracktracer
{
    public partial class Administracja : System.Web.UI.Page
    {
        private int user_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                user_id = (int)Session["user_id"];                
            }
            catch
            {
                Server.Transfer("Index.aspx");
            }

            if (user_id == 1)
            {
                zarzadzaj_Button.Visible = true;
            }            

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Server.Transfer("ZarzadzanieUzytkownikami.aspx");
        }

        protected void haslo_Button_Click(object sender, EventArgs e)
        {
            Server.Transfer("ZmianaHasla.aspx");
        }

        protected void dane_Button_Click(object sender, EventArgs e)
        {
            Server.Transfer("ZmianaDanych.aspx");
        }
    }
}