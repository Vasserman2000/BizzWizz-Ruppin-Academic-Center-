using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BizWizProj.Models
{
    public class StockItem
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public string Notes { get; set; }
    }
}