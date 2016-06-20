using BizWizProj.Authorization;
using BizWizProj.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace BizWizProj.Controllers
{
    [MyAuthorize]
    public class EmailController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<ActionResult> SendEmail(SendEMailViewModel model)
        {
            var message = await EMailTemplate("WelcomeEmail");
            message = message.Replace("@ViewBag.Name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.FirstName));
            await MessageServices.SendEmailAsync(model.Email, "Welcome!!", message);
            return View("EmailSent");
        }

        [HttpGet]
        [AllowAnonymous]

        public ActionResult EmailSent()
        {
            return View();
        }

        public static async Task<string> EMailTemplate (string template)
        {
            var templateFilePath = HostingEnvironment.MapPath("~/Content/templates/") + template + ".cshtml";
            StreamReader objstreamreaderfile = new StreamReader(templateFilePath); //reads chars. from a stream
            var body = await objstreamreaderfile.ReadToEndAsync();
            objstreamreaderfile.Close();
            return body;
        }
    }
}