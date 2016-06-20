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
using System.Globalization;

namespace BizWizProj.Controllers
{
    [MyAuthorize]
    public class ClosedShiftsController : Controller
    {
        //Avi-Searcing for Employees That is not Working on Some Shift 
        #region
        public static int ShiftId;
        public void idSave (int shiftid)
        {
            ShiftId = shiftid;
        }
        public string OpenModelPopup()
        {
            List<BizUser> BizzUsersList = db.BizUsers.ToList(); //loading all Users from db 
            if (!BizzUsersList.Any())
                return "";
            List<BizUser> NotWorkingUsers = new List<BizUser>(); //List of the Workers That not work on this Shift
            ClosedShift ThisShift = db.ShiftHistory.Find(ShiftId);// Loading this Shift from db
            bool flag = false;

            foreach (BizUser user in BizzUsersList)
            {
                foreach (Worker worker in ThisShift.Workers)
                {
                    if (user.ID == worker.userID)
                        flag = true;
                }
                if (!flag)
                    NotWorkingUsers.Add(user);
                flag = false;
            }
            string workersNames = "<br><br>";
            if (!NotWorkingUsers.Any())
                return workersNames;
            int num = 1;
            foreach (BizUser worker in NotWorkingUsers)
            {
                workersNames += num + ") Name: " + worker.FullName + " Email: " + worker.Email +"<br>";
                num++;
            }
            return workersNames;
        }
        #endregion

        public ActionResult PopScreen()
        {
            ViewBag.HtmlStr = OpenModelPopup();
            return View();
        }


        public ClosedShiftsController()
        {
            db.Database.CreateIfNotExists();
        }
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
                    case "today":
                        StartDate = DateTime.Today;
                        Update(CallBackUpdateType.Full);
                        break;
                    case "navigate":
                        Update(CallBackUpdateType.Full);
                        break;
                }
            }

            protected override void OnBeforeHeaderRender(BeforeHeaderRenderArgs e)
            {
                e.InnerHtml = e.Date.DayOfWeek.ToString() + " " + e.Date.ToShortDateString();
            }

            protected override void OnFinish()
            {

                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                DataIdField = "Id";
                DataStartField = "Start";
                DataEndField = "End";
                DataTextField = "Text";

                Events = from e in dc.ShiftHistory select e;
            }
        }

        private DB db = new DB();

        // GET: ClosedShifts
        public ActionResult Index()
        {
            //List of workers and mounthly working hours
            List<UserHours> lst1 = HoursSum.AllWorkers(db);

            UserHours UserHt = new UserHours();

            ViewBag.UsersHours = lst1;

            BizUser thisUser = Session["user"] as BizWizProj.Models.BizUser;
            UserHt.User = thisUser;
            foreach (var user in lst1)
                if (user.User.ID.Equals(thisUser.ID))
                {
                    UserHt = user;
                    break;
                }
            ViewBag.CurrentUser = UserHt;

            /* Start of System notices part */
            ViewBag.NoticesForMe = ""; //declaring an empty variable in ViewBag
            /*Declaring an empty list (that will hold relevant for every employee
            type notices, which will be sent then to an index view of ClosedShifts)*/
            List<BizWizProj.Models.SystemNotices> notices; 
            switch (((HttpContext.Session["user"] as BizUser).EmployeeType).ToString())
            {
                /*Manager can view all notices in the system*/
                case ("Manager"):
                    notices = (from notif in db.Notices.ToList() where (DateOk(Convert.ToDateTime(notif.Date))) select notif).ToList();
                    notices.Reverse();
                    ViewBag.NoticesForMe = notices;
                    break;
                /*SSM can view all notices except those which were assigned to users of type Manager*/
                case ("SuperShiftManager"):
                    notices = (from notif in db.Notices.ToList() where (notif.To.ToString() != "Manager" && DateOk(Convert.ToDateTime(notif.Date))) select notif).ToList();
                    notices.Reverse();
                    ViewBag.NoticesForMe = notices;
                    break;
                /*SM can view all notices except those which were assigned to users of type Manager and SSM*/
                case ("ShiftManager"):
                    notices = (from notif in db.Notices.ToList() where (notif.To.ToString() != "Manager" && notif.To.ToString() != "SuperShiftManager" && DateOk(Convert.ToDateTime(notif.Date))) select notif).ToList();
                    notices.Reverse();
                    ViewBag.NoticesForMe = notices;
                    break;
                /*Users of employee type 'Employee' can view notices assigned to them only*/
                case ("Employee"):
                    notices = (from notif in db.Notices.ToList() where (notif.To.Equals(((HttpContext.Session["user"] as BizUser).EmployeeType)) && DateOk(Convert.ToDateTime(notif.Date))) select notif).ToList();
                    notices.Reverse();
                    ViewBag.NoticesForMe = notices;
                    break;
            }

            /* End of System notices part */

            return View(db.ShiftHistory.ToList());
        }

        //the function below is checking if a notice is out of date (i.e: older than 14 days)
        public bool DateOk(DateTime d)
        {
            DateTime now = DateTime.Now;
            if ((now - d).TotalDays < 14)
                return true;
            return false;
        }

        // GET: ClosedShifts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClosedShift ClosedShift = db.ShiftHistory.Find(id);
            if (ClosedShift == null)
            {
                return HttpNotFound();
            }
            return View(ClosedShift);
        }

        // GET: ClosedShifts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClosedShifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Start,End")] ClosedShift ClosedShift)
        {
            if (ModelState.IsValid)
            {
                db.ShiftHistory.Add(ClosedShift);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ClosedShift);
        }

        // GET: ClosedShifts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClosedShift ClosedShift = db.ShiftHistory.Find(id);
            if (ClosedShift == null)
            {
                return HttpNotFound();
            }
            return View(ClosedShift);
        }

        // POST: ClosedShifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Start,End")] ClosedShift ClosedShift)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ClosedShift).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ClosedShift);
        }

        // GET: ClosedShifts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClosedShift ClosedShift = db.ShiftHistory.Find(id);
            if (ClosedShift == null)
            {
                return HttpNotFound();
            }
            return View(ClosedShift);
        }

        // POST: ClosedShifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClosedShift ClosedShift = db.ShiftHistory.Find(id);
            db.ShiftHistory.Remove(ClosedShift);
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
