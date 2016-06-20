using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BizWizProj.Models
{
    public class BizUser
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "This field can not be empty.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "This field can not be empty.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "This field can not be empty.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field can not be empty.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "This field can not be empty.")]
        public EmployeeType EmployeeType { get; set; }
    }

    public enum EmployeeType
    {
        Employee,ShiftManager, SuperShiftManager,Manager
    }
}