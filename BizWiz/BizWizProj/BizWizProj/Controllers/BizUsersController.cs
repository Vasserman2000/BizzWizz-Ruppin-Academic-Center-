using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BizWizProj.Context;
using BizWizProj.Models;
using BizWizProj.Authorization;

namespace BizWizProj.Controllers
{
    [MyAuthorize]
    public class BizUsersController : Controller
    {
        private DB db = new DB();

        // GET: BizUsers
        public ActionResult Index()
        {
            return View(db.BizUsers.ToList());
        }

        // GET: BizUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BizUser bizUser = db.BizUsers.Find(id);
            if (bizUser == null)
            {
                return HttpNotFound();
            }
            return View(bizUser);
        }

        // GET: BizUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BizUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FullName,Password,Email,PhoneNumber,EmployeeType")] BizUser bizUser)
        {
            if (ModelState.IsValid)
            {
                db.BizUsers.Add(bizUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bizUser);
        }

        // GET: BizUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BizUser bizUser = db.BizUsers.Find(id);
            if (bizUser == null)
            {
                return HttpNotFound();
            }
            return View(bizUser);
        }

        // POST: BizUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FullName,Password,Email,PhoneNumber,EmployeeType")] BizUser bizUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bizUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bizUser);
        }

        // GET: BizUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BizUser bizUser = db.BizUsers.Find(id);
            if (bizUser == null)
            {
                return HttpNotFound();
            }
            return View(bizUser);
        }

        // POST: BizUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BizUser bizUser = db.BizUsers.Find(id);
            db.BizUsers.Remove(bizUser);
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
