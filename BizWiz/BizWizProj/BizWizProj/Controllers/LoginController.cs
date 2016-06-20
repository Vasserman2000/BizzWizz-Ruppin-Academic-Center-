using BizWizProj.Context;
using BizWizProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BizWizProj.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string name, string Password)
        {
            DB db = new DB();
            //Backdoor for first login
            if ("admin".Equals(name) && "admin".Equals(Password))
            {
                Session["user"] = new BizUser() { FullName = name, EmployeeType = EmployeeType.Manager };
                if (Session["returnUrl"]!=null)
                    return Redirect(Session["returnUrl"].ToString());

                return RedirectToAction("Index", "Home");
            }
            //for real users.
            List<BizUser> userList = db.BizUsers.ToList();
            foreach (var item in userList)
            {
                if (item.Email.Equals(name) && item.Password.Equals(Password))
                {
                    Session["user"] = new BizUser()
                    {
                        FullName = item.FullName,
                        Email = item.Email,
                        EmployeeType = item.EmployeeType,
                        ID = item.ID
                    };
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}