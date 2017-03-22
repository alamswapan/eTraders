using LILI_BMS.Utility;
using AutoMapper;
using LILI_CRM.Web.Utility;

namespace LILI_CRM.Web.Utility
{
    public class AutomapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            BMSMapper _bmsMapper = new BMSMapper();
        }
        protected virtual void ViceVersa<T1, T2>()
        {
            Mapper.CreateMap<T1, T2>();
            Mapper.CreateMap<T2, T1>();
        }
    }
}