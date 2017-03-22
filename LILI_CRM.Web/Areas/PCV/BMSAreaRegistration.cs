using System.Web.Mvc;

namespace LILI_CRM.Web.Areas.PCV
{
    public class BMSAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PCV";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "BMS_default",
                "PCV/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
