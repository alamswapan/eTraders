using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LILI_CRM.DAL.CRM.CustomEntities
{
    public class MenuList
    {
        public int Id { get; set; }

        public string ModuleName { get; set; }

        public string MenuName { get; set; }

        public string ParentMenuName { get; set; }

        public string PageUrl { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }
    }
}
