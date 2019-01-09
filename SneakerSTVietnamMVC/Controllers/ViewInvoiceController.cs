using SneakerSTVietnamMVC.Models.DataView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SneakerSTVietnamMVC.Models;
using SneakerSTVietnamMVC.Helpers;

namespace SneakerSTVietnamMVC.Controllers
{
    [AuthorizerController]
    public class ViewInvoiceController : Controller
    {
        // GET: ViewInvoice
        private DB_SNEAKERSTV2 db = new DB_SNEAKERSTV2();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(InvoiceDataView invoice)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            return RedirectToAction("View", invoice);
        }

        public ActionResult View(int? InvoiceID)
        {
            if (InvoiceID == null)
            {
                return View("Error");
            }
            Invoice invoice = db.Invoices.Find(InvoiceID);

            if (invoice == null)
            {
                TempData["Message"] = "Sorry. Not found order number: " + InvoiceID;
                return RedirectToAction("Index");
            }
            List<ShoppingDetail> shoppingDetail = invoice.ShoppingDetails.ToList();
            List<BasketDataView> basketDetail = new List<BasketDataView>();
            double totalAmount = 0;
            foreach (var item in shoppingDetail)
            {
                Product p = db.Products.Find(item.ProductID);
                Size s = db.Sizes.Find(item.SizeID);
                BasketDataView b = new BasketDataView() { ProductName = p.ProductName, CategoryName = p.Category.CategoryName, Gender = p.Gender, Quantity = item.Quantity, SellPrice = item.SellPrice, SizeName = s.SizeName, Thumb = p.ImageProducts.Where(m => m.IsDisplay).FirstOrDefault().ImageURL, TotalAmount = (item.Quantity * item.SellPrice), ProductID = p.ProductID, SizeID = s.SizeID };
                basketDetail.Add(b);
                totalAmount += b.TotalAmount;
            }
            ViewBag.Basket = basketDetail;
            ViewBag.TotalAmount = totalAmount;
            return View(invoice);
        }
    }
}