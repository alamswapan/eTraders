using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class CallReportSalesCallInquiryInfoModel: BaseViewModel
    {

        public CallReportSalesCallInquiryInfoModel()
        { 

        }

        [DisplayName("Customer")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select one")]
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        
        [DisplayName("Supplier")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select one")]
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }

        [DisplayName("Date Of Update")]
        [UIHint("_Date")]
        public DateTime DateOfUpdate { get; set; }

        [DisplayName("Sales Person")]
        [Required(ErrorMessage = "Sales Person is required.")]
        [MaxLength(50, ErrorMessage = "Exceeding maximum length.")]
        public string SalesPerson { get; set; }

        [DisplayName("Product")]
        public string ProductCode { get; set; }
        public string ProductName { get; set; }

        public IEnumerable<SelectListItem> ddlCustomer { get; set; }
        public IEnumerable<SelectListItem> ddlSupplier { get; set; }

        public IEnumerable<SalesCallInquiryDetailViewModel> salesCallInquiryDetailViewModel { get; set; }
    }


    public class SalesCallInquiryDetailViewModel : BaseViewModel
    {
        public SalesCallInquiryDetailViewModel()
        {
            this.CauseList = new List<CurrentStageViewModel>();
        }
        public long SalesCallInquiryId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? Quantity { get; set; }
        public string Unit { get; set; }
        public decimal? OfferedAmountPerUnit { get; set; }
        public string PriceValidity { get; set; }
        public decimal? BidPrice { get; set; }
        public long StageId { get; set; }
        public string StageName { get; set; }
        public string CurrentStage { get; set; }
        public string Remarks { get; set; }

        public long StageIdTemp { get; set; }

        public string Causes { get; set; }

        [UIHint("CauseListEditor")]
        public IEnumerable<CurrentStageViewModel> CauseList { get; set; }
    }


    public class SalesCallInquiryEntityViewModel : BaseViewModel
    {
        public SalesCallInquiryEntityViewModel()
        {

        }
        public long SalesCallInquiryId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
        public float OfferedAmountPerUnit { get; set; }
        public string PriceValidity { get; set; }
        public float BidPrice { get; set; }
        public long StageId { get; set; }
        public string StageName { get; set; }
        public string Remarks { get; set; }

        public long StageIdTemp { get; set; }

        public string Causes { get; set; }

        [UIHint("CauseListEditor")]
        public IEnumerable<CurrentStageViewModel> CauseList { get; set; }
    }


    public class CurrentStageViewModel
    {
        public long StageId { get; set; }
        public string StageName { get; set; }
    }
    

    
}