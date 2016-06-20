using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BizWizProj.Models
{

    public class SendEMailViewModel
    {
        /* replace the placeholder in our template*/
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class AccountViewModels
    {

    }
}