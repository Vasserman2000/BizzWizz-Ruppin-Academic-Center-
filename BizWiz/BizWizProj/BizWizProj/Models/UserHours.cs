using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizWizProj.Models
{
    public class UserHours
    {
        public BizUser User { get; set; }
        public int Hours { get; set; }
    }
}