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
                    //// отправитель - устанавливаем адрес и отображаемое в письме имя
                    //MailAddress from = new MailAddress("somemail@gmail.com", "Tom");
                    //// кому отправляем
                    //MailAddress to = new MailAddress("somemail@yandex.ru");
                    //// создаем объект сообщения
                    //MailMessage m = new MailMessage(from, to);
                    //// тема письма
                    //m.Subject = "Тест";
                    //// текст письма
                    //m.Body = "<h2>Письмо-тест работы smtp-клиента</h2>";
                    //// письмо представляет код html
                    //m.IsBodyHtml = true;
                    //// адрес smtp-сервера и порт, с которого будем отправлять письмо
                    //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    //// логин и пароль
                    //smtp.Credentials = new NetworkCredential("somemail@gmail.com", "mypassword");
                    //smtp.EnableSsl = true;
                    //smtp.Send(m);

                }
            }
        }
    }
}