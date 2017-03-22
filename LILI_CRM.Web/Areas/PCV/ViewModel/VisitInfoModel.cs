using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class VisitInfoModel : BaseViewModel
    {
        [DisplayName("Preparation Date")]
        [Required(ErrorMessage = "Preparation Date is required.")]
        [UIHint("_Date")]
        public DateTime PreparationDate { get; set; }

        [DisplayName("Sales Person")]
        [Required(ErrorMessage = "Sales Person is required.")]
        [MaxLength(50, ErrorMessage = "Exceeding maximum length.")]
        public string SalesPerson { get; set; }

        [DisplayName("Point Of Discussion")]
        [MaxLength(250, ErrorMessage = "Exceeding maximum length.")]
        public string PointOfDiscussion { get; set; }

        [DisplayName("Details")]
        [MaxLength(250, ErrorMessage = "Exceeding maximum length.")]
        public string Details { get; set; }

        [DisplayName("Remarks")]
        [MaxLength(250, ErrorMessage = "Exceeding maximum length.")]
        public string Remarks { get; set; }

    }
}