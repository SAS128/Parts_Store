using PartsStore.Models;
using PartsStore.Models.Repository;
using PartsStore.Pages.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
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
                    //------------------------------Mail---------------------------------------
                    var fromAddress = new MailAddress("kormet61@gmail.com", "From Name");
                    var toAddress = new MailAddress("Sandmen461@gmail.com", "To Name");
                    const string fromPassword = "Rjhybqxer1";
                    const string subject = "Покупка деталей на СТО";
                    const string body = "Вы оплатиле следующие товары: на сумму:...";

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                }
            }
        }
    }
}