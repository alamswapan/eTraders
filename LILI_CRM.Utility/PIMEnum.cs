using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LILI_BMS.Utility
{
    public static class PIMEnum
    {
        public enum ProjectCategory { Internal = 1, External = 2 };

        public enum ProjectType { 
            Proposed=1,
            Active =2,
            Subproject =3,
            Internal=4
        }
        public enum ProjectBudgetSteps
        {
            BudgetInitiation=1,
            HumanResourceAllocation=2,
            OtherResourceAllocation=3,
            AccountHeadWiseAllcoation=4,
            PRF=5            
        }
        public enum ProjectBudgetStatus
        {
            DRAFT=1,
            SUBMIT=2,
            REJECT=3,
            RECOMMEND=4,
            APPROVED=5,
            REVIEW=6
        }

    }
}
