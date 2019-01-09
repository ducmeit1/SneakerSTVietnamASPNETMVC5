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
    public class CheckoutController : Controller
    {
        // GET: Checkout
        private DB_SNEAKERSTV2 db = new DB_SNEAKERSTV2();
        private SelectList paymentSelectList = new SelectList(new List<SelectListItem> { new SelectListItem { Text = "Visa", Value = "Visa" }, new SelectListItem { Text = "Master Card", Value = "Master Card" } }, "Value", "Text");
        private SelectList deliveryMethodList = new SelectList(new List<SelectListItem> { new SelectListItem { Text = "Standard", Value = "Standard" }, new SelectListItem { Text = "Express", Value = "Express" } }, "Value", "Text");
        public ActionResult Index()
        {
            if (Session["basket"] == null)
            {
                return RedirectToAction("Index", "Basket");
            }
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.PaymentMethod = paymentSelectList;
            ViewBag.DeliveryMethod = deliveryMethodList;

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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CheckoutDataView model)
        {

            if (ModelState.IsValid)
            {
                bool success = true;
                Invoice invoice = new Invoice();
                invoice.FirstName = model.FirstName;
                invoice.LastName = model.LastName;
                invoice.Email = model.Email;
                invoice.PhoneNumber = model.PhoneNumber;
                invoice.City = model.City;
                invoice.State = model.State;
                invoice.Country = model.Country;
                invoice.Address = model.Address;
                invoice.DeliveryMethod = model.DeliveryMethod;
                invoice.PaymentMethod = model.PaymentMethod;
                invoice.Postcode = model.Postcode;
                ViewUserDataModel us = (ViewUserDataModel)Session["user"];
                invoice.UserID = us.UserID;
                invoice.InvoiceStatusID = 1;
                invoice.InvoiceID = db.Invoices.Count() + 1;
                try
                {
                    db.Invoices.Add(invoice);
                    db.SaveChanges();
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                    Console.WriteLine(ex.Message);
                }
                if (success)
                {
                    List<Basket> baskets = (List<Basket>)Session["basket"];
                    foreach (var item in baskets)
                    {
                        ShoppingDetail s = new ShoppingDetail() { InvoiceID = invoice.InvoiceID, ProductID = item.ProductID, Quantity = item.Quantity, SellPrice = item.SellPrice, SizeID = item.SizeID };
                        db.ShoppingDetails.Add(s);
                        db.SaveChanges();
                    }
                    Session["basket"] = null;
                    var callBackUrl = Url.Action("View", "ViewInvoice", new { InvoiceID = invoice.InvoiceID }, protocol: Request.Url.Scheme);
                    string messageEmail = String.Format("Hi {0}, <br /> Thank you for order at SneakerST Vietnam. <br/> Your order number is: {1}. <br /> Please click <a href=\'{2}\' title=\'View Order\'> here</a> to view your order information. <br />Best regards!", invoice.LastName, invoice.InvoiceID, callBackUrl);
                    if (!HelpersSet.SendEmail(invoice.Email, "Confirm Order Information - SneakerST Vietnam", messageEmail)) return View("Error");
                    TempData["Message"] = "<div class='alert alert-success alert-dismissable' id='alert'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Success: An email confirm your order sent to your email</div>";
                    return RedirectToAction("View", "ViewInvoice", new { InvoiceID = invoice.InvoiceID });
                }
                else
                {
                    return View("Error");
                }
            }
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
            ViewBag.PaymentMethod = paymentSelectList;
            ViewBag.DeliveryMethod = deliveryMethodList;
            return View(model);
        }
    }
}