using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace LILI_CRM.Web.Utility
{
    public static class CommonExceptionMessage
    {
        public static string GetMessageForAlertInfo(string strMsg)
        {
            return "<label class='alert-info'>" + strMsg + "</label>";
        }

        public static string GetMessageForAlert(string strMsg)
        {
            return "<label class='alert'>" + strMsg + "</label>";
        }

        public static string GetMessageForError(string strMsg)
        {
            return "<label class='error'>" + strMsg + "</label>";
        }

        public static string GetMessageForSuccess(string strMsg)
        {
            return "<label class='success'>" + strMsg + "</label>";
        }

        public static string GetExceptionMessage(Exception ex)
        {
            string message = "Error: There was a problem while processing your request: " + ex.Message;

            try
            {
                if (ex.InnerException != null)
                {
                    Exception inner = ex.InnerException;

                    if (inner is SqlException)
                    {
                        SqlException sqlException = ex.InnerException as SqlException;
                        message = Common.GetSqlExceptionMessage(sqlException.Number);
                    }
                    else if (inner is System.Data.Common.DbException)
                        message = "Database is currently experiencing problems. " + inner.Message;
                    else if (inner is System.Data.UpdateException)
                        message = "Datebase is currently updating problem.";
                    else if (inner is System.Data.EntityException)
                        message = "Entity is problem.";
                    else if (inner is NullReferenceException)
                        message = "There are one or more required fields that are missing.";
                    else if (inner is ArgumentException)
                    {
                        string paramName = ((ArgumentException)inner).ParamName;
                        message = string.Concat("The ", paramName, " value is illegal.");
                    }
                    else if (inner is ApplicationException)
                        message = "Exception in application" + inner.Message;
                    else
                        message = inner.Message;
                }
            }
            catch (Exception excep)
            {
                message = excep.Message;
            }

            //return message;
            return GetMessageForError(message);
        }

        public static string GetExceptionMessage(Exception ex, CommonAction actionName)
        {
            string message = "Error: There was a problem while processing your request: " + ex.Message;

            try
            {
                if (actionName == CommonAction.Save)
                    message = Common.GetCommomMessage(CommonMessageEnum.InsertFailed);
                if (actionName == CommonAction.Delete)
                    message = Common.GetCommomMessage(CommonMessageEnum.DeleteFailed);
                if (actionName == CommonAction.Update)
                    message = Common.GetCommomMessage(CommonMessageEnum.UpdateFailed);

                if (ex.InnerException != null)
                {
                    Exception inner = ex.InnerException;

                    if (inner is SqlException)
                    {
                        SqlException sqlException = ex.InnerException as SqlException;
                        message += Common.GetSqlExceptionMessage(sqlException.Number);
                    }
                    else if (inner is System.Data.Common.DbException)
                        message += "Database is currently experiencing problems. " + inner.Message;
                    else if (inner is System.Data.UpdateException)
                        message += "Datebase is currently updating problem.";
                    else if (inner is System.Data.EntityException)
                        message += "Entity is problem.";
                    else if (inner is NullReferenceException)
                        message += "There are one or more required fields that are missing.";
                    else if (inner is ArgumentException)
                    {
                        string paramName = ((ArgumentException)inner).ParamName;
                        message += string.Concat("The ", paramName, " value is illegal.");
                    }
                    else if (inner is ApplicationException)
                        message += "Exception in application" + inner.Message;
                    else
                        message += inner.Message;
                }
            }
            catch (Exception exObj)
            {
                message = exObj.Message;
            }

            return GetMessageForError(message);
        }

    }
}