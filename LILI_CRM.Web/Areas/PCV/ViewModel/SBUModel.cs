using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class SBUModel : BaseViewModel
    {

        [DisplayName("SBU Name")]
        [Required(ErrorMessage = "SBUt Name is required.")]
        [MaxLength(120, ErrorMessage = "Exceeding maximum length.")]
        public string SBUName { get; set; }

        [DisplayName("Descriptions")]
        public string Descriptions { get; set; }

   }
}