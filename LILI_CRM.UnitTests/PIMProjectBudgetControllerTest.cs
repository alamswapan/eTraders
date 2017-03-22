//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using LILI_BMS.Web.Utility;
//using Moq;
//using LILI_BMS.Domain.PIM;
//using LILI_BMS.Web.Areas.PIM.Controllers;
//using System.Web.Mvc;
//using LILI_BMS.DAL.PIM;
//using LILI_BMS.Web.Areas.PIM.ViewModel;
//using LILI_BMS.Utility;
//using System.Data.Objects;
//using System.Data.EntityClient;

//namespace LILI_BMS.UnitTests
//{
//    /* 
//     * 
//     * ------------------------------------Project Initiation Business Rule---------------------------------
//     1. Create Business Rule
     
//       •Budget year will be as financial year of the company. 
//       •Start date is first date of the financial year and end date is the last date of the financial year.
//       •Financial year, start date and end date will be applicable to the IWM internal projects.  
     
//     2. Edit Business Rule
      
//       •User can’t update the approved budget.  
//       •Budget year will be as financial year of the company. 
//       •Start date is first date of the financial year and end date is the last date of the financial year.
//       •Financial year, start date and end date will be applicable to the IWM internal projects.  
       
//      3. Delete Business Rule
//         •	User cannot delete approve budget.
//     *-------------------------------------------------------------------------------------------------------
     
//     * 
//     * 
//     * 
//     * 
//     * 
//     * 
//     * 
//     *
//     * 
//     */

//    [TestClass]
//    public class PIMProjectBudgetControllerTest
//    {
//        private Mock<PIMCommonService> mockPIMService;
//        private Mock<PIM_UnitOfWork> mockUnitofWork;
//        private ProjectBudgetController controller;
//        private List<PIM_ProjectInfo> projectList;
//        private IWM_MFSPIMEntities _context;
//        private EntityConnection con;


//        [TestInitialize]
//        public void TestInitialize()
//        {       
//            this.mockUnitofWork = new Mock<PIM_UnitOfWork>();
//           // this.mockUnitofWork.Setup(d => d.ProjectInformation).Returns(new PIM_GenericRepository<PIM_ProjectInfo>(_context));
//            //this.mockUnitofWork.Setup(d => d.ProjectInformation).Returns(new PIM_GenericRepository<PIM_ProjectInfo>(_context));
           
//            this.mockPIMService = new Mock<PIMCommonService>(mockUnitofWork.Object);
//           // this.controller = new ProjectBudgetController(mockPIMService.Object);
//            this._context = new IWM_MFSPIMEntities();           

//            projectList = new List<PIM_ProjectInfo>();
//            projectList.Add(new PIM_ProjectInfo { Id=1, ProjectNo="TestProjectNo-1" });
//            projectList.Add(new PIM_ProjectInfo { Id = 2, ProjectNo = "TestProjectNo-2" });  
       
//            //var mapper = new AutomapperStartupTask();
//            //mapper.Execute();
//        }

//        [TestMethod]
//        public void Index_ReturnIndexView()
//        {
//            //Arrange                      
           
//            //Act

//            //var result =(ViewResult) controller.Index();

//            //Assert
//            //Assert.AreEqual("Index", result.ViewName);
//        }

//        [TestMethod]
//        public void Create_ReturnCreateView()
//        {
//            //Arrange
            
//            //Act
//             var result = (ViewResult)controller.Create();

//            //Assert
//            Assert.AreEqual("CreateOrEdit", result.ViewName);
//        }

//        [TestMethod]
//        public void Create_ModelBudgetState_ShouldbIn_BudgetIntilaize()
//        {
//            //Arrange

//            //Act
//            var result = (ViewResult)controller.Create();
//            var model = (BudgetMasterCreateOrEditViewModel)result.ViewData.Model;

//            //Assert
//            Assert.AreEqual(model.BudgetStep,(int)PIMEnum.ProjectBudgetSteps.BudgetInitiation);
//        }

//        [TestMethod]
//        public void Create_WhenCalled_ShouldHave_ProjectInformationList()
//        {
//            //Arrange

//            //mockUnitofWork.Setup(d => d.ProjectInformation).Returns(new PIM_GenericRepository<PIM_ProjectInfo>(_context));

//             mockPIMService.Setup(r => r.PIMUnit.ProjectInformation.GetAll()).Returns(projectList);

//            //Act
//            var result = (ViewResult)controller.Create();
//            var model = (BudgetMasterCreateOrEditViewModel)result.ViewData.Model;
//           // var childModel = model.projectInfo;

//            //Assert
//          ///  Assert.IsNotNull(childModel.ProjectNoList);
//          //  Assert.IsTrue(childModel.ProjectNoList.Count() > 1);
            
//        }
//    }
//}
