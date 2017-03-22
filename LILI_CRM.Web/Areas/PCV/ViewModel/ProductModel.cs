using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class ProductModel: BaseViewModel
    {
        [ScaffoldColumn(false)]
        public string MaxProductCode { get; set; }

        [DisplayName("Product Code")]
        [Required(ErrorMessage = "Product Code is required.")]
        [MaxLength(10, ErrorMessage= "Exceeding maximum length.")]
        public string ProductCode { get; set; }

        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Productt Name is required.")]
        [MaxLength(120, ErrorMessage = "Exceeding maximum length.")]
        public string ProductName { get; set; }

        [DisplayName("Generic Name")]
        public string GenericName { get; set; }

        [DisplayName("Short Desc")]
        public string ShortDesc { get; set; }

        [DisplayName("HS Code")]
        public string HSCode { get; set; }

        [DisplayName("Selling Unit")]
        [Required(ErrorMessage = "Selling Unit is required.")]
        [MaxLength(20, ErrorMessage = "Exceeding maximum length.")]
        public string SellingUnit { get; set; }

        [DisplayName("Sampling Unit")]
        public string SamplingUnit { get; set; }
        
        [DisplayName("Unit Price")]
        [Required(ErrorMessage = "Unit Price is required.")]
        public decimal UnitPrice { get; set; }

        [DisplayName("Duty Structure")]
        [Required(ErrorMessage = "Duty Structure is required.")]
        public decimal DutyStructure { get; set; }
        
        [DisplayName("Origin")]
        public string Origin { get; set; }

        [DisplayName("LeadTime")]
        public string LeadTime { get; set; }

        [DisplayName("MOQ")]
        [Required(ErrorMessage = "MOQ is required.")]
        public decimal MOQ { get; set; }

        [DisplayName("Sub Category")]
        //[Range(1,int.MaxValue, ErrorMessage="Exceeding maximum length.")]
        public long SubCategoryId { get; set; }
        //public string SubCategory { get; set; }

        [DisplayName("Organization")]
        //[Range(1, int.MaxValue, ErrorMessage = "Exceeding maximum length.")]
        public long OrganizationId { get; set; }
        //public string Organization { get; set; }

        [DisplayName("Offered $/Unit")]
        public decimal OfferedAmountPerUnit { get; set; }

        [DisplayName("Bid Price")]
        public decimal BidPrice { get; set; }


        
        public IEnumerable<SelectListItem> ddlProductSubCategory { get; set; }
        public IEnumerable<SelectListItem> ddlOrganization { get; set; }

    }
}