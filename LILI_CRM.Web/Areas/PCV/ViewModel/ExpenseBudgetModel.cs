using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class ExpenseBudgetModel : BaseViewModel
    {
        public ExpenseBudgetModel()
        {
            ExpenseBudgetDetailModel = new List<ExpenseBudgetDetailViewModel>();
        }

        //public List<CustomerInfoModel> customerListModels { get; set; }

        [DisplayName("Sales Person")]
        //[Range(1, int.MaxValue, ErrorMessage = "Sales Person is required")]
        public long SalesPersonId { get; set; }
        public string SalesPersonName { get; set; }

        [DisplayName("Division Name")]
        public long DivisionId { get; set; }
        public string DivisionName { get; set; }

        [DisplayName("SBU")]
        //[Range(1, int.MaxValue, ErrorMessage = "SBU is  required")]
        public long SBUId { get; set; }
        public string SBUName { get; set; }

        //[DisplayName("Product")]
        //public long ProductId { get; set; }
        //public string ProductName { get; set; }

        [DisplayName("Year")]
        public string BudgetYear { get; set; }            


        public IEnumerable<SelectListItem> ddlDivision { get; set; }
        public IEnumerable<SelectListItem> ddlSBU { get; set; }  


        public List<ExpenseBudgetDetailViewModel> ExpenseBudgetDetailModel { get; set; }

        
    }

    public class ExpenseBudgetDetailViewModel : BaseViewModel
    {
        public ExpenseBudgetDetailViewModel()
        {

        }
        public decimal JanVal { get; set; }

        public decimal FebVal { get; set; }

        public decimal MarVal { get; set; }

        public decimal AprVal { get; set; }

        public decimal MayVal { get; set; }

        public decimal JunVal { get; set; }

        public decimal JulyVal { get; set; }

        public decimal AugVal { get; set; }

        public decimal SepVal { get; set; }

        public decimal OctVal { get; set; }

        public decimal NovVal { get; set; }

        public decimal DecVal { get; set; }   

        public long ParticularId { get; set; }
        public string ParticularName { get; set; }

    }

    public class ExpenseBudgetDetail : BaseViewModel
    {
        public ExpenseBudgetDetail()
        {

        }  
       
        public long ExpenseBudgetId { get; set; }
        public long ParticularId { get; set; }
        public int BudgetMonth { get; set; }
        public decimal Value { get; set; }

    }

    public class ParticularViewModel
    {
        public long ParticularId { get; set; }
        public string ParticularName { get; set; }
    }

}