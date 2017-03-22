using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LILI_CRM.DAL.CRM.CustomEntities;

namespace LILI_CRM.Web.Models
{
    public class MenuModels
    {
        #region Ctor
        public MenuModels()
        {
            MenuList = new List<MenuList>();
        }
        #endregion
        public List<MenuList> MenuList;
    }    
}