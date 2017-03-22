//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Net.Mail;
//using System.Reflection;
//using LILI_BMS.Web.Areas.PRM.Controllers;

//namespace LILI_BMS.UnitTests
//{
//    /// <summary>
//    /// Summary description for UnitTest1
//    /// </summary>
//    [TestClass]
//    public class UnitTest1
//    {
//        public UnitTest1()
//        {
//            //
//            // TODO: Add constructor logic here
//            //
//        }

//        private TestContext testContextInstance;

//        /// <summary>
//        ///Gets or sets the test context which provides
//        ///information about and functionality for the current test run.
//        ///</summary>
//        public TestContext TestContext
//        {
//            get
//            {
//                return testContextInstance;
//            }
//            set
//            {
//                testContextInstance = value;
//            }
//        }

//        #region Additional test attributes
//        //
//        // You can use the following additional attributes as you write your tests:
//        //
//        // Use ClassInitialize to run code before running the first test in the class
//        // [ClassInitialize()]
//        // public static void MyClassInitialize(TestContext testContext) { }
//        //
//        // Use ClassCleanup to run code after all tests in a class have run
//        // [ClassCleanup()]
//        // public static void MyClassCleanup() { }
//        //
//        // Use TestInitialize to run code before running each test 
//        // [TestInitialize()]
//        // public void MyTestInitialize() { }
//        //
//        // Use TestCleanup to run code after each test has run
//        // [TestCleanup()]
//        // public void MyTestCleanup() { }
//        //
//        #endregion

//        [TestMethod]
//        public void TestMethod1()
//        {
//            string smtphost = "192.168.50.19";
//            int port = 0;
//            string user = "test@ACI.com.bd";
//            string password = "notes6";
//            string message = "Hello everybody,<br /><br />" +
//                "<table><tr><td>Name</td><td>ID</td></tr>"
//                +"<tr><td>1</td><td>2</td></tr>"
//                +"</table>!" +
//                "Multiple receipient test!</b>. ";

           
//            EmailService emailService = new EmailService(smtphost,port,user,password);
//            List<string> reciepientList = new List<string>();
//           // reciepientList.Add("zillur.rahman@ACI.com.bd");
//            reciepientList.Add("mr.rooman@gmail.com");
//            string subject = "AutoNotification for Job Confirmation";
//            emailService.SendMail(reciepientList,subject,message);           
//        }

//        //[TestMethod]
//        //public void TestMethod2()
//        //{            Type t = typeof(EmployeeController);
//        //    MethodInfo[] mi = t.GetMethods();
//        //    foreach (MethodInfo m in mi)
//        //    {
//        //        if (m.IsPublic)
//        //            if (typeof(System.WebActionResult).IsAssignableFrom(m.ReturnParameter.ParameterType))
//        //                methods = m.Name + Environment.NewLine + methods;
//        //    }       
        
//        //}


//    }

//    public class EmailService {

//        private SmtpClient _emailClient;

//        public EmailService(string host,int port,string netUser,string password)
//        {
//            _emailClient = ConfigureClient(host,port,netUser,password);    
//        }
//        public void SendMail(List<string> reciepientList,string subject,string message)
//        {

//            MailMessage msg = new MailMessage();            
//            msg.From =
//                new MailAddress("nahid.hossain@ACI.com.bd", "AutoMail");
//            foreach (var item in reciepientList)
//            {
//                msg.To.Add(item);            
//            }    
//            msg.Priority = MailPriority.High;
//            msg.Subject = subject;         
//            msg.Body = message;             
//            msg.IsBodyHtml = true;
//            // Attachment Example
//            // msg.Attachments.Add(new Attachment("C:\\Site.lnk"));
//            // Connecting to the server and configuring it
//            _emailClient.Send(msg);             

//        }

//        public SmtpClient ConfigureClient(string host,int port,string networkUser,string password)
//        {
//            var client= new SmtpClient();
//            client.Host = host;// "smtp.gmail.com";
//            //if (port >= 0)
//            //{
//            //    client.Port = port; // example 578
//            //}
//            //client.EnableSsl = true; //should be enabled for ssl        
//            client.UseDefaultCredentials = false;
//            client.Credentials = new System.Net.NetworkCredential(networkUser, password);
//            client.DeliveryMethod = SmtpDeliveryMethod.Network;
//            return client;
//        }    
//    }
//}
