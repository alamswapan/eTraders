using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using LILI_CRM.Web.Areas.PCV.ViewModel;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class UserMenuModel : BaseViewModel
    {
        #region Ctor
        public UserMenuModel()
        {
            this.IUser = HttpContext.Current.User.Identity.Name;
            this.EUser = this.IUser;
            this.IDate = DateTime.Now;
            this.EDate = this.IDate;

            this.Mode = "Create";
            this.UserMenuDetail = new List<UserMenuDetail>();
            this.MenuList = new List<SelectListItem>();
        }
        #endregion

        #region Standard Property

        public int Id { get; set; }

        [DisplayName("User Name")]
        [Required]
        public string UserName { get; set; }

        [DisplayName("Password")]
        [Required]
        public string Password { get; set; }

        public int EmpId { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        public string IUser { get; set; }
        public string EUser { get; set; }
        public System.DateTime IDate { get; set; }
        public System.DateTime EDate { get; set; }

        #endregion

        #region Other

        public string Mode { get; set; }
        public IList<UserMenuDetail> UserMenuDetail { get; set; }

        [DisplayName("Menu Name")]
        public int MenuId { get; set; }
        public IList<SelectListItem> MenuList { get; set; }
        
        #endregion
    }

    public class UserMenuDetail
    {
        #region Ctor

        public UserMenuDetail()
        {
            this.IUser = HttpContext.Current.User.Identity.Name;
            this.EUser = this.IUser;
            this.IDate = DateTime.UtcNow;
            this.EDate = this.IDate;

            //this.MenuList = new List<SelectListItem>();
        }
        
        #endregion

        #region Standard Property
        
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MenuId { get; set; }
        //public IList<SelectListItem> MenuList { get; set; }

        public string IUser { get; set; }
        public string EUser { get; set; }
        public System.DateTime IDate { get; set; }
        public System.DateTime EDate { get; set; }

        #endregion

        #region Other
        public string MenuName { get; set; }
        #endregion
    }

    public class UserMenuSearch : BaseViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        //public string EmpFullName { get; set; }

        //public string Designation { get; set; }

        //public string Department { get; set; }
    }
}