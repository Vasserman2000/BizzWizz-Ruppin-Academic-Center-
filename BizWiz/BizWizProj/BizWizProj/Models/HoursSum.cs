
using BizWizProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BizWizProj.Context;

using System.ComponentModel.DataAnnotations;


namespace BizWizProj.Models
{
    public class HoursSum
    {
        static public int IniHours(DB db, BizUser user)
        {
            var counter = 0;
            var currentDay = DateTime.Now.Day;
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            List<ClosedShift> shiftList;
            if (user.EmployeeType == BizWizProj.Models.EmployeeType.Employee)
            {
                shiftList = db.ShiftHistory
                           .Where(s => s.Start.Month == currentMonth)
                           .Where(s => s.Start.Year == currentYear)
                           .Where(s => s.Start.Day <= currentDay)
                           .Where(s => s.Workers.Select(w => w.userID.ToString()).Contains(user.ID.ToString()))
               .ToList();
            }
            else
            {
                shiftList = db.ShiftHistory
                          .Where(s => s.Start.Month == currentMonth)
                          .Where(s => s.Start.Year == currentYear)
                          .Where(s => s.Start.Day <= currentDay)
                          .Where(s => s.ShiftManager.ID==user.ID)
              .ToList();
            }
           


            foreach (var shift in shiftList)
            {
                if ((shift.End.Hour - shift.Start.Hour) < 0)
                    counter += shift.End.Hour - shift.Start.Hour + 24;
                else
                    counter += shift.End.Hour - shift.Start.Hour;
            }
            
            return counter;
        }

        static public List<UserHours> AllWorkers(DB db)
        {
            var counter = 0;
            String WorkesrHours = "";
            List<BizUser> WorkersList = db.BizUsers.ToList();
            List<UserHours> lst1 = new List<UserHours>();

            if (WorkersList != null)
                foreach (var s_item in WorkersList)
                {
                    counter = IniHours(db, s_item);
                    WorkesrHours += s_item.FullName + " " + counter.ToString() + " MH. ";
                    lst1.Add(new UserHours() { User = s_item, Hours = counter });
                }
            else
                return null;

            return lst1;

        }


    }
}

