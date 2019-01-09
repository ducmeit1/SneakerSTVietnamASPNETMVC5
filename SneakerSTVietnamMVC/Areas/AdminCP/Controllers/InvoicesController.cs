using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SneakerSTVietnamMVC.Models;
using SneakerSTVietnamMVC.Models.DataView;

namespace SneakerSTVietnamMVC.Areas.AdminCP.Controllers
{
    public class InvoicesController : Controller
    {
        private DB_SNEAKERSTV2 db = new DB_SNEAKERSTV2();

        // GET: AdminCP/Invoices
        public ActionResult Index()
        {
            var invoices = db.Invoices.Include(i => i.InvoiceStatu).Include(i => i.User);
            return View(invoices.ToList());
        }

        // GET: AdminCP/Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
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

        // GET: AdminCP/Invoices/Create
        public ActionResult Create()
        {
            ViewBag.InvoiceStatusID = new SelectList(db.InvoiceStatus, "InvoiceStatusID", "InvoiceStatusName");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email");
            return View();
        }

        // POST: AdminCP/Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceID,FirstName,LastName,Email,Address,City,Postcode,State,PhoneNumber,Country,PaymentMethod,DeliveryMethod,InvoiceStatusID,UserID")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                TempData["AddSuccess"] = "<div class='alert alert-success alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Success: Created Invoice!</div>";
                return RedirectToAction("Index");
            }

            ViewBag.InvoiceStatusID = new SelectList(db.InvoiceStatus, "InvoiceStatusID", "InvoiceStatusName", invoice.InvoiceStatusID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", invoice.UserID);
            return View(invoice);
        }

        // GET: AdminCP/Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceStatusID = new SelectList(db.InvoiceStatus, "InvoiceStatusID", "InvoiceStatusName", invoice.InvoiceStatusID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", invoice.UserID);
            return View(invoice);
        }

        // POST: AdminCP/Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceID,FirstName,LastName,Email,Address,City,Postcode,State,PhoneNumber,Country,PaymentMethod,DeliveryMethod,InvoiceStatusID,UserID")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AddSuccess"] = "<div class='alert alert-success alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Success: Updated Invoice!</div>";
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceStatusID = new SelectList(db.InvoiceStatus, "InvoiceStatusID", "InvoiceStatusName", invoice.InvoiceStatusID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", invoice.UserID);
            return View(invoice);
        }

        // GET: AdminCP/Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
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

        // POST: AdminCP/Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
            db.SaveChanges();
            TempData["AddSuccess"] = "<div class='alert alert-success alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Success: Deleted Invoice!</div>";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
