using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SneakerSTVietnamMVC.Models;
using SneakerSTVietnamMVC.Helpers;

namespace SneakerSTVietnamMVC.Areas.AdminCP.Controllers
{
    [AuthorizerController]
    public class StocksController : Controller
    {
        private DB_SNEAKERSTV2 db = new DB_SNEAKERSTV2();

        // GET: AdminCP/Stocks
        public ActionResult Index()
        {
            var stocks = db.Stocks.Include(s => s.Product).Include(s => s.Size);
            return View(stocks.ToList());
        }


        // GET: AdminCP/Stocks/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");
            ViewBag.SizeID = new SelectList(db.Sizes, "SizeID", "SizeName");
            return View();
        }

        // POST: AdminCP/Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SizeID,ProductID,Quantity")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Stocks.Add(stock);
                db.Products.Find(stock.ProductID).InStock = true;
                db.SaveChanges();
                TempData["AddSuccess"] = "<div class='alert alert-success alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Success: Imported New Stock!</div>";
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", stock.ProductID);
            ViewBag.SizeID = new SelectList(db.Sizes, "SizeID", "SizeName", stock.SizeID);
            return View(stock);
        }

        // GET: AdminCP/Stocks/Edit/5
        public ActionResult Edit(int? sizeid, int? productid)
        {
            if (productid == null || sizeid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(sizeid, productid);
            if (stock == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", stock.ProductID);
            ViewBag.SizeID = new SelectList(db.Sizes, "SizeID", "SizeName", stock.SizeID);
            return View(stock);
        }

        // POST: AdminCP/Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SizeID,ProductID,Quantity")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AddSuccess"] = "<div class='alert alert-success alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Success: Updated Stock!</div>";
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", stock.ProductID);
            ViewBag.SizeID = new SelectList(db.Sizes, "SizeID", "SizeName", stock.SizeID);
            return View(stock);
        }

        // GET: AdminCP/Stocks/Delete/5
        public ActionResult Delete(int? sizeid, int? productid)
        {
            if (productid == null || sizeid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(sizeid, productid);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: AdminCP/Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int sizeid, int productid)
        {
            Stock stock = db.Stocks.Find(sizeid, productid);
            db.Stocks.Remove(stock);
            db.SaveChanges();
            TempData["AddSuccess"] = "<div class='alert alert-success alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Success: Deleted Stock Of Product!</div>";
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
