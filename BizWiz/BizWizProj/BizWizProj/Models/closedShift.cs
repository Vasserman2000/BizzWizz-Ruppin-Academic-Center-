using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BizWizProj.Models
{
    public class ClosedShift
    {
        [Key]
        public int ID { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public virtual BizUser ShiftManager { get; set; }
        public virtual ICollection<Worker> Workers { get; set; }
        //Text to display Workers
        public string Text { get; set; }
    }
}