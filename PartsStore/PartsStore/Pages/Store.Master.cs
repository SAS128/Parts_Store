using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PartsStore.Pages
{
    public partial class Store : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           

        }
        protected void cmdApply_Change_Theme_Click(object sender, EventArgs e)
        {

            if(cmdApplyChangeTheme.Text == "Светлая Тема")
            {
                Session["Theme"] = "WhiteTheme";
                cmdApplyChangeTheme.Text = "Темная Тема";
            }
            else
            {
                Session["Theme"] = "DarkTheme";
                cmdApplyChangeTheme.Text = "Светлая Тема";
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Theme"] == null)
            {
                // Тема не выбрана
                Page.Theme = "";
            }
            else
            {
                Page.Theme = (string)Session["Theme"];

            }
        }
    }
}