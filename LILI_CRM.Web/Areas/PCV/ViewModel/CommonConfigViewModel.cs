using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class CommonConfigViewModel
    {
        public string DisplayName { set; get; }

        [Required] 
        public int Id { set; get; }

        [Required]
        public string Code { set; get; }
        
        [Required]
        public string Name { set; get; }

        [DisplayName("Sort Order/ Size")]
        public int? SortOrder { set; get; }

        //[Required]
        public string Description { set; get; }

        [DisplayName("Is Active")]
        public bool IsActive { set; get; }

        public virtual ICollection<CommonConfigTypeViewModel> CommonConfigType { get; set; }

        //#region Ctor
        //public CommonConfigViewModel()
        //{
        //    this.IsActive = true;
        //}
        //#endregion
    }
}