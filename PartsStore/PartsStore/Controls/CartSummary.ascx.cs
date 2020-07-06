using PartsStore.Models;
using PartsStore.Pages.Helpers;
using System;
using System.Linq;
using System.Web.Routing;

namespace PartsStore.Controls
{
    public partial class CartSummary : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cart cart = SessionHelper.GetCart(Session);
          
            csQuantity.InnerText = cart.Lines.Sum(x => x.Quantity).ToString();
            csTotal.InnerText = cart.ComputeTotalPrice().ToString("c");
            csLink.HRef = RouteTable.Routes.GetVirtualPath(null, "cart", null).VirtualPath;
        }
    }
}