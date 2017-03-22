using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class InvoiceModel : BaseViewModel
    {
        public InvoiceModel()
        {
            InvoiceDetailModel = new List<InvoiceDetailModel>();
        }

        [DisplayName("Product")]
        public string Product { get; set; }

        [DisplayName("Invoice No")]
        //[Required(ErrorMessage = "Invoice No is required.")]
        //[MaxLength(15, ErrorMessage= "Exceeding maximum length.")]
        public string InvoiceNo { get; set; }

        public int InvoicePeriod { get; set; }

        [DisplayName("Invoice Date")]
        [Required(ErrorMessage = "Invoice Date is required")]
        [UIHint("_Date")]
        public DateTime? InvoiceDate { get; set; }

        [DisplayName("Invoice Type")]
        public int InvoiceTypeId { get; set; }
        public string InvoiceType { get; set; }
        
        [DisplayName("Organization")]
        //[Range(1, int.MaxValue, ErrorMessage = "Exceeding maximum length.")]
        public long OrganizationId { get; set; }
        public string Organization { get; set; }

        [DisplayName("Customer")]
        [Range(1, int.MaxValue, ErrorMessage = "Customer is required")]
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }

        [DisplayName("Address")]
        public string CustomerAddress { get; set; }
        
        [DisplayName("Phone")]
        public string Phone { get; set; }
        
        [DisplayName("Email")]
        public string Email { get; set; }
        
        [DisplayName("Tin")]
        public string TIN { get; set; }
        
        [DisplayName("Bin")]
        public string BIN { get; set; }

        [DisplayName("Currency")]
        [Range(1, int.MaxValue, ErrorMessage = "Currency is required")]
        public long CurrencyId { get; set; }
        public string Currency { get; set; }

        [DisplayName("Price Terms")]
        [Range(1, int.MaxValue, ErrorMessage = "Price Term is required")]
        public long PriceTermId { get; set; }
        public string PriceTermName { get; set; }



        [DisplayName("Total")]
        public decimal Total { get; set; }
        
        //[DisplayName("Discount")]
        //public decimal Discount { get; set; }
        
        [DisplayName("Other Charges")]
        [Range(0, int.MaxValue, ErrorMessage = "Invalid Data")]
        public decimal OtherCharges { get; set; }
        
        [DisplayName("Grand Total")]
        public decimal NET { get; set; }

        [DisplayName("Shipment from")]
        [Range(1, int.MaxValue, ErrorMessage = "Shipment from is required")]
        public long ShipmentFromCountryId { get; set; }
        public string ShipmentCountryName { get; set; }

        [DisplayName("Port of Discharge")]
        [Range(1, int.MaxValue, ErrorMessage = "Port of Discharge is required")]
        public long PortOfDischargeId { get; set; }
        public string PortName { get; set; }

        [DisplayName("Commision Calculating Type")]
        [Range(1, int.MaxValue, ErrorMessage = "Commision Calculating Type is required")]
        public long CommCalcTermId { get; set; }
        public string CommissionCalculationTerm { get; set; }

        [DisplayName("Commission Percent")]
        public decimal CommissionPer { get; set; }

        [DisplayName("Commission")]
        public decimal Commission { get; set; }

        public string Paid { get; set; }
        public string Remarks { get; set; }

        [DisplayName("LC No")]
        public string LCNo { get; set; }

        [DisplayName("LC Date")]
        [Required(ErrorMessage = "LC Date is required")]
        [UIHint("_Date")]
        public DateTime? LCDate { get; set; }

        [DisplayName("LDE")]
        [Required(ErrorMessage = "LDE Date is required")]
        [UIHint("_Date")]
        public DateTime? LDE { get; set; }

        [DisplayName("LDS")]
        [Required(ErrorMessage = "LDS Date is required")]
        [UIHint("_Date")]
        public DateTime? LDS { get; set; }

        [DisplayName("Expected Date of Shipment ")]
        [Required(ErrorMessage = "Shipment Date is required")]
        [UIHint("_Date")]
        public DateTime? ShipmentDate { get; set; }

        [DisplayName("Expected Date of Arrival")]
        [Required(ErrorMessage = "Arrival Date is required")]
        [UIHint("_Date")]
        public DateTime? ArrivalDate { get; set; }

        [DisplayName("Commercial Invoice No")]
        public string CommercialInvoiceNo { get; set; }

        [DisplayName("Commercial Invoice Date")]
        [Required(ErrorMessage = "Commercial Invoice Date is required")]
        [UIHint("_Date")]
        public DateTime? CommercialInvoiceDate { get; set; }

        [DisplayName("Bill of Lading No #")]
        public string BoLNo { get; set; }

        
        public IEnumerable<SelectListItem> ddlInvoiceType { get; set; }
        public IEnumerable<SelectListItem> ddlOrganization { get; set; }
        public IEnumerable<SelectListItem> ddlCustomer { get; set; }
        public IEnumerable<SelectListItem> ddlCurrency { get; set; }
        public IEnumerable<SelectListItem> ddlPriceTerm { get; set; }
        public IEnumerable<SelectListItem> ddlCommissionTerm { get; set; }
        public IEnumerable<SelectListItem> ddlShipmentCountry { get; set; }
        public IEnumerable<SelectListItem> ddlPortOfDischarge { get; set; }
        public List<InvoiceDetailModel> InvoiceDetailModel { get; set; }
    }

    public class InvoiceDetailModel : BaseViewModel
    {
        public InvoiceDetailModel()
        { }

        public long InvoiceId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string GenericName { get; set; }
        public string HSCode { get; set; }
        public string Origin { get; set; }
        public string SellingUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public IEnumerable<SelectListItem> ddlProduct { get; set; }
    }
    public class InvoiceDetailViewModel : BaseViewModel
    {
        public InvoiceDetailViewModel()
        { }

        public long InvoiceId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string GenericName { get; set; }
        public string HSCode { get; set; }
        public string Origin { get; set; }
        public string SellingUnit { get; set; }
        public float UnitPrice { get; set; }
        public float Quantity { get; set; }
        public float SubTotal { get; set; }
    }
}