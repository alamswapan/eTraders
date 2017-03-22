using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class QuoteModel : BaseViewModel
    {
        public QuoteModel()
        {
            QuoteDetailModel = new List<QuoteDetailModel>();
            documentModels = new List<DocumentModel>();
            quoteDocumentModels = new List<QuoteDocumentModel>();
        }

        [DisplayName("Sales Call ID")]
        public long SalesCallId { get; set; }

        [DisplayName("Article Code")]
        public string ArticleCode { get; set; }

        [DisplayName("Quote No")]
        public string QuoteNo { get; set; }


        [DisplayName("Date of Submission")]
        [Required(ErrorMessage = "Quote Date is required")]
        [UIHint("_Date")]
        public DateTime? QuoteDate { get; set; }

        [DisplayName("Organization")]
        public long OrganizationId { get; set; }
        public string Organization { get; set; }

        [DisplayName("Submitted to")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Customer is Required")]
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }

        [DisplayName("Attention")]
        //[Range(1, Int32.MaxValue, ErrorMessage = "Customer is Required")]
        public long CusContactId { get; set; }
        public string ContactsName { get; set; }
        public string Designation { get; set; }

        [DisplayName("Quote Text")]
        public string QuoteText { get; set; }


        public IEnumerable<SelectListItem> ddlOrganization { get; set; }
        public IEnumerable<SelectListItem> ddlCustomer { get; set; }
        public IEnumerable<SelectListItem> ddlContactPerson{ get; set; }
        public List<QuoteDetailModel> QuoteDetailModel { get; set; }
        public List<DocumentModel> documentModels { get; set; }
        public List<QuoteDocumentModel> quoteDocumentModels { get; set; }
    }

    public class QuoteDetailModel : BaseViewModel
    {
        public QuoteDetailModel()
        { }

        public long QuoteId { get; set; }
        public long ArticleId { get; set; }
        public string ArticleCode { get; set; }
        public string ArticleName { get; set; }
        public string BaseUnit { get; set; }
        public decimal QuoteQuantity { get; set; }
        public decimal MOQ { get; set; }
        public DateTime? ValidityDate { get; set; }
        public decimal UnitPrice { get; set; }
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Origin { get; set; }
        public string ApproveState { get; set; }
        public string DetailText { get; set; }
        public long? PriceTermId { get; set; }
        public long PaymentTermId { get; set; }
        public string result { get; set; }
        public long ShipmentCountryId { get; set; }
        public string ShipmentLeadTime { get; set; }
    }

    public class QuoteDetailViewModel : BaseViewModel
    {
        public QuoteDetailViewModel()
        { }

        public long QuoteId { get; set; }
        public long ArticleId { get; set; }
        public string ArticleCode { get; set; }
        public string ArticleName { get; set; }
        public string BaseUnit { get; set; }
        public float QuoteQuantity { get; set; }
        public decimal MOQ { get; set; }
        public DateTime? ValidityDate { get; set; }
        public float UnitPrice { get; set; }
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Origin { get; set; }
        public string ApproveState { get; set; }
        public string DetailText { get; set; }
        public long PriceTermId { get; set; }
        public long PaymentTermId { get; set; }
        public string result { get; set; }
        public long ShipmentCountryId { get; set; }
        public string ShipmentLeadTime { get; set; }

    }

    //public class SupplierViewModel
    //{
    //    public long SupplierId { get; set; }
    //    public string SupplierName { get; set; }
    //}

    public class ApproveStateViewModel
    {
        public string ApproveStateId { get; set; }
        public string ApproveStateName { get; set; }
    }

    public class DocumentModel
    {
        public int Id { get; set; }
        public string DocName { get; set; }
        public bool IsSelected { get; set; }
    }

    //public class QuoteDocumentList
    //{
    //    public List<QuoteDocumentModel> sampleDocumentModels { get; set; }
    //}

    public class QuoteDocumentModel : BaseViewModel
    {
        public QuoteDocumentModel()
        { }

        public long QuoteId { get; set; }
        public int DocumentId { get; set; }
    }
}