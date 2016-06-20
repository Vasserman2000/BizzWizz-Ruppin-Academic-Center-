using BizWizProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace BizWizProj.Authorization
{
    class MyAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            httpContext.Session["returnUrl"] = httpContext.Request.RawUrl; //when attempting
            //to visit a page which requests to be authorized in order to enter it, then a
            //user will be redirected to "/login/login" while the return URL will be saved in
            //Session["returnUrl"] and after successful login a user will be redirected back to 
            //(previously unauthorised) requested page. This will happen in LoginController
            //in this line:
            // return Redirect(Session["returnUrl"].ToString());
            if (httpContext.Session["user"] == null)
                return false;
            return true;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Session["user"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                                  new RouteValueDictionary
                                   {
                                       { "action", "Login" },
                                       { "controller", "Login" }
                                   });
            }
        }
    }
}