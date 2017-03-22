using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class SampleRequestModel : BaseViewModel
    {
        public SampleRequestModel()
        {
            SampleRequestDetailModel = new List<SampleRequestDetailModel>();
            sampleDocumentModels = new List<SampleDocumentModel>();
            sampleRequestDocumentModel = new List<SampleRequestDocumentModel>();
        }

        [DisplayName("Product")]
        public string Product { get; set; }

        [DisplayName("Request No")]
        public string RequestNo { get; set; }


        [DisplayName("Request Date")]
        [Required(ErrorMessage = "Request Date is required")]
        [UIHint("_Date")]
        public DateTime? RequestDate { get; set; }

        [DisplayName("Organization")]
        public long OrganizationId { get; set; }
        public string Organization { get; set; }

        [DisplayName("Supplier")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Supplier is Required")]
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }

        [DisplayName("Expecting Date")]
        //[Required(ErrorMessage = "Expecting Date is required")]
        [UIHint("_Date")]
        public DateTime? ExpectingDate { get; set; }

        [DisplayName("Courier")]
        //[Range(1, int.MaxValue, ErrorMessage = "Exceeding maximum length.")]
        public int? TransporterId { get; set; }
        public string TransporterName { get; set; }

        [DisplayName("Doc Tracking  No")]
        public string DocTrackingNo { get; set; }

        [DisplayName("Status")]
        public int? DeliveryStateId { get; set; }
        public string DeliveryState { get; set; }

        [DisplayName("Remarks")]
        public string Remarks { get; set; }


        public IEnumerable<SelectListItem> ddlOrganization { get; set; }
        public IEnumerable<SelectListItem> ddlSupplier { get; set; }
        public IEnumerable<SelectListItem> ddlTransporter { get; set; }
        public IEnumerable<SelectListItem> ddlDeliveryState { get; set; }
        public List<SampleRequestDetailModel> SampleRequestDetailModel { get; set; }
        public List<SampleDocumentModel> sampleDocumentModels { get; set; }
        public List<SampleRequestDocumentModel> sampleRequestDocumentModel { get; set; } 
        
    }

    public class SampleRequestDetailModel : BaseViewModel
    {
        public SampleRequestDetailModel()
        { }

        public long RequestId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string SamplingUnit { get; set; }
        public decimal RequestQuantity { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Purpose { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string DetailText { get; set; }

        public IEnumerable<SelectListItem> ddlArticle { get; set; }
    }
    public class SampleRequestDetailViewModel : BaseViewModel
    {
        public SampleRequestDetailViewModel()
        { }

        public long RequestId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string SamplingUnit { get; set; }
        public float RequestQuantity { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Purpose { get; set; }
        public float ReceivedQuantity { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string DetailText { get; set; }

    }

    public class CustViewModel
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
    }

    public class SampleDocumentModel
    {
        public int Id { get; set; }
        public string DocName { get; set; }
        public bool IsSelected { get; set; }
    }

    //public class DocumentList
    //{
    //    public List<SampleDocumentModel>sampleDocumentModels { get; set; }
    //}

    public class SampleRequestDocumentModel : BaseViewModel
    {
        public SampleRequestDocumentModel()
        { }

        public long RequestId { get; set; }
        public int DocumentId { get; set; }
    }
}