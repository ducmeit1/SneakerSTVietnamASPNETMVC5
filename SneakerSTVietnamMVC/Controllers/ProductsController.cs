using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SneakerSTVietnamMVC.Models;
using SneakerSTVietnamMVC.Models.DataView;
using SneakerSTVietnamMVC.Helpers;

namespace SneakerSTVietnamMVC.Controllers
{
    [AuthorizerController]
    public class ProductsController : Controller
    {
        private DB_SNEAKERSTV2 db = new DB_SNEAKERSTV2();

        private SelectList dateSelectList = new SelectList(new List<SelectListItem> { new SelectListItem { Text = "A-Z", Value = "0" }, new SelectListItem { Text = "Z-A", Value = "1" } }, "Value", "Text");
        private SelectList priceSelectList = new SelectList(new List<SelectListItem> { new SelectListItem { Text = "A-Z", Value = "0" }, new SelectListItem { Text = "Z-A", Value = "1" } }, "Value", "Text");
        // GET: NewArrivals
        public ActionResult NewArrivals()
        {
            var productList = db.Products.Where(m => m.IsDisplay == true).OrderByDescending(m => m.PublishDate).ToList();
            List<ProductModelRepresentView> productDisplayList = new List<ProductModelRepresentView>();
            foreach (var p in productList)
            {
                var imagesProductList = p.ImageProducts.Where(m => m.IsDisplay == true).FirstOrDefault();
                if (imagesProductList == null) continue;
                TimeSpan compaireTime = DateTime.Today - p.PublishDate;
                bool isNew = compaireTime.TotalDays > 5 ? false : true;
                ProductModelRepresentView model = new ProductModelRepresentView() { ProductID = p.ProductID, ProductName = p.ProductName, SellPrice = p.SellPrice, ImageLink = imagesProductList.ImageURL, IsNew = isNew };
                productDisplayList.Add(model);
            }
            ViewBag.date = dateSelectList;
            ViewBag.price = priceSelectList;
            ViewBag.category = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.size = new SelectList(db.Sizes, "SizeID", "SizeName");
            return View(productDisplayList);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewArrivals(int? date, int? price, int? category)
        {
            if (date == null && price == null && category == null)
            {
                return RedirectToAction("Index");
            }
            var productList = db.Products.Where(m => m.IsDisplay == true);
            switch (date)
            {
                case 0:
                    productList = productList.OrderBy(m => m.PublishDate);
                    break;
                case 1:
                    productList = productList.OrderByDescending(m => m.PublishDate);
                    break;
            }
            switch (price)
            {
                case 0:
                    productList = productList.OrderBy(m => m.SellPrice);
                    break;
                case 1:
                    productList = productList.OrderByDescending(m => m.SellPrice);
                    break;
            }
            if (category != null)
            {
                productList = productList.Where(m => m.CategoryID == category);
            }

            List<ProductModelRepresentView> productDisplayList = new List<ProductModelRepresentView>();
            foreach (var p in productList.ToList())
            {
                var imagesProductList = p.ImageProducts.Where(m => m.IsDisplay == true).FirstOrDefault();
                if (imagesProductList == null) continue;
                TimeSpan compaireTime = DateTime.Today - p.PublishDate;
                bool isNew = compaireTime.TotalDays > 5 ? false : true;
                ProductModelRepresentView model = new ProductModelRepresentView() { ProductID = p.ProductID, ProductName = p.ProductName, SellPrice = p.SellPrice, ImageLink = imagesProductList.ImageURL, IsNew = isNew };
                productDisplayList.Add(model);
            }
            ViewBag.date = dateSelectList;
            ViewBag.price = priceSelectList;
            ViewBag.category = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.size = new SelectList(db.Sizes, "SizeID", "SizeName");
            return View(productDisplayList);
        }

        public ActionResult ViewDetail(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            var productDetail = db.Products.Find(id);
            if (productDetail == null)
            {
                return View("Error");
            }
            ViewBag.MainImage = productDetail.ImageProducts.Where(p => p.IsDisplay == true).FirstOrDefault().ImageURL;
            //ViewBag.Size = new SelectList(productDetail.Stocks.ToList(), "SizeID", "SizeName");
            List<SelectListItem> sizeListItem = new List<SelectListItem>();
            foreach (var item in productDetail.Stocks.Where(m => m.Quantity > 0))
            {
                SelectListItem s = new SelectListItem { Value = item.SizeID.ToString(), Text = item.Size.SizeName };
                sizeListItem.Add(s);
            }
            if (sizeListItem.Count > 0) ViewBag.Size = new SelectList(sizeListItem, "Value", "Text");
            else
            {
                db.Products.Find(productDetail.ProductID).InStock = false;
                db.SaveChanges();
            }
            var randomProduct = db.Products.Where(m => m.IsDisplay == true).Where(m => m.CategoryID == productDetail.CategoryID).OrderByDescending(m => m.PublishDate).Take(8).ToList();
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
            return View(productDetail);
        }

        public ActionResult Search(string keyword)
        {
            if (keyword.Trim().Length > 0) {
                var productList = db.Products.Where(m => m.IsDisplay == true).Where(m => m.ProductName.Contains(keyword) || m.Category.CategoryName.Contains(keyword)).OrderByDescending(m => m.PublishDate).Distinct().ToList();
                if (productList.Count > 0)
                {
                    List<ProductModelRepresentView> productDisplayList = new List<ProductModelRepresentView>();
                    foreach (var p in productList)
                    {
                        var imagesProductList = p.ImageProducts.Where(m => m.IsDisplay == true).FirstOrDefault();
                        if (imagesProductList == null) continue;
                        TimeSpan compaireTime = DateTime.Today - p.PublishDate;
                        bool isNew = compaireTime.TotalDays > 5 ? false : true;
                        ProductModelRepresentView model = new ProductModelRepresentView() { ProductID = p.ProductID, ProductName = p.ProductName, SellPrice = p.SellPrice, ImageLink = imagesProductList.ImageURL, IsNew = isNew };
                        productDisplayList.Add(model);
                    }
                    ViewBag.KeyWord = keyword;
                    return View(productDisplayList);
                }
            }
            TempData["Message"] = "<h1 class='text-center'>Sorry. Your keyword is empty or not found!</h1>" + "<br/><br/><h3>RECENT SNEAKERS MAY BE YOU NEED</h3>";
            var randomProduct = db.Products.Where(m => m.IsDisplay == true).Take(8).ToList();
            List<ProductModelRepresentView> productRandomDisplayList = new List<ProductModelRepresentView>();
            foreach (var p in randomProduct)
            {
                var imagesProductList = p.ImageProducts.Where(m => m.IsDisplay == true).FirstOrDefault();
                if (imagesProductList == null) continue;
                TimeSpan compaireTime = DateTime.Today - p.PublishDate;
                bool isNew = compaireTime.TotalDays > 5 ? false : true;
                ProductModelRepresentView model = new ProductModelRepresentView() { ProductID = p.ProductID, ProductName = p.ProductName, SellPrice = p.SellPrice, ImageLink = imagesProductList.ImageURL, IsNew = isNew };
                productRandomDisplayList.Add(model);
            }
            return View(productRandomDisplayList);
        }
    }
}