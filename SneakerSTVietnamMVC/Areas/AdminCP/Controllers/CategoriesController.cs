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
using SneakerSTVietnamMVC.Helpers;

namespace SneakerSTVietnamMVC.Areas.AdminCP.Controllers
{
    [AuthorizerController]
    public class CategoriesController : Controller
    {
        private DB_SNEAKERSTV2 db = new DB_SNEAKERSTV2();

        // GET: AdminCP/Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryUploadView model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.CategoryLogo.ContentLength > 0 && (model.CategoryLogo.ContentType.Contains("jpeg") || model.CategoryLogo.ContentType.Contains("png") || model.CategoryLogo.ContentType.Contains("gif")))
            {
                string filePath = "";
                try
                {
                    string fileName = model.CategoryLogo.FileName;
                    model.CategoryLogo.SaveAs(Server.MapPath("~/Template/Images/Brand/" + fileName));
                    filePath += "Template/Images/Brand/" + fileName;
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ViewBag.Message = "<div class='alert alert-danger alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Danger: Upload Image Category Logo Failed!</div>";
                    return View(model);
                }
                Category category = new Category() { CategoryID = model.CategoryID, CategoryDescription = model.CategoryDescription, CategoryName = model.CategoryName, CategoryLogo = filePath };
                try
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    TempData["AddSuccess"] = "<div class='alert alert-success alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Success: Added New Category!</div>";
                    return RedirectToAction("Index");
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ViewBag.Message = "<div class='alert alert-danger alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Danger: Add Category Failed!</div>";
                }
            }
            return View(model);
        }

        // GET: AdminCP/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: AdminCP/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: AdminCP/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName,CategoryLogo,CategoryDescription")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AddSuccess"] = "<div class='alert alert-success alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Success: Updated Category!</div>";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: AdminCP/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: AdminCP/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
