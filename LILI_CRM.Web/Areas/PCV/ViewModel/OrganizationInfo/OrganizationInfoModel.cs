using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LILI_CRM.Web.Areas.PCV.ViewModel;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class OrganizationInfoModel : BaseViewModel
    {
        #region Ctor
        public OrganizationInfoModel()
        {
        }
        #endregion

        #region Standard Property

        [DisplayName("Organization Name")]
        [Required]
        [StringLength(250)]
        public string OrganizationName { get; set; }

        [DisplayName("Address")]
        [StringLength(500)]
        public string Address { get; set; }

        [DisplayName("Phone")]
        [StringLength(50)]
        public string Phone { get; set; }

        [DisplayName("Fax")]
        [StringLength(50)]
        public string Fax { get; set; }

        [DisplayName("EMail")]
        [StringLength(50)]
        public string EMail { get; set; }

        [DisplayName("Description")]
        [StringLength(500)]
        public string Description { get; set; }

        #endregion

    }
}