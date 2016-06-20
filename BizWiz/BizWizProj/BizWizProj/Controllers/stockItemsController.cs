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
    public class StockItemsController : Controller
    {
        private DB db = new DB();

        // GET: StockItems
        public ActionResult Index()
        {
            return View(db.Stocks.ToList());
        }

        // GET: StockItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockItem StockItem = db.Stocks.Find(id);
            if (StockItem == null)
            {
                return HttpNotFound();
            }
            return View(StockItem);
        }

        // GET: StockItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StockItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Quantity,Category,Notes")] StockItem StockItem)
        {
            if (ModelState.IsValid)
            {
                db.Stocks.Add(StockItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(StockItem);
        }

        // GET: StockItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockItem StockItem = db.Stocks.Find(id);
            if (StockItem == null)
            {
                return HttpNotFound();
            }
            return View(StockItem);
        }

        // POST: StockItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Quantity,Category,Notes")] StockItem StockItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(StockItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(StockItem);
        }

        // GET: StockItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockItem StockItem = db.Stocks.Find(id);
            if (StockItem == null)
            {
                return HttpNotFound();
            }
            return View(StockItem);
        }

        // POST: StockItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockItem StockItem = db.Stocks.Find(id);
            db.Stocks.Remove(StockItem);
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
