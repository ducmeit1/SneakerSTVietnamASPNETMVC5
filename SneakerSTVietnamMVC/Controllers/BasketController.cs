using SneakerSTVietnamMVC.Models;
using SneakerSTVietnamMVC.Models.DataView;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SneakerSTVietnamMVC.Helpers;

namespace SneakerSTVietnamMVC.Controllers
{
    [AuthorizerController]
    public class BasketController : Controller
    {
        private DB_SNEAKERSTV2 db = new DB_SNEAKERSTV2();
        public ActionResult Index()
        {
            var randomProduct = db.Products.Where(m => m.IsDisplay == true).OrderByDescending(m => m.PublishDate).Take(8).ToList();
            List<ProductModelRepresentView> productDisplayList = new List<ProductModelRepresentView>();
            foreach (var p in randomProduct)
            {
                var imagesProductList = p.ImageProducts.Where(m => m.IsDisplay == true).FirstOrDefault();
                if (imagesProductList == null) continue;
                TimeSpan compaireTime = DateTime.Today - p.PublishDate;
                bool isNew = compaireTime.TotalDays > 5 ? false : true;
                ProductModelRepresentView model = new ProductModelRepresentView() { ProductID = p.ProductID, ProductName = p.ProductName, SellPrice = p.SellPrice, ImageLink = imagesProductList.ImageURL, IsNew = isNew };
                productDisplayList.Add(model);
            }
            ViewBag.RandomProduct = productDisplayList;
            if (Session["basket"] != null)
            {
                List<Basket> basketList = (List<Basket>)Session["basket"];
                List<BasketDataView> basketDataList = new List<BasketDataView>();
                double totalAmount = 0;
                foreach (var item in basketList)
                {
                    Product p = db.Products.Find(item.ProductID);
                    Size s = db.Sizes.Find(item.SizeID);
                    BasketDataView b = new BasketDataView() { ProductName = p.ProductName, CategoryName = p.Category.CategoryName, Gender = p.Gender, Quantity = item.Quantity, SellPrice = item.SellPrice, SizeName = s.SizeName, Thumb = p.ImageProducts.Where(m => m.IsDisplay).FirstOrDefault().ImageURL, TotalAmount = (item.Quantity * item.SellPrice), ProductID = p.ProductID, SizeID = s.SizeID };
                    basketDataList.Add(b);
                    totalAmount += b.TotalAmount;
                }
                ViewBag.Basket = basketDataList;
                ViewBag.TotalAmount = String.Format("{0:#,#}", totalAmount);
            }
            return View();
        }
        public string AddToBasket(int? id, int? size)
        {
            string responseData = "<div class='alert alert-danger alert-dismissable' id='alert'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Error: Add To Basket Failed!</div>";
            if (id == null || size == null)
            {
                return responseData;
            }
            Product p = db.Products.Find(id);
            if (p == null)
            {
                return responseData;
            }
            Size s = db.Sizes.Find(size);
            if (s == null)
            {
                return responseData;
            }
            int stockNumber = db.Stocks.Find(size, id).Quantity;
            if (stockNumber <= 0)
            {
                responseData = "<div class='alert alert-danger alert-dismissable' id='alert'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Error: Out Of Stock!</div>";
                return responseData;
            }
            List<Basket> basketList = null;
            if (Session["basket"] == null)
            {
                basketList = new List<Basket>();
                Basket b = new Basket() { ProductID = p.ProductID, Quantity = 1, SellPrice = p.SellPrice, SizeID = s.SizeID };
                basketList.Add(b);
                Session["basket"] = basketList;
            }
            else
            {
                basketList = (List<Basket>)Session["basket"];
                Basket b = basketList.Find(m => m.ProductID == p.ProductID && m.SizeID == s.SizeID);
                if (b != null)
                {
                    b.Quantity++;
                }
                else
                {
                    b = new Basket() { ProductID = p.ProductID, Quantity = 1, SellPrice = p.SellPrice, SizeID = s.SizeID };
                    basketList.Add(b);
                }
                Session["basket"] = basketList;
            }
            Stock newStock = db.Stocks.Find(size, id);
            newStock.Quantity--;
            db.Entry(newStock).State = EntityState.Modified;
            db.SaveChanges();
            responseData = "<div class='alert alert-success alert-dismissable' id='alert'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Success: Added To Cart!</div>";
            return responseData;
        }

        public string RemoveFromCart(int? id, int? size)
        {
            string responseData = "<div class='alert alert-danger alert-dismissable' id='alert'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Error: Remove From Basket Failed!</div>";
            if (id == null || size == null)
            {
                return responseData;
            }
            Product p = db.Products.Find(id);
            if (p == null)
            {
                return responseData;
            }
            Size s = db.Sizes.Find(size);
            if (s == null)
            {
                return responseData;
            }
            if (Session["basket"] != null)
            {
                List<Basket> basketList = (List<Basket>)Session["basket"];
                Basket b = basketList.Find(m => m.ProductID == p.ProductID && m.SizeID == s.SizeID);
                if (b != null) b.Quantity--;
                if (b.Quantity <= 0)
                {
                    basketList.Remove(b);
                    responseData = "";
                }
                else
                {
                    responseData = "<div class='alert alert-success alert-dismissable' id='alert'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Success: Removed To Cart!</div>";
                }
                if (basketList.Count > 0)
                {
                    Session["basket"] = basketList;
                }
                else
                {
                    Session["basket"] = null;
                }
                Stock newStock = db.Stocks.Find(size, id);
                newStock.Quantity++;
                db.Entry(newStock).State = EntityState.Modified;
                db.SaveChanges();
            }
            return responseData;
        }

        public string GetNewAmount(int id, int size)
        {
            Product p = db.Products.Find(id);
            Size s = db.Sizes.Find(size);
            List<Basket> basketList = (List<Basket>)Session["basket"];
            Basket b = basketList.Find(m => m.ProductID == p.ProductID && m.SizeID == s.SizeID);
            string responseData = String.Format("{0:#,#}", b.SellPrice * b.Quantity);
            return responseData;
        }

        public string GetTotalAmount()
        {
            List<Basket> basketList = (List<Basket>)Session["basket"];
            double totalAmount = 0;
            foreach (Basket bs in basketList)
            {
                totalAmount += bs.Quantity * bs.SellPrice;
            }
            return String.Format("{0:#,#}", totalAmount);
        }
    }
}