using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LILI_CRM.DAL.CRM;
using System.Data;

namespace LILI_CRM.Service
{
   public class BMSServiceContext:IDisposable
    {
        internal readonly LILI_CRMEntities1 _context;

        public BMSServiceContext()
        {
            _context = new LILI_CRMEntities1(); 
        }
        public LILI_CRMEntities1 AIMSContext
        {
            get { return _context; }
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public static string getAIMSConStrIntegrated()
        {
            string conStrIntegratedSecurity = new System.Data.EntityClient.EntityConnectionStringBuilder
            {
                Metadata = "res://PRM/Models.PRM_MfsIwm.csdl|res://PRM/Models.PRM_MfsIwm.ssdl|res://PRM/Models.PRM_MfsIwm.msl",
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = new System.Data.SqlClient.SqlConnectionStringBuilder
                {
                    InitialCatalog = "LILI_BMS",
                    DataSource = @"PC54\SQL2008",
                    IntegratedSecurity = false,
                    UserID = "sa",
                    Password = "dataport",
                    MultipleActiveResultSets = true,
                    ConnectTimeout = 0,
                    MaxPoolSize = 500,
                }.ConnectionString
            }.ConnectionString;

            return conStrIntegratedSecurity;
        }

      //     <add name="PRM_MfsIwmEntities" 
      //         connectionString="metadata=res://PRM/Models.PRM_MfsIwm.csdl|res://PRM/Models.PRM_MfsIwm.ssdl|res://PRM/Models.PRM_MfsIwm.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=PC54\SQL2008;initial catalog=IWM_MFS;persist security info=True;user id=sa;password=dataport;multipleactiveresultsets=True;App=EntityFramework&quot;"
      //providerName="System.Data.EntityClient" />


    }
}
