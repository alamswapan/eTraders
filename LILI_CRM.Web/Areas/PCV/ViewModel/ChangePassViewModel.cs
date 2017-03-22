using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Globalization;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class ChangePassViewModel : BaseViewModel
    {
        public ChangePassViewModel()
        {

        }
        
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("User Id")]
        public string UserId { get; set; }

        [DisplayName("Old Password")]
        [Required(ErrorMessage = "Old Password is required")]
        public string OldPassword { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [DisplayName("Re-type Password")]
        [Required(ErrorMessage = "Re-type Password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public DateTime? JoiningDate { get; set; }
        public string Designation { get; set; }
        public bool grpAdd { get; set; }
        public bool grpSup { get; set; }
        public bool grpISup { get; set; }
        public bool grpUser { get; set; }
        public string Active { get; set; }
        public string InvoiceFormat { get; set; }
        public string DefaultBusiness { get; set; }
        public string UserLevel { get; set; }
        public string LevelCode { get; set; }
        public string Subbusinesscode { get; set; }
        public string staffid { get; set; }
        public string UserLocation { get; set; }
        public string UserLevelMKT { get; set; }

        
    }
}