using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class DivisionModel : BaseViewModel
    {
        [DisplayName("Division Name")]
        [Required(ErrorMessage = "Division Name is required.")]
        public string DivisionName { get; set; }

        [DisplayName("Descriptions")]
        [MaxLength(250, ErrorMessage = "Descriptions maximum length.")]
        public string Descriptions { get; set; } 


    }
}