using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SneakerSTVietnamMVC.Models.DataView
{
    public partial class BasketDataView
    {
        public int ProductID { set; get; }
        public string Thumb { set; get; }
        public string ProductName { set; get; }
        public string CategoryName { set; get; }
        public string Gender { set; get; }
        public string SizeName { set; get; }
        public int SizeID { set; get; }
        public int Quantity { set; get; }
        public double SellPrice { set; get; }
        public double TotalAmount { set; get; }
    }

    public class Basket
    {
        public int ProductID { set; get; }
        public int Quantity { set; get; }
        public int SizeID { set; get; }
        public double SellPrice { set; get; }
    }
}