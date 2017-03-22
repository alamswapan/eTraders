//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using LILI_BMS.Web;
//using LILI_BMS.Web.Areas.PIM.Controllers;
//using LILI_BMS.Domain.PIM;
//using Moq;
//using System.Web.Mvc;
//using LILI_BMS.Web.Areas.PIM.ViewModel;
//using LILI_BMS.DAL.PIM;
//using LILI_BMS.Web.Utility;



//namespace LILI_BMS.UnitTests
//{
//    /// <summary>
//    /// Summary description for UnitTest2
//    /// </summary>
//    [TestClass]
//    public class PIMActivityControllerTest
//    {
//        public PIMActivityControllerTest()
//        {
          
//            //ActivityController controller = 
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

//        //[TestMethod]
//        //public void When_Create_Post_ISModelState_False_returnRightMessage_()
//        //{
//        //    //Arrenge
//        //    Mock<PIMCommonService> svc = new Mock<PIMCommonService>();
//        //    ActivityController controller = new ActivityController(svc.Object);
//        //    controller.ModelState.AddModelError("test", new Exception());
//        //    var model = new ActivityViewModel() { };

//        //    //Act
//        //    var result =controller.Create(model) as ViewResult;
//        //    var newModel = result.ViewData.Model as ActivityViewModel;  

//        //    //Assert
//        //    Assert.IsFalse(controller.ModelState.IsValid);
//        //    Assert.AreEqual(newModel.Message, Common.GetCommomMessage(CommonMessage.InsertFailed));
//        //}
        
//        //need a test that will verify all valid redirect action senario
        

//        [TestMethod]
//        public void When_Index_called_ItShouldReturn_rightView()
//        {
//            //Arrenge
//            var context = new Mock<IWM_MFSPIMEntities>();
//           // var genericRepository =new PIM_UnitOfWork(null,null,null,null,null,genericRepository
          
//            Mock<PIMCommonService> svc = new Mock<PIMCommonService>();
//            ActivityController controller = new ActivityController(svc.Object);
          
           
//            //Act
//            var result=(ViewResult) controller.Create();                  
            
//            //Assert
//            Assert.AreEqual("ActivityIndex",result.ViewName);

//          }

//        [TestMethod]
//        public void When_BMSalidModel_Posted_Modelstate_InValid()
//        {
//            //Arrenge
//            Mock<PIMCommonService> svc = new Mock<PIMCommonService>();             
//            ActivityController controller = new ActivityController(svc.Object);
//            controller.ModelState.AddModelError("test", new Exception());
//            var model = new ActivityViewModel() { };

//            //Act
//            controller.Create(model, new string[]{});           

//            //Assert
//            Assert.IsFalse(controller.ModelState.IsValid);
//        }
//        [TestMethod]
//        public void When_Create_Post_InvalidModel_ModelSuccess_Isfalse()
//        {
//            //Arrenge
//            Mock<PIMCommonService> svc = new Mock<PIMCommonService>();
//            ActivityController controller = new ActivityController(svc.Object);          
//            var model = new ActivityViewModel() { };           
           
//            //Act
//            var result = (ViewResult)controller.Create(model, new string[] { });
//           var newModel = result.ViewData.Model as ActivityViewModel;
            
//            //Assert
//            Assert.IsFalse(newModel.IsSuccessful);
//            Assert.AreEqual("ActivityIndex", result.ViewName);
//            Assert.AreEqual(newModel.Message, Common.GetCommomMessage(CommonMessage.InsertFailed));           
//        }

//        [TestMethod]
//        public void When_Create_Post_AnyExceptionOccured_ShouldReturnRightMessage_()
//        {
//            Mock<PIM_UnitOfWork> unitOfWork = new Mock<PIM_UnitOfWork>();

//            Mock<PIMCommonService> svc = new Mock<PIMCommonService>();                           
//            ActivityController controller = new ActivityController(svc.Object);
//            var model = new ActivityViewModel() { };
//            svc.Setup(d => d.PIMUnit.Activity.SaveChanges()).Throws(new NullReferenceException());     //Exception occured 

//            //Act 
//            var result = (ViewResult)controller.Create(model, new string[] { });
//            var newModel = result.ViewData.Model as ActivityViewModel;
     
//            //Assert
//            Assert.AreEqual(newModel.Message, CommonExceptionMessage.GetExceptionMessage(new Exception(), CommonAction.Save));        
//        }

//        [TestMethod]
//        public void When_EmptyModel_Posted_Modelstate_InValid()
//        {
//            //var mock = new Mock<ISomethingUseful>();
//            //mock.Expect(u => u.CalculateSomething(123)).Returns(1);

//            //var myClass = new MyClass(mock.Object);
//            //Assert.AreEqual(2, myClass.MethodUnderTest(123));

//            //Arrenge
//            Mock<PIMCommonService> svc = new Mock<PIMCommonService>();
//            //Mock<IPIM_UnitOfWork> unitOfWork = new Mock<IPIM_UnitOfWork>();
//            //svc.Setup(d=>d.PIMUnit.Activity.SaveChanges()).Throws(new Exception());


//            ActivityController controller = new ActivityController(svc.Object);
//            controller.ModelState.AddModelError("test", new Exception());
//            var model = new ActivityViewModel() { };

//            //svc.Setup(unitOfWork.Behavior.PIMUnit.Activity.Add(model.ToEntity()));
         
//            //Act
//            controller.Create(model, new string[] { });
//            //var resultModel =result.ViewData.Model as ActivityViewModel;

//            //Assert
//            Assert.IsFalse(controller.ModelState.IsValid);

//        }

//        // arrange
//    //var mockRepository = new Mock<IBlogPostSVC>();
//    //var homeController = new HomeController(mockRepository.Object);

//    //// act
//    //var p = new BlogPost { Title = "test" };            // date and content should be required
//    //homeController.Index(p);

//    //// assert
//    //Assert.IsTrue(!homeController.ModelState.IsValid);

//    }
//}
