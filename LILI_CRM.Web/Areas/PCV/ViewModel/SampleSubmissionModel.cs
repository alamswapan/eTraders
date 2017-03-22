using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class SampleSubmissionModel : BaseViewModel
    {
        public SampleSubmissionModel()
        {
            SampleSubmissionDetailModel = new List<SampleSubmissionDetailModel>();
            sampleDocumentModels = new List<SampleDocumentModel>();
            sampleSubmissionDocumentModel = new List<SampleSubmissionDocumentModel>();
        }


        [DisplayName("Product")]
        public string Product { get; set; }

        [DisplayName("Submission No")]
        public string SubmissionNo { get; set; }


        [DisplayName("Submission Date")]
        [Required(ErrorMessage = "Submission Date is required")]
        [UIHint("_Date")]
        public DateTime? SubmissionDate { get; set; }

        [DisplayName("Organization")]
        public long OrganizationId { get; set; }
        public string Organization { get; set; }

        [DisplayName("Customer")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Customer is Required")]
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }

        [DisplayName("Attention")]
        //[Range(1, Int32.MaxValue, ErrorMessage = "Customer is Required")]
        public long CusContactId { get; set; }
        public string ContactsName { get; set; }
        public string Designation { get; set; }

        [DisplayName("Submission Text")]
        public string SubmissionText { get; set; }


        public IEnumerable<SelectListItem> ddlOrganization { get; set; }
        public IEnumerable<SelectListItem> ddlCustomer { get; set; }
        public IEnumerable<SelectListItem> ddlContactPerson{ get; set; }
        public List<SampleSubmissionDetailModel> SampleSubmissionDetailModel { get; set; }
        public List<SampleDocumentModel> sampleDocumentModels { get; set; }
        public List<SampleSubmissionDocumentModel> sampleSubmissionDocumentModel { get; set; } 
    }

    public class SampleSubmissionDetailModel : BaseViewModel
    {
        public SampleSubmissionDetailModel()
        { }

        public long SubmissionId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string SamplingUnit { get; set; }
        public decimal SubmissionQuantity { get; set; }
        public DateTime? ExpieryDate { get; set; }
        public string Purpose { get; set; }
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Origin { get; set; }
        public string TrialReport { get; set; }
        public int ApproveState { get; set; }
        public string DetailText { get; set; }
    }

    public class SampleSubmissionDetailViewModel : BaseViewModel
    {
        public SampleSubmissionDetailViewModel()
        { }

        public long SubmissionId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string SamplingUnit { get; set; }
        public float SubmissionQuantity { get; set; }
        public DateTime? ExpieryDate { get; set; }
        public string Purpose { get; set; }
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Origin { get; set; }
        public string TrialReport { get; set; }
        public int ApproveState { get; set; }
        public string DetailText { get; set; }

    }

    public class SampleSubmissionDocumentModel : BaseViewModel
    {
        public SampleSubmissionDocumentModel()
        { }

        public long SubmissionId { get; set; }
        public int DocumentId { get; set; }
    }
}