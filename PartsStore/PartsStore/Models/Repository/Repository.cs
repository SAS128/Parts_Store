using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PartsStore.Models.Repository
{
    public class Repository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Detail> Details { get { return context.Details;  } }
        public IEnumerable<User> Users { get { return context.Users; } }
        public IEnumerable<Order> Orders
        {
            get
            {
                return context.Orders.Include(c => c.OrderLines.Select(ol => ol.Details));
            }
        }
        public void SaveOrder(Order order)
        {
            if(order.OrderId == 0)
            {
                order = context.Orders.Add(order);
                foreach (OrderLine line in order.OrderLines)
                {
                    context.Entry(line.Details).State = System.Data.Entity.EntityState.Modified;
                }
            }
            else
            {
                Order dbOrder = context.Orders.Find(order.OrderId);
                if(dbOrder != null)
                {
                    dbOrder.Name = order.Name;
                    dbOrder.Line1= order.Line1;
                    dbOrder.Line2 = order.Line2;
                    dbOrder.Line3 = order.Line3;
                    dbOrder.City = order.City;
                    dbOrder.GiftWrap = order.GiftWrap;
                    dbOrder.Dispatched = order.Dispatched;
                }
            }
            context.SaveChanges();
        }
        public void SaveDetails(Detail details)
        {
            if (details.DetailsId == 0)
            {
                details = context.Details.Add(details);
            }
            else
            {
                Detail dbDetail = context.Details.Find(details.DetailsId);
                if (dbDetail != null)
                {
                    dbDetail.Name = details.Name;
                    dbDetail.Price = details.Price;
                    dbDetail.Category = details.Category;
                    dbDetail.Description = details.Description;
                }
            }
            context.SaveChanges();
        }
        public void DeleteDetail(Detail details)
        {
            IEnumerable<Order> orders = context.Orders.
                Include(
                    o => o.OrderLines.Select(
                        ol => ol.Details))
                    .Where(
                        o => o.OrderLines.Count(
                            ol => ol.Details.DetailsId == details.DetailsId) > 0
                           ).ToArray();
            foreach (Order order in orders)
            {
                context.Orders.Remove(order);
            }
            context.Details.Remove(details);
            context.SaveChanges();
        }
    }
}