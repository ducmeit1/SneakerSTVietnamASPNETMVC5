using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SneakerSTVietnamMVC.Models.DataView;
using SneakerSTVietnamMVC.Models;
using System.Data.Entity;

namespace SneakerSTVietnamMVC.Controllers
{
    public class SessionController
    {
        public void SessionDestroyed()
        {
            if (HttpContext.Current.Session["basket"] != null)
            {
                DB_SNEAKERSTV2 db = new DB_SNEAKERSTV2();
                List<Basket> basketList = (List<Basket>)HttpContext.Current.Session["basket"];
                foreach (var item in basketList)
                {
                    Stock newStock = db.Stocks.Find(item.SizeID, item.ProductID);
                    newStock.Quantity = newStock.Quantity + item.Quantity;
                    db.Entry(newStock).State = EntityState.Modified;
                    db.SaveChanges();
                }
                HttpContext.Current.Session["basket"] = null;
            }
        }
    }
}