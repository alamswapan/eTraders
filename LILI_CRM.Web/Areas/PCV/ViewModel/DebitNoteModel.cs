using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class DebitNoteModel : BaseViewModel
    {
        public DebitNoteModel()
        {
            InvoiceDetailModel = new List<InvoiceDetailModel>();
        }

        //[DisplayName("Product")]
        //public string Product { get; set; }

        [DisplayName("Debit Note No")]
        [Required(ErrorMessage = "Invoice No is required.")]
        [MaxLength(15, ErrorMessage= "Exceeding maximum length.")]
        public string DebitNoteNo { get; set; }


        [DisplayName("Debit Note Date")]
        [Required(ErrorMessage = "Debit Note Date is required")]
        [UIHint("_Date")]
        public DateTime? DebitNoteDate { get; set; }

        [DisplayName("Supplier")]
        [Range(1, int.MaxValue, ErrorMessage = "Supplier is required")]
        public long SupplierId { get; set; }

        [DisplayName("Supplier Name")]
        public string SupplierName { get; set; }

        [DisplayName("Address ")]
        public string SupplierAddress { get; set; }

        [DisplayName("Attention Person")]
        public string AttentionPerson { get; set; }

        [DisplayName("Organization")]
        //[Range(1, int.MaxValue, ErrorMessage = "Exceeding maximum length.")]
        public long OrganizationId { get; set; }
        public string Organization { get; set; }

        [DisplayName("Bank")]
        //[Range(1, int.MaxValue, ErrorMessage = "Exceeding maximum length.")]
        public long BankId { get; set; }
        
        public string Bank { get; set; }

        [DisplayName("Bank Address")]
        public string BankAddress { get; set; }
        [DisplayName("Account No #")]
        public string AccountNo { get; set; }
        [DisplayName("Swift Code")]
        public string SwiftCode { get; set; }


        public decimal Total { get; set; }

        public IEnumerable<SelectListItem> ddlBank { get; set; }
        public IEnumerable<SelectListItem> ddlSupplier { get; set; }
        public IEnumerable<SelectListItem> ddlOrganization { get; set; }

        public List<InvoiceDetailModel> InvoiceDetailModel { get; set; }
    }

    public class DebitNoteDetailModel : BaseViewModel
    {
        public DebitNoteDetailModel()
        { }

        public long DebitNoteId { get; set; }

        public long InvoiceId { get; set; }
        public DateTime? InvoiceDate { get; set; }

        public string CommercialInvoiceNo { get; set; }
        public DateTime? CommercialInvoiceDate { get; set; }

        public long CustomerId { get; set; }
        public string CustomerName { get; set; }

        public decimal Quantity { get; set; }
        
        public long CommCalcTermId { get; set; }
        public string CommissionCalculationTerm { get; set; }

        public decimal CommissionPer { get; set; }

        public decimal Commission { get; set; }

        public string LCNo { get; set; }
        public DateTime? LCDate { get; set; }
    }

    public class DebitNoteDetailViewModel : BaseViewModel
    {
        public DebitNoteDetailViewModel()
        { }

        public long DebitNoteId { get; set; }

        public long InvoiceId { get; set; }
        public DateTime? InvoiceDate { get; set; }

        public string CommercialInvoiceNo { get; set; }
        public DateTime? CommercialInvoiceDate { get; set; }

        public long CustomerId { get; set; }
        public string CustomerName { get; set; }

        public decimal Quantity { get; set; }

        public long CommCalcTermId { get; set; }
        public string CommissionCalculationTerm { get; set; }

        public decimal CommissionPer { get; set; }

        public decimal Commission { get; set; }

        public string LCNo { get; set; }
        public DateTime? LCDate { get; set; }

    }

}