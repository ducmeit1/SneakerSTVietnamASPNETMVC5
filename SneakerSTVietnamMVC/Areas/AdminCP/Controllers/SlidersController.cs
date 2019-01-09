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
    public class SlidersController : Controller
    {
        private DB_SNEAKERSTV2 db = new DB_SNEAKERSTV2();

        // GET: AdminCP/Sliders
        public ActionResult Index()
        {
            return View(db.Sliders.ToList());
        }

        // GET: AdminCP/Sliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: AdminCP/Sliders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminCP/Sliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SlideName,Slide_Description,SlideLink,SlideButtonName,SliderImage")] SliderDataView model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.SliderImage.ContentLength > 0 && (model.SliderImage.ContentType.Contains("jpeg") || model.SliderImage.ContentType.Contains("png") || model.SliderImage.ContentType.Contains("gif")))
            {
                string filePath = "";
                try
                {
                    string fileName = model.SliderImage.FileName;
                    model.SliderImage.SaveAs(Server.MapPath("~/Template/Images/Slider/" + fileName));
                    filePath += "Template/Images/Slider/" + fileName;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ViewBag.Message = "<div class='alert alert-danger alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Danger: Upload Image Slider Failed!</div>";
                    return View(model);
                }

                try
                {
                    Slider slider = new Slider() { SlideLink = model.SlideLink, SlideButtonName = model.SlideButtonName, SlideName = model.SlideName, Slide_Description = model.Slide_Description, SliderImage = filePath };
                    db.Sliders.Add(slider);
                    db.SaveChanges();
                    TempData["AddSuccess"] = "<div class='alert alert-success alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Success: Added New Slider!</div>";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ViewBag.Message = "<div class='alert alert-danger alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Danger: Add Slider Failed!</div>";
                }
            }
            return View(model);
        }

        // GET: AdminCP/Sliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: AdminCP/Sliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SlideID,SlideName,Slide_Description,SlideLink,SlideButtonName,SliderImage")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slider).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AddSuccess"] = "<div class='alert alert-success alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Success: Updated Slider!</div>";
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: AdminCP/Sliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: AdminCP/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.Sliders.Find(id);
            db.Sliders.Remove(slider);
            db.SaveChanges();
            TempData["AddSuccess"] = "<div class='alert alert-success alert-dismissable'><span class='close' data-dismiss='alert'>&times;</span><i class='fa fa-info'></i> Success: Deleted Size!</div>";
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
