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
using System.IO;
using SneakerSTVietnamMVC.Helpers;

namespace SneakerSTVietnamMVC.Areas.AdminCP.Controllers
{
    [AuthorizerController]
    public class ImageProductsController : Controller
    {
        private DB_SNEAKERSTV2 db = new DB_SNEAKERSTV2();

        // GET: AdminCP/ImageProducts
        public ActionResult Index()
        {
            var imageProducts = db.ImageProducts.Include(i => i.Product);
            return View(imageProducts.ToList());
        }

        // GET: AdminCP/ImageProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageProduct imageProduct = db.ImageProducts.Find(id);
            if (imageProduct == null)
            {
                return HttpNotFound();
            }
            return View(imageProduct);
        }

        // GET: AdminCP/ImageProducts/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");
            return View();
        }

        // POST: AdminCP/ImageProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,IsDisplay")]  ImageProduct ip, IEnumerable<HttpPostedFileBase> fileUpload)
        {
            if (ip.ProductID.ToString() != null)
            {
                foreach (var f in fileUpload)
                {
                    if (!(f.ContentLength > 0 && (f.ContentType.Contains("jpeg") || f.ContentType.Contains("png") || f.ContentType.Contains("gif"))))
                    {
                        ViewBag.Message = "<div class='alert alert-danger alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Danger: Invalid File Image!</div>";
                        ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", ip.ProductID);
                        return View(ip);
                    }

                }
                Product p = db.Products.Find(ip.ProductID);
                if (p != null)
                {
                    var originalDirectory = new DirectoryInfo(string.Format("{0}Template\\Images\\Products\\", Server.MapPath(@"\")));
                    string directoryPath = Path.Combine(originalDirectory.ToString(), p.ProductName.Trim());
                    bool directoryIsExit = Directory.Exists(directoryPath);
                    if (!directoryIsExit) Directory.CreateDirectory(directoryPath);
                    string filePath;
                    foreach (var f in fileUpload)
                    {
                        string fileName = f.FileName;
                        try
                        {
                            f.SaveAs(Server.MapPath("~/Template/Images/Products/" + p.ProductName.Trim() + "/" + fileName.Trim()));
                            filePath = "Template/Images/Products/" + p.ProductName.Trim() + "/" + fileName.Trim();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            ViewBag.Message = "<div class='alert alert-danger alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Danger: Upload Some Image Files Failed!</div>";
                            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", ip.ProductID);
                            return View(ip);
                        }
                        ip.ImageURL = filePath;
                        db.ImageProducts.Add(ip);
                        db.SaveChanges(); 
                    }
                    TempData["AddSuccess"] = "<div class='alert alert-success alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Success: Added Image To Product!</div>";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "<div class='alert alert-danger alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Danger: Add Image Product Failed!</div>";
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", ip.ProductID);
            return View(ip);
        }

        // GET: AdminCP/ImageProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageProduct imageProduct = db.ImageProducts.Find(id);
            if (imageProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", imageProduct.ProductID);
            return View(imageProduct);
        }

        // POST: AdminCP/ImageProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImageID,ImageURL,ProductID,IsDisplay")] ImageProduct imageProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imageProduct).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AddSuccess"] = "<div class='alert alert-success alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Success: Updated Image!</div>";
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", imageProduct.ProductID);
            return View(imageProduct);
        }

        // GET: AdminCP/ImageProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageProduct imageProduct = db.ImageProducts.Find(id);
            if (imageProduct == null)
            {
                return HttpNotFound();
            }
            return View(imageProduct);
        }

        // POST: AdminCP/ImageProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImageProduct imageProduct = db.ImageProducts.Find(id);
            db.ImageProducts.Remove(imageProduct);
            db.SaveChanges();
            TempData["AddSuccess"] = "<div class='alert alert-success alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Success: Deleted Category!</div>";
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
