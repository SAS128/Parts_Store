﻿using PartsStore.Models;
using PartsStore.Models.Repository;
using PartsStore.Pages.Admin;
using PartsStore.Pages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Telegram.Bot.Types.ReplyMarkups;

namespace PartsStore.Pages
{
    public partial class Listing : System.Web.UI.Page
    {
        private Repository repository = new Repository();
        private int pageSize = 4;

        protected int MaxPage
        {
            get
            {
                int prodCount = FilterDetails().Count();
                return (int)Math.Ceiling((decimal)prodCount / pageSize);
            }
        }
        protected IEnumerable<string> GetCategories() => new Repository().Details.Select(p => p.Category).Distinct().OrderBy(x => x);
        //public void AddButton()
        //{
        //            List<InlineKeyboardButton> list = new List<InlineKeyboardButton>();
           
        //    string currentCategory = (string)RouteData.Values["category"] ?? Request.QueryString["category"];
           
        //    foreach (string s in Details.)
        //        {
        //            list.Add(InlineKeyboardButton.WithCallbackData(s));
        //        }
        //    var inline = new InlineKeyboardMarkup(list);
        //}
private int GetPageFromRequest()
        {
            int page;
            string reqValue = (string)RouteData.Values["page"] ?? Request.QueryString["page"];
            return reqValue != null && int.TryParse(reqValue, out page) ? page : 1;
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
        protected int CurrentPage
        {
            get
            {
                int page;
                page = GetPageFromRequest();
                return page > MaxPage ? MaxPage : page;
            }
        }

     
        public IEnumerable<Detail> GetDetails()
        {
            return FilterDetails().
                OrderBy(g => g.DetailsId).
                Skip((CurrentPage - 1) * pageSize).
                Take(pageSize);
        }

        //метод фильтрации по каттегории
        private IEnumerable<Detail> FilterDetails()
        {
            IEnumerable<Detail> details = repository.Details;
            string currentCategory = (string)RouteData.Values["category"] ?? 
                Request.QueryString["category"];
            return currentCategory == null ? details :
                details.Where(p => p.Category == currentCategory);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                int selectedDetailsId;
                if(int.TryParse(Request.Form["add"], out selectedDetailsId))
                {
                    Detail detailsGame = repository.Details.Where(g => g.DetailsId == selectedDetailsId).FirstOrDefault();
                    if (detailsGame != null) {
                        SessionHelper.GetCart(Session).AddItem(detailsGame, 1);
                        SessionHelper.Set(Session, SessionKey.RETURN_URL, Request.RawUrl);

                        Response.Redirect(RouteTable.Routes.GetVirtualPath(null, "cart", null).VirtualPath);
                    }
                }
            }
        }
    }
}