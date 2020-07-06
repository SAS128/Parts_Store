using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartsStore.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Details details, int quantity)
        {
            CartLine cartLine = lineCollection.Where(p => p.Details.DetailsId == details.DetailsId).FirstOrDefault();
            if (cartLine == null)
            {
                lineCollection.Add(new CartLine() { Details = details, Quantity = quantity });
            }
            else
            {
                cartLine.Quantity += quantity;
            }
        }
        public void RemoveLine(Details details)
        {
            lineCollection.RemoveAll(l => l.Details.DetailsId == details.DetailsId);
        }
        public void Clear()
        {
            lineCollection.Clear();
        }
        public decimal ComputeTotalPrice()
        {
            return lineCollection.Sum(e => e.Details.Price * e.Quantity);
        }
        public IEnumerable<CartLine> Lines
        { get{ return lineCollection; } }
    }

    public class CartLine
    {
        public Details Details { get; set; }
        public int Quantity { get; set; }
    }
}