using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreFrontLab.DATA.EF;

namespace StoreFrontLab.MVC.UI.Models
{
    public class ShoppingCartViewModel
    {
        public int Qty { get; set; }

        public Car Product { get; set; }

        public ShoppingCartViewModel(int qty, Car product)
        {
            Qty = qty;
            Product = product;
        }
    }
}