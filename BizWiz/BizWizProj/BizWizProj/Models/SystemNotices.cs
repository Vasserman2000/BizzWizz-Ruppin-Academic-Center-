using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BizWizProj.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace BizWizProj.Models
{
    public class SystemNotices
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "This field can not be empty.")]
        public string Subject { get; set; } //title of the notice
        [Required(ErrorMessage = "This field can not be empty.")]
        public string Text { get; set; }   //body message
        public string Date { get; set; }
        public string From { get; set; }
        public EmployeeType To { get; set; }

        public SystemNotices()
        {
            this.Date = DateTime.Now.Date.ToShortDateString();
            this.From = (HttpContext.Current.Session["user"] as BizUser).FullName;
        }
    }
}