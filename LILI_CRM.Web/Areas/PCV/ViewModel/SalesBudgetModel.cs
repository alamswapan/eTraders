using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class SalesBudgetModel : BaseViewModel
    {
        public SalesBudgetModel()
        {
            SalesBudgetDetailModel = new List<SalesBudgetDetailViewModel>();
        }

        //public List<CustomerInfoModel> customerListModels { get; set; }

        [DisplayName("Sales Person")]
        //[Range(1, int.MaxValue, ErrorMessage = "Sales Person is required")]
        public long SalesPersonId { get; set; }
        public string SalesPersonName { get; set; }

        [DisplayName("SBU")]
        //[Range(1, int.MaxValue, ErrorMessage = "SBU is  required")]
        public long SBUId { get; set; }
        public string SBUName { get; set; }

        [DisplayName("Product")]
        public long ProductId { get; set; }
        public string ProductName { get; set; }

        [DisplayName("Year")]
        public string BudgetYear { get; set; }

        [DisplayName("Division Name")]
        public string DivisionName { get; set; }    


        //public IEnumerable<SelectListItem> ddlDivision { get; set; }
        public IEnumerable<SelectListItem> ddlSBU { get; set; }
        public IEnumerable<SelectListItem> ddlProduct { get; set; }

        public IEnumerable<SelectListItem> ddlCustomer { get; set; }
        public IEnumerable<SelectListItem> ddlSupplier { get; set; }

        public List<SalesBudgetDetailViewModel> SalesBudgetDetailModel { get; set; }

        
    }

    public class SalesBudgetDetailViewModel : BaseViewModel
    {
        public SalesBudgetDetailViewModel()
        {

        }
        public decimal JanQty { get; set; }
        public decimal JanVal { get; set; }
        public decimal JanCom { get; set; }

        public decimal FebQty { get; set; }
        public decimal FebVal { get; set; }
        public decimal FebCom { get; set; }

        public decimal MarQty { get; set; }
        public decimal MarVal { get; set; }
        public decimal MarCom { get; set; }

        public decimal AprQty { get; set; }
        public decimal AprVal { get; set; }
        public decimal AprCom { get; set; }

        public decimal MayQty { get; set; }
        public decimal MayVal { get; set; }
        public decimal MayCom { get; set; }

        public decimal JunQty { get; set; }
        public decimal JunVal { get; set; }
        public decimal JunCom { get; set; }

        public decimal JulyQty { get; set; }
        public decimal JulyVal { get; set; }
        public decimal JulyCom { get; set; }

        public decimal AugQty { get; set; }
        public decimal AugVal { get; set; }
        public decimal AugCom { get; set; }

        public decimal SepQty { get; set; }
        public decimal SepVal { get; set; }
        public decimal SepCom { get; set; }

        public decimal OctQty { get; set; }
        public decimal OctVal { get; set; }
        public decimal OctCom { get; set; }

        public decimal NovQty { get; set; }
        public decimal NovVal { get; set; }
        public decimal NovCom { get; set; }

        public decimal DecQty { get; set; }
        public decimal DecVal { get; set; }
        public decimal DecCom { get; set; }

        public long CustomerId { get; set; }
        public long SupplierId { get; set; }

        public string CustomerName { get; set; }
        public string SupplierName { get; set; }

    }

    public class SalesBudgetDetail : BaseViewModel
    {
        public SalesBudgetDetail()
        {

        }  
       
        public long SalesBudgetId { get; set; }
        public long CustomerId { get; set; }
        public long SupplierId { get; set; }
        public int BudgetMonth { get; set; }
        public decimal Quantity { get; set; }
        public decimal Value { get; set; }
        public decimal Commission { get; set; }

    }

}