using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class CustomerInfoModel: BaseViewModel
    {

        public CustomerInfoModel()
        {
            CustomerContactPerson = new List<CustomerContactPerson>();
        }

        [DisplayName("Customer Id")]
        [Required(ErrorMessage = "Customer Id is required.")]
        [MaxLength(10, ErrorMessage= "Exceeding maximum length.")]
        public string CustomerId { get; set; }
                
        [DisplayName("Customer Name")]
        [Required(ErrorMessage = "Customer Name is required.")]
        [MaxLength(100, ErrorMessage = "Exceeding maximum length.")]
        public string CustomerName { get; set; }

        [DisplayName("Address")]
        [MaxLength(250, ErrorMessage = "Exceeding maximum length.")]
        public string Address { get; set; }

       //// [DisplayName("Address 2")]
       //// //[DefaultValue("")]
       ////// [DisplayFormat(ConvertEmptyStringToNull = false)]
       //// [MaxLength(250, ErrorMessage = "Exceeding maximum length.")]
       //// public string Address2 { get; set; }

        [DisplayName("Country")]
        [MaxLength(50, ErrorMessage = "Exceeding maximum length.")]
        public string Country { get; set; }

        [DisplayName("Phone")]
        [MaxLength(30, ErrorMessage = "Exceeding maximum length.")]
        public string Phone { get; set; }

        //[DisplayName("Fax")]
        //[MaxLength(30, ErrorMessage = "Exceeding maximum length.")]
        //public string Fax { get; set; }

        //[DisplayName("Mobile")]
        //[MaxLength(30, ErrorMessage = "Exceeding maximum length.")]
        //public string Mobile { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email required")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Wrong email format")]
        public string Email { get; set; }

        [DisplayName("WebAddress")]
        [MaxLength(30, ErrorMessage = "Exceeding maximum length.")]
        public string WebAddress { get; set; }

        [DisplayName("TIN")]
        [MaxLength(30, ErrorMessage = "Exceeding maximum length.")]
        public string TIN { get; set; }

        [DisplayName("BIN")]
        [MaxLength(30, ErrorMessage = "Exceeding maximum length.")]
        public string BIN { get; set; }

        public List<CustomerContactPerson> CustomerContactPerson { get; set; }
    }

    public class CustomerContactPerson: BaseViewModel
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
}