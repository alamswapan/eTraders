using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class PriceRequestModel :BaseViewModel
    {
        public PriceRequestModel()
        {
            //PriceRequestModel = new List<SalesBudgetDetailViewModel>();
        }

        [DisplayName("Sales Call ID")]
        public long SalesCallId { get; set; }

        [DisplayName("Date of Submission")]
        [Required(ErrorMessage = "Submission Date is required")]
        [UIHint("_Date")]
        public DateTime? PriceRequestDate { get; set; }

        [DisplayName("Request To suppier")]
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }

        [DisplayName("For Customer")]
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }

        [DisplayName("Article Code")]
        public string ArticleCode { get; set; }

       

        public IEnumerable<SelectListItem> ddlSupplier { get; set; }
        public IEnumerable<SelectListItem> ddlCustomer { get; set; }

    }

    public class PriceRequestDetailViewModel : BaseViewModel
    {
        public PriceRequestDetailViewModel()
        {            
        }

        public long ArticleId { get; set; }
        public string ArticleName { get; set; }

        public long PriceRequestId { get; set; }
        public long ProductId { get; set; }
        public long PriceTermId { get; set; }

        public decimal ExpectedPrice { get; set; }
        public decimal ReceivedPrice { get; set; }
        public DateTime? AsOnDate { get; set; }
        public DateTime? ValidityDate { get; set; }
        public decimal MOQ { get; set; }
        public long ShipmentCountryId { get; set; }
        public string ShipmentLeadTime { get; set; }
        public string PriceRequestStatus { get; set; }

    }

    public class PriceTermModel : BaseViewModel
    {
        public long PriceTermId { get; set; }
        public string PriceTermName { get; set; }
    }

    public class CountryViewModel : BaseViewModel
    {
        public long ShipmentCountryId { get; set; }
        public string CountryName { get; set; }
    }


    

}