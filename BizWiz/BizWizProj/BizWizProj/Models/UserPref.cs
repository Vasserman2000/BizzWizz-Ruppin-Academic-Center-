using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BizWizProj.Models
{
    public class UserPref
    {
        [Key]
        public int ID { get; set; }

        public int UserID { get; set; }
        public string UserName { get; set; }
        public int Preference { get; set; }
        public bool IsManager { get; set; }
    }
}
