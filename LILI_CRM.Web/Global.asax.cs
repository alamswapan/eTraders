using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LILI_CRM.Web.Utility;
using Autofac;
using System.Reflection;
using Autofac.Integration.Mvc;
//using LILI_CRM.Web.Reports.PRM.viewers;
using LILI_CRM.Web.Filters;
using LILI_CRM.DAL.CRM;
using LILI_CRM.Domain.PCV;



namespace LILI_CRM.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            filters.Add(new AuthorizationFilterAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");

            /*  routes.MapRoute(
                  "Default", // Route name
                  "{controller}/{action}/{id}", // URL with parameters
                  new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                  new[] { AppConstant.ProjectName + ".Controllers" });*/

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Account", action = "LogOn", id = UrlParameter.Optional }, // Parameter defaults
                new[] { AppConstant.ProjectName + ".Controllers" });

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            //ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());

            #region IoC registration

            var builder = new ContainerBuilder();
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            #region DataLayer DI for PRM

            //builder.Register(c => new IWM_MFSPRMEntities()).InstancePerHttpRequest();
            //builder.RegisterType<PRM_UnitOfWork>().InstancePerHttpRequest();
            //builder.RegisterGeneric(typeof(PRM_GenericRepository<>)).InstancePerHttpRequest();
            //builder.RegisterType<PRM_ExecuteFunctions>().InstancePerHttpRequest();

            #endregion

            #region DataLayer DI for AIMS

            builder.Register(c => new LILI_CRMEntities1()).InstancePerHttpRequest();
            builder.RegisterType<BMS_UnitOfWork>().InstancePerHttpRequest();
            builder.RegisterGeneric(typeof(BMS_GenericRepository<>)).InstancePerHttpRequest();
            builder.RegisterType<BMS_ExecuteFunctions>().InstancePerHttpRequest();

            #endregion



            #region Service DI for PRM

            //builder.RegisterType<JobGradeService>().InstancePerHttpRequest();
            //builder.RegisterType<SalaryStructureService>().InstancePerHttpRequest();
            //builder.RegisterType<PRMCommonSevice>().InstancePerHttpRequest();
            //builder.RegisterType<EmployeeService>().InstancePerHttpRequest();
            //builder.RegisterType<PersonalInfoService>().InstancePerHttpRequest();
            //builder.RegisterType<JobDesignationService>().InstancePerHttpRequest();
            //builder.RegisterType<DivisionHeadMapingService>().InstancePerHttpRequest();
            //builder.RegisterType<LILI_BMS.Domain.BMS.ResourceInfoService>().InstancePerHttpRequest();
            //builder.RegisterType<LILI_BMS.Domain.BMS.ResourceCategoryService>().InstancePerHttpRequest();
            //builder.RegisterType<EmployeeTransferInfoService>().InstancePerHttpRequest();
            //builder.RegisterType<EmployeeDesignationChangeService>().InstancePerHttpRequest();
            //builder.RegisterType<EmployeeConfirmIncrementPromotionService>().InstancePerHttpRequest();
            //builder.RegisterType<EmployeeSeperationService>().InstancePerHttpRequest();

            #endregion


            #region

            builder.RegisterType<BMSCommonSevice>().InstancePerHttpRequest();

            #endregion


            #region Report

            builder.RegisterType<ReportBase>().InstancePerHttpRequest();

            #endregion

            ///**********Example: Dot not Delete **********************************
            //builder.RegisterType<PRM_UnitOfWork>().PropertiesAutowired().InstancePerHttpRequest();
            //builder.Register(x => new PRM_UnitOfWork(new PRM_MfsIwmEntities())).InstancePerHttpRequest();          
            //builder.RegisterType<PRM_GenericRepository<PRM_JobGrade>>().InstancePerHttpRequest();  
            //builder.RegisterGeneric(typeof(PRM_GenericRepository<>)).As(typeof(IRepository<>)).InstancePerHttpRequest();
            //builder.RegisterGeneric(typeof(DataRepository<>)).As(typeof(IRepository<>)).InstancePerHttpRequest();
            //// Automapper
            //builder.RegisterType<AutomapperStartupTask>().As<IStartupTask>().InstancePerHttpRequest();

            /////////////////////////////////////////////////////////////////////////


            //build container
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            #endregion

            #region AutoMapper

            var mapper = new AutomapperStartupTask();
            mapper.Execute();

            #endregion

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }


        #region All Enums

        public enum EnumDocumentApprovalInformation
        {
            Draft,
            Prepared,
            Submitted,
            Rejected,
            Approved,
            Reviewed,
            Recommended
        }

        public enum CPFMembershipStatus
        {
            Active,
            Inactive
        }

        public enum CPFApprovalStatus
        {
            Draft,
            Submitted,
            Reviewed,
            Approved,
            Rejected
        }

        public enum CPFTransactionType
        {
            Debit,
            Credit
        }

        #endregion
    }
}