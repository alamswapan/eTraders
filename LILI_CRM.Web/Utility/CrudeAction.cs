﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LILI_CRM.Web.Utility
{
    public static class CrudeAction
    {
        public static string Create { get { return "Create"; } }
        public static string AddNew { get { return "AddNew"; } }
        public static string Edit { get { return "Edit"; } }
        public static string Reject { get { return "Reject"; } }
        public static string Delete { get { return "Delete"; } }

        public static string Approve { get { return "Approve"; } }

        public static string ProjectLeader { get { return "PL"; } }
        public static string ProjectSupervisor  { get { return "PS"; } }
        public static string Director_OP { get { return "DIROP"; } }
        public static string DirectorPnD  { get { return "DIRPnD"; } }
        public static string ExecutiveDirector { get { return "ED"; } }
    }
}