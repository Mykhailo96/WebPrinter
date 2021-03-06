﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WP.Models;

namespace WP.Controllers
{
    public class PurchasesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Purchases
        [Authorize]
        public ActionResult Index()
        {
            if(User.Identity.Name == "Admin")
            {
                var allPurchase = db.Purchases;
                return View(allPurchase.ToList());
            }
            var purchases = from p in db.Purchases where p.ApplicationUser.UserName == User.Identity.Name select p;
            return View(purchases.ToList());
        }

        // GET: Purchases/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }

            var dir = new DirectoryInfo(Server.MapPath("~/App_Data/3DModels/"));
            FileInfo[] fileNames = dir.GetFiles(purchase.FileName);
            ViewBag.fileName = fileNames;
            return View(purchase);
        }

        public FileResult Download(string name)
        {
            return File("~/App_Data/3DModels/" + name, System.Net.Mime.MediaTypeNames.Application.Octet, name);
        }

        // GET: Purchases/Create
        [Authorize(Roles = "user")]
        public ActionResult Create()
        {
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "ApplicationUserID");
            return View();
        }

        // POST: Purchases/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ObjectPrecision,ObjecttColor,ObjectMaterial")] Purchase purchase)
        {
            var fileTypes = new string[] { ".stl", ".wrl", ".vrml", ".amf", ".sldprt", ".obj", ".x3g", ".ply", ".fbx" };

            var file = Request.Files[0];

            if (file.ContentLength == 0)
            {
                return View(purchase);
            }

            var fileName = Path.GetFileName(file.FileName);
            if (!fileTypes.Any(fileName.ToLower().Contains))
            {
                return View(purchase);
            }
            var path = Path.Combine(Server.MapPath("~/App_Data/3DModels"), fileName);
            file.SaveAs(path);

            purchase.FileName = fileName;
            purchase.OrderStatus = Status.Pending;
            purchase.ApplicationUserID = User.Identity.GetUserId();

            Random rand = new Random();
            purchase.OrderNumber = rand.Next(100000, 99999999);

            if (ModelState.IsValid)
            {
                db.Purchases.Add(purchase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "ApplicationUserID", purchase.ApplicationUserID);
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        [Authorize(Roles = "admin, user")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Purchase purchase = db.Purchases.Find(id);

            if(purchase.OrderStatus.ToString() != "Pending" && User.Identity.Name != "Admin")
            {
                return RedirectToAction("Details", new { id = id});
            }

            if (purchase == null)
            {
                return HttpNotFound();
            }

            ViewBag.ApplicationUserID = new SelectList(db.Users , "Id", "FirstName", purchase.ApplicationUserID);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Price,ObjectPrecision,ObjecttColor,ObjectMaterial,OrderStatus,OrderNumber,FileName,ApplicationUserID")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                var fileTypes = new string[] { ".stl", ".wrl", ".vrml", ".amf", ".sldprt", ".obj", ".x3g", ".ply", ".fbx" };

                if (Request.Files.Count != 0)
                {
                    var file = Request.Files[0];

                    if (file.ContentLength != 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        if (!fileTypes.Any(fileName.ToLower().Contains))
                        {
                            return View(purchase);
                        }

                        string fullPath = Path.Combine(Server.MapPath("~/App_Data/3DModels"), purchase.FileName);

                        System.IO.File.Delete(fullPath);

                        var path = Path.Combine(Server.MapPath("~/App_Data/3DModels"), fileName);
                        file.SaveAs(path);

                        purchase.FileName = fileName;
                    }
                }

                db.Entry(purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "FirstName", purchase.ApplicationUserID);
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        [Authorize(Roles = "admin, user")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            db.Purchases.Remove(purchase);
            db.SaveChanges();
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
