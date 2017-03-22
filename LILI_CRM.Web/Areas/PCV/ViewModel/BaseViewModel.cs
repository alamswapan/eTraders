using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class BaseViewModel
    {
        //[Required]
        //public int Id { get; set; }
        public long Id { get; set; }


        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        //AuditInfo
        public string IUser { get; set; }
        public DateTime IDate { get; set; }
        public string EUser { get; set; }
        public DateTime? EDate { get; set; }

        //Edit/View/Delete link
        public virtual string ActionLink { get; set; }

        //ErrorHandling
        public string ActionType { get; set; }
        public string ButtonText { get; set; }
        public bool DeleteEnable { get; set; }
        public string SelectedClass { get; set; }
        public string Message { get; set; }
        public string ErrorClass { get; set; }



        public string Mode { get; set; }
        public int IsError { set; get; }
        public string ErrMsg { set; get; }
    }
}