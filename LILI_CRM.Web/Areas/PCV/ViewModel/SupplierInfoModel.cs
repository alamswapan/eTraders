using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class SupplierInfoModel: BaseViewModel
    {

        public SupplierInfoModel()
        { 
            SupplierContactPerson =new List < SupplierContactPerson>();
        }

        [DisplayName("Supplier Id")]
        [Required(ErrorMessage = "Supplier Id is required.")]
        [MaxLength(10, ErrorMessage= "Exceeding maximum length.")]
        public string SupplierId { get; set; }

        [DisplayName("Supplier Name")]
        [Required(ErrorMessage = "Supplier Name is required.")]
        [MaxLength(100, ErrorMessage = "Exceeding maximum length.")]
        public string SupplierName { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage="Address is required.")]
        [MaxLength(250, ErrorMessage = "Exceeding maximum length.")]
        public string Address { get; set; }



        //[DisplayName("Country")]
        //[MaxLength(50, ErrorMessage = "Exceeding maximum length.")]
        //public string Country { get; set; }

        [DisplayName("Country")]
        //[Range(1, Int32.MaxValue, ErrorMessage = "Country is Required")]       
        public string Country { get; set; }


        [DisplayName("Phone")]
        [MaxLength(30, ErrorMessage = "Exceeding maximum length.")]
        public string Phone { get; set; }

        [DisplayName("Fax")]
        [MaxLength(30, ErrorMessage = "Exceeding maximum length.")]
        public string Fax { get; set; }

        [DisplayName("Mobile")]
        [MaxLength(30, ErrorMessage = "Exceeding maximum length.")]
        public string Mobile { get; set; }

        [DisplayName("Email")]
        [MaxLength(30, ErrorMessage = "Exceeding maximum length.")]
        public string Email { get; set; }

        [DisplayName("WebAddress")]
        [MaxLength(30, ErrorMessage = "Exceeding maximum length.")]
        public string WebAddress { get; set; }

        [DisplayName("Industry")]
        [MaxLength(100, ErrorMessage = "Exceeding maximum length.")]
        public string Industry { get; set; }


        //public IEnumerable<SelectListItem> ddlCountry { get; set; }

        public List<SupplierContactPerson> SupplierContactPerson { get; set; }

       

    }

    public class SupplierContactPerson : BaseViewModel
    {

        public string Designation { get; set; }
        public string Division { get; set; }
        public string Cell { get; set; }

        [DisplayName("Phone")]
        [MaxLength(30, ErrorMessage = "Exceeding maximum length.")]
        public string Phone { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email required")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Wrong email format")]
        public string Email { get; set; }

        [DisplayName("WebAddress")]
        [MaxLength(30, ErrorMessage = "Exceeding maximum length.")]
        public string WebAddress { get; set; }
        public string SkypeName { get; set; }
    }

    public class SupplierViewModel
    {
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
    }

    public class CountryModel : BaseViewModel
    {
        public long CountryId { get; set; }
        public string CountryName { get; set; }
    }

}