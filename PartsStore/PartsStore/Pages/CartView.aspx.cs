using PartsStore.Models;
using PartsStore.Models.Repository;
using PartsStore.Pages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace PartsStore.Pages
{
    public partial class CartView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Repository repository = new Repository();
                int detailsId;
                if(int.TryParse(Request.Form["remove"], out detailsId)){
                    Detail gameToRemove = repository.Details.Where(g => g.DetailsId == detailsId).FirstOrDefault();
                    if(detailsId != null)
                    {
                        SessionHelper.GetCart(Session).RemoveLine(gameToRemove);
                    }
                }
            }
        }
     
        public IEnumerable<PartsStore.Models.CartLine> GetCartLines()
        {
            return SessionHelper.GetCart(Session).Lines;
        }

        public decimal CartTotal { get { return SessionHelper.GetCart(Session).ComputeTotalPrice(); } }

        public string ReturnUrl { get { return SessionHelper.Get<string>(Session, SessionKey.RETURN_URL); } }
        public string CheckoutUrl { get { return RouteTable.Routes.GetVirtualPath(null, "checkout", null).VirtualPath; } }
    }
}