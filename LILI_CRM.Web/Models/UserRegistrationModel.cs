using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LILI_CRM.Web.Models
{
    public class UserRegistrationModel
    {
        #region Ctor
        public UserRegistrationModel()
        {
            this.IUser = HttpContext.Current.User.Identity.Name;
            this.EUser = this.IUser;
            this.IDate = DateTime.Now;
            this.EDate = this.IDate;
            this.ddlOrganizationTypeList = new List<SelectListItem>();
            this.ddlOrgTypeFundSourceMinistryList = new List<SelectListItem>();
            this.Mode = "Create";
            this.Status = "AFR";
        }
        #endregion

        #region Standard Property

        public int Id { get; set; }

        [DisplayName("User Name")]
        [Required(ErrorMessage = "User Name is Required")]
        [StringLength(100)]
        public string UserFullName { get; set; }

        [DisplayName("Organization Type")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select one")]
        public int OrganizationTypeId { get; set; }
        public string OrganizationTypeName { get; set; }

        [DisplayName("Organization")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select one")]
        public int? OrgTypeFundSourceMinistryId { get; set; }
        public string OrgTypeFundSourceMinistryName { get; set; }

        [DisplayName("Organization Name")]
        [StringLength(100)]
        public string OrganizationName { get; set; }

        [DisplayName("E-Mail")]
        [StringLength(100)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid E-Mail Adress")]
        public string EMail { get; set; }

        [DisplayName("Telephone")]
        [StringLength(50)]
        public string Telephone { get; set; }

        [DisplayName("Address")]
        [StringLength(250)]
        public string Address { get; set; }

        [DisplayName("User Id")]
        [Required(ErrorMessage = "User Id is Required")]
        [StringLength(50)]
        public string UserId { get; set; }

        [DisplayName("Password")]
        [StringLength(50)]
        public string UserPassword { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "Status is Required")]
        [StringLength(50)]
        public string Status { get; set; }

        public string IUser { get; set; }
        public string EUser { get; set; }
        public DateTime IDate { get; set; }
        public DateTime? EDate { get; set; }
        
        #endregion

        #region Other
        public IList<SelectListItem> ddlOrganizationTypeList { get; set; }
        public IList<SelectListItem> ddlOrgTypeFundSourceMinistryList { get; set; }
        public IList<SelectListItem> ddlStatusList { get; set; }

        //Edit/View/Delete link
        public virtual string ActionLink { get; set; }

        public string Mode { get; set; }
        public string strMessage { get; set; }
        #endregion
    }


}