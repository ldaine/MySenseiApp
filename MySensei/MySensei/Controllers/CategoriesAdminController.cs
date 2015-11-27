﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MySensei.Infrastructure;
using MySensei.Models;

namespace MySensei.Controllers
{
    public class CategoriesAdminController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: CategoriesAdmin
        public ActionResult Index()
        {
            return View(db.AppCategorys.ToList());
        }

        // GET: CategoriesAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppCategory appCategory = db.AppCategorys.Find(id);
            if (appCategory == null)
            {
                return HttpNotFound();
            }
            return View(appCategory);
        }

        // GET: CategoriesAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Category")] AppCategory appCategory)
        {
            if (ModelState.IsValid)
            {
                db.AppCategorys.Add(appCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appCategory);
        }

        // GET: CategoriesAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppCategory appCategory = db.AppCategorys.Find(id);
            if (appCategory == null)
            {
                return HttpNotFound();
            }
            return View(appCategory);
        }

        // POST: CategoriesAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Category")] AppCategory appCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appCategory);
        }

        // GET: CategoriesAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppCategory appCategory = db.AppCategorys.Find(id);
            if (appCategory == null)
            {
                return HttpNotFound();
            }
            return View(appCategory);
        }

        // POST: CategoriesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppCategory appCategory = db.AppCategorys.Find(id);
            db.AppCategorys.Remove(appCategory);
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
