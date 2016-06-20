using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BizWizProj.Models
{
    public class ModelShift
    {
        [Key]
        public int ID { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "At least one employee required")]
        public int NumOfEmployees { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        //Text to display Workers
        public string Text { get; set; }
    }
}