﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BizWizProj.Models
{
    public class Worker
    {
        [Key]
        public int ID { get; set; }
        public int userID { get; set; }
    }
}