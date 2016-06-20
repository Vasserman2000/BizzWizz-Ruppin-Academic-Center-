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
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Events.Calendar;
using DayPilot.Web.Mvc.Enums;
using BizWizProj.Authorization;

namespace BizWizProj.Controllers
{
    [MyAuthorize]
    public class ModelShiftsController : Controller
    {
        public ActionResult Backend()
        {
            return new Dpc().CallBack(this);
        }

        class Dpc : DayPilotCalendar
        {
            private DB dc = new DB();
            protected override void OnCommand(CommandArgs e)
            {
                switch (e.Command)
                {
                    case "refresh":
                        Update();
                        break;
                    case "initNav":
                        StartDate = new DateTime(2016, 05, 01);
                        Update(CallBackUpdateType.Full);
                        break;
                }
            }

            protected override void OnBeforeHeaderRender(BeforeHeaderRenderArgs e)
            {
                e.InnerHtml = e.Date.DayOfWeek.ToString();
            }

            protected override void OnEventDelete(EventDeleteArgs e)
            {
                int Id = Convert.ToInt32(e.Id);
                var item = (from ev in dc.ModelShifts where ev.ID == Id select ev).First();

                dc.ModelShifts.Remove(item);
                dc.SaveChanges();
                Update();
            }

            protected override void OnFinish()
            {
                if (UpdateType == CallBackUpdateType.None)
                    return;
                DataIdField = "Id";
                DataStartField = "Start";
                DataEndField = "End";
                DataTextField = "Text";
                Events = from e in dc.ModelShifts select e;
            }
        }


        private DB db = new DB();

        // GET: ModelShifts
        public ActionResult Index()
        {
            return View(db.ModelShifts.ToList());
        }

        // GET: ModelShifts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelShift ModelShift = db.ModelShifts.Find(id);
            if (ModelShift == null)
            {
                return HttpNotFound();
            }
            return View(ModelShift);
        }

        // GET: ModelShifts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModelShifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NumOfEmployees,Start,End")] ModelShift ModelShift)
        {
            if (ModelState.IsValid)
            {
                db.ModelShifts.Add(ModelShift);
                db.SaveChanges();

                return View("SucModelShift");
            }

            return View(ModelShift);
        }

        // GET: ModelShifts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelShift ModelShift = db.ModelShifts.Find(id);
            if (ModelShift == null)
            {
                return HttpNotFound();
            }
            return View(ModelShift);
        }

        // POST: ModelShifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NumOfEmployees,Start,End")] ModelShift ModelShift)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ModelShift).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ModelShift);
        }

        // GET: ModelShifts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelShift ModelShift = db.ModelShifts.Find(id);
            if (ModelShift == null)
            {
                return HttpNotFound();
            }
            return View(ModelShift);
        }

        // POST: ModelShifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ModelShift ModelShift = db.ModelShifts.Find(id);
            db.ModelShifts.Remove(ModelShift);
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