using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

using LILI_CRM.Web.Utility;
using LILI_BMS.Domain.PCV;
using System.Text;
using LILI_CRM.Domain.PCV;
using LILI_CRM.Web.Models;



namespace LILI_CRM.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Fields
        private readonly BMSCommonSevice _bmsCommonService;
        #endregion

        public IFormsAuthentication FormsAuth
        {
            get;
            private set;
        }

        public IMembershipService MembershipService
        {
            get;
            private set;
        }
        // This constructor is used by the MVC framework to instantiate the controller using
        // the default forms authentication and membership providers.

        public AccountController()
            : this(null, null)
        {
        }

        // This constructor is not used by the MVC framework but is instead provided for ease
        // of unit testing this type. See the comments at the end of this file for more
        // information.
        public AccountController(IFormsAuthentication formsAuth, IMembershipService service)
        {
            FormsAuth = formsAuth ?? new FormsAuthenticationService();
            MembershipService = service ?? new AccountMembershipService();
        }
        public AccountController(BMSCommonSevice bmsCommonService)
        {
            this._bmsCommonService = bmsCommonService;
        }
        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //if (MembershipService.ValidateUser(model.UserName, model.Password))
                if (ValidateLogOnUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {

                        //return RedirectToAction("Index", "Home");
                        return RedirectToAction("Index", "Home", new { area = "PCV" });
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public bool ValidateLogOnUser(string userId, string password)
        {
            bool valid = false;
            var encritedPass = _bmsCommonService.BMSUnit.UserInfoRepository.GetAll().Where(c => c.UserId == userId).SingleOrDefault();
            var list = _bmsCommonService.BMSUnit.UserInfoRepository.GetAll();
            foreach (var item in list)
            {
                //if (item.UserName == userName && item.Password == password)
                if (item.UserId == userId && password == PassDeCode(encritedPass.Passwd))
                {
                    valid = true;
                }
            }
            return valid;
        }

        public static string PassDeCode(string xPassword)
        {
            string functionReturnValue = null;
            functionReturnValue = "";
            int I = 0;
            Int32 A = 0;
            for (I = (xPassword.Length - 1); I >= 0; I += -1)
            {
                A = (int)(Convert.ToChar(xPassword.Substring(I, 1)));
                byte s = Encoding.Default.GetBytes(Convert.ToChar(A).ToString())[0];
                try
                {
                    functionReturnValue = functionReturnValue + Convert.ToChar((int)s - 104);
                }
                catch
                {
                    return "";
                }
            }
            return functionReturnValue;
        }




        public static String PassEnCode(String xPassword)
        {
            String vPassOut = "";
            int p = 0;
            for (int i = (xPassword.Length) - 1; i >= 0; i--)
            {
                //vPassOut = vPassOut + (Char)(Microsoft.VisualBasic.Strings.Asc(vPass[i]) - 104);
                char s = xPassword[i];
                p = (int)(xPassword[i]);
                int intVal = (int)(xPassword[i]) + 104;
                byte[] intBytes = BitConverter.GetBytes(intVal);
                char v2 = Encoding.Default.GetString(intBytes)[0];
                vPassOut = vPassOut + v2;
            }
            return vPassOut;
        }





        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();


            return RedirectToAction("LogOn", "Account");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

        // The FormsAuthentication type is sealed and contains static members, so it is difficult to
        // unit test code that calls its members. The interface and helper class below demonstrate
        // how to create an abstract wrapper around such a type in order to make the AccountController
        // code unit testable.

        public interface IFormsAuthentication
        {
            void SignIn(string userName, bool createPersistentCookie);
            void SignOut();
        }

        public class FormsAuthenticationService : IFormsAuthentication
        {
            public void SignIn(string userName, bool createPersistentCookie)
            {
                FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
            }
            public void SignOut()
            {
                FormsAuthentication.SignOut();
            }
        }

        public interface IMembershipService
        {
            int MinPasswordLength { get; }

            bool ValidateUser(string userName, string password);
            MembershipCreateStatus CreateUser(string userName, string password, string email);
            bool ChangePassword(string userName, string oldPassword, string newPassword);
        }

        public class AccountMembershipService : IMembershipService
        {
            private CustomMembershipProvider _provider;

            public AccountMembershipService()
                : this(null)
            {
            }

            public AccountMembershipService(CustomMembershipProvider provider)
            {
                _provider = provider ?? new CustomMembershipProvider();
            }

            public int MinPasswordLength
            {
                get
                {
                    return _provider.MinRequiredPasswordLength;
                }
            }

            public bool ValidateUser(string userName, string password)
            {
                return _provider.ValidateUser(userName, password);
            }

            public MembershipCreateStatus CreateUser(string userName, string password, string email)
            {
                MembershipCreateStatus status;
                _provider.CreateUser(userName, password, email, null, null, true, null, out status);
                return status;
            }

            public bool ChangePassword(string userName, string oldPassword, string newPassword)
            {
                return new CustomMembershipProvider().ChangePassword(userName, oldPassword, newPassword);
            }
        }
    }
}
