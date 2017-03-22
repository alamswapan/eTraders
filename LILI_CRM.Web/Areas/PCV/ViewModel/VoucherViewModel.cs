using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Globalization;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class VoucherViewModel : BaseViewModel
    {
        public VoucherViewModel()
        {
            VoucherDetailEntityModel = new List<VoucherDetailEntityModel>();
        }
        

        //[Required(ErrorMessage = "Voucher No. is Required")]
        [DisplayName("Voucher No")]
        public long VoucherNo { get; set; }
        //public int VoucherNo { get; set; }

        public string CompanyCode { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum length is 50")]
        [Required(ErrorMessage = "Pay To is Required")]
        [DisplayName("Pay To")]
        public string PayTo { get; set; }

        
        [MaxLength(50, ErrorMessage = "Maximum length is 50")]
        [Required(ErrorMessage = "Staff ID is Required")]
        [DisplayName("Staff ID")]
        public string StaffID { get; set; }

        [DisplayName("Voucher Date")]
        [Required(ErrorMessage = "Voucher Date is required")]
        [UIHint("_Date")]
        public DateTime? VoucherDate { get; set; }

        public DateTime TransDate { get; set; }
        public string UserId { get; set; }
        public string Posted { get; set; }

        public string BusiNo { get; set; }
        public string Dept { get; set; }

        public int Period { get; set; }
        public string JVNo { get; set; }


        public List<VoucherDetailEntityModel> VoucherDetailEntityModel
        {
            get;
            set;
        }
    }

    public class VoucherDetailViewModel : BaseViewModel
    {
        public VoucherDetailViewModel()
        {
            
        }
        public long VoucherNo { get; set; }
        public string AMCode { get; set; }
        public string AMDetails { get; set; }
        public string ASCode { get; set; }
        public string Narration { get; set; }
        public float Debit { get; set; }
        public float Credit { get; set; }

        public string Category { get; set; }
    }


    public class VoucherDetailEntityModel : BaseViewModel
    {
        public VoucherDetailEntityModel()
        {

        }
        public long VoucherNo { get; set; }
        public string AMCode { get; set; }
        public string ASCode { get; set; }
        public string Narration { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }

        public string Category { get; set; }
    }

    public class VoucherDebitACModel 
    {
        //public VoucherDebitACModel()
        //{

        //}
        public string AMCode { get; set; }
        public string AMDetails { get; set; }

    }
         
}