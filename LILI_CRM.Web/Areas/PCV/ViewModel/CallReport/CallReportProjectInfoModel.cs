using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class CallReportProjectInfoModel: BaseViewModel
    {

        public CallReportProjectInfoModel()
        { 

        }

        [DisplayName("Created On")]
        [Required(ErrorMessage = "Project Name is required.")]
        [UIHint("_Date")]
        public DateTime CreateDate { get; set; }

        [DisplayName("Project Name")]
        [Required(ErrorMessage = "Project Name is required.")]
        [MaxLength(250, ErrorMessage = "Exceeding maximum length.")]
        public string ProjectName { get; set; }

        [DisplayName("Current Stage")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select one")]
        public long CurrentStageId { get; set; }
        public string CurrentStage { get; set; }

        [DisplayName("Customer")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select one")]
        public long CustomerId { get; set; }
        
        [DisplayName("Supplier")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select one")]
        public long SupplierId { get; set; }

        [DisplayName("Description")]
        [MaxLength(500, ErrorMessage = "Exceeding maximum length.")]
        public string Description { get; set; }

        [DisplayName("Selling Opportunity(Items)")]
        [MaxLength(250, ErrorMessage = "Exceeding maximum length.")]
        public string SellingOpportunity { get; set; }

        [DisplayName("Pot Vol/Yr")]
        //[MaxLength(100, ErrorMessage = "Exceeding maximum length.")]
        public decimal? PotVolPerYear { get; set; }

        [DisplayName("Pot $/yr")]
        //[MaxLength(100, ErrorMessage = "Exceeding maximum length.")]
        public decimal? PotAmountPerYear { get; set; }

        [DisplayName("Remarks > Next Action")]
        [MaxLength(250, ErrorMessage = "Exceeding maximum length.")]
        public string RemarkNextAction { get; set; }

        [DisplayName("Communication Channel")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select one")]
        public long CommunicationChannelId { get; set; }

        [DisplayName("Sales Person")]
        [Required(ErrorMessage = "Sales Person is required.")]
        [MaxLength(50, ErrorMessage = "Exceeding maximum length.")]
        public string SalesPerson { get; set; }


        public IEnumerable<SelectListItem> ddlCurrentStage { get; set; }
        public IEnumerable<SelectListItem> ddlCustomer { get; set; }
        public IEnumerable<SelectListItem> ddlSupplier { get; set; }
        public IEnumerable<SelectListItem> ddlCommunicationChannel { get; set; }
    }

    

    
}