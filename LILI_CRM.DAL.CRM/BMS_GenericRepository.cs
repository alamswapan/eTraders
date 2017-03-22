using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;
using LILI_CRM.DAL.Infrastructure;
using LILI_CRM.DAL.CRM;





namespace LILI_CRM.DAL.CRM
{
    public class BMS_GenericRepository<T> : DataRepository<T> where T : class
    {
        public BMS_GenericRepository(LILI_CRMEntities1 context)
            : base(context)
        {
            //constructor
        }
    }
}