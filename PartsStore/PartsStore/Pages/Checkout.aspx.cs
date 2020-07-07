using PartsStore.Models;
using PartsStore.Models.Repository;
using PartsStore.Pages.Helpers;
using System;
using System.Collections.Generic;
using System.Web.ModelBinding;

namespace PartsStore.Pages
{
    public partial class Checkout : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            checkoutForm.Visible = true;
            checkoutMessage.Visible = false;

            if(IsPostBack)
            {
                Order order = new Order();
                if(TryUpdateModel(order, new FormValueProvider(ModelBindingExecutionContext)))
                {
                    order.OrderLines = new List<OrderLine>();

                    Cart cart = SessionHelper.GetCart(Session);

                    foreach (CartLine line in cart.Lines)
                    {
                        order.OrderLines.Add(new OrderLine() {
                            Order = order,
                            Details = line.Details,
                            Quantity = line.Quantity
                        });
                    }

                    new Repository().SaveOrder(order);
                    cart.Clear();

                    checkoutForm.Visible = false;
                    checkoutMessage.Visible = true;
                }
            }
        }
    }
}