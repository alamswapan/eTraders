using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AIMS_UNDP.Web.AIMSSecurityService;

namespace AIMS_UNDP.Web.Utility
{
    public class SecurityAgent
    {
        #region User Info Operation
        
        public static Int32 InsertUserData(User user)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.InsertUserData(user);
            }            
            
        }

        public static void DeleteUserData(int id)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                umService.DeleteUserData(id);
            }            
        }

        public static int UpdateUserData(User user)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.UpdateUserData(user);
            }
        }

        public static int InserUserRoles(int userId, List<Role> roleList,int applicationId,int moduleId)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.InsertUserRoles(userId, roleList,applicationId,moduleId);
            }
        }
        
        public static List<User> GetUserList()
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                List<User> list = umService.GetUserList();
                return list;
            }

        }

        
        public static User GetUser(int id)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetUser(id);
            }
        }

        public static User GetUserByLoginId(string loginId)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetUserByLoginId(loginId);
            }
        }

        public static List<User> GetUserListByCraiteria(User user)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetUserListByCraiteria(user);
            }
        }
        


        #endregion
        
        #region Group Operation

        public static Int32 CreateGroup(UserGroup item,List<Role> roleList)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.CreateGroup(item,roleList);
            }
        }

        public static Int32 UpdateGroup(UserGroup item,List<Role> roleList)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.UpdateGroup(item, roleList);
            }
        }


        public static Int32 DeleteGroup(int id)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.DeleteGroup(id);
            }
        }

        public static UserGroup GetUserGroupById(int id)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetGroupById(id);
            }
        }

        public static List<UserGroup> GetGroupList()
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetGroupList();
            }
        }

        #endregion

        #region Roles
        public static List<Role> GetRoleList()
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetRolesList();
            }
        }

        public static List<Role> GetRoleListByCraiteria(Role role)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetRolesListByCraiteria(role);
            }
        }

        public static List<RoleGroup> GetRoleGroupList()
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetRoleGroups();
            }
        }

        public static List<Role> GetRoleListByUser(int userId)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetRolesListByUser(userId);
            }
        }

        public static List<Role> GetRolesListByUserGroup(int userGroupId)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetRolesListByUserGroup(userGroupId);
            }
        }

        public static Role GetRoleById(int roleId)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetRoleById(roleId);
            }
        }

        public static int InsertRole(Role role, List<Menu> menuList, List<Right> rightList)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.InsertRolesMenusRight(role, menuList, rightList);
            }
        }

        public static int UpdateRole(Role role, List<Menu> menuList, List<Right> rightList)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.UpdateRolesMenusRight(role, menuList, rightList);
            }
        }

        public static  Int32 CreateRoleGroup(RoleGroup item)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.CreateRoleGroup(item);
            }
        }


        public static  Int32 UpdateRoleGroup(RoleGroup item)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.UpdateRoleGroup(item);
            }
        }


        public static  Int32 DeleteRoleGroup(int Id)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.DeleteRoleGroup(Id);
            }
        }


        public static RoleGroup GetRoleGroupById(int Id)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetRoleGroupById(Id);
            }
        }

       

        #endregion

        #region Menus


        public static int InsertMenuData(Menu menu)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.InsertMenuData(menu);
            }
        }


        public static int UpdateMenuData(Menu menu)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.UpdateMenuData(menu);
            }
        }


        public static int DeleteMenuData(int id)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.DeleteMenuData(id);
            }
        }

        public static List<Menu> GetAllMenus()
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetAllMenus();
            }
        }

        public static List<Menu> GetMenusByParent(int parentId)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetMenusByParent(parentId);
            }
        }

        public static List<Menu> GetMenusByRoleId(int roleId)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetMenuListByRoleId(roleId);
            }
        }


        public static Menu GetMenusByMenuId(int menuId)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetMenuByMenuId(menuId);
            }
        }

        public static Menu GetMenuByMenuNameAndLoginId(string loginId, string MenuName)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetMenuByMenuNameAndLoginId(loginId, MenuName);
            }

        }

        public static List<Menu> GetMenusByApplicationAndModuleId(int roleId,int applicationId, int moduleId)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetMenusByApplicationAndModuleId(roleId,applicationId, moduleId);
            }
        }


        public static List<Menu> GetMenusByApplicationAndModuleName(string appName, string modName)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetMenusByApplicationAndModuleName(appName, modName);
            }
        }

        public static List<Menu> GetMenus(string loginId,string appName, string modName)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetMenus(loginId, appName, modName);
            }
        }


        

        #endregion

        #region Rights
        public static List<Right> GetAllRights()
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetAllRights();
            }
        }

        public static List<Right> GetAllRightsMapedByRole(int roleId)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetAllRightsMapedByRole(roleId);
            }
        }

        public static Right GetRightByLoginIdAndRightName(string loginId, string rightName)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetRightByLoginIdAndRightName(loginId, rightName);
            }

        }

        public static List<Right> GetRightByRoleAndAppAndModule(int roleId,int appId,int moduleId)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetRightsByRoleAndAppAndModule(roleId, appId, moduleId);
            }

        }


        #endregion


        #region "Department"
            public static List<Department> GetAllDepartment()
            {
                using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
                {
                    return umService.GetAllDepartment();
                }
            }
        #endregion

        #region "Employee"
        public static List<Employee> GetAllEmployee()
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
               return umService.GetAllEmployee();
            }
        }

        public static List<Employee> GetAllEmployeeWithPaging(int startRowIndex,int maxRow)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetAllEmployeeWithPaging(startRowIndex, maxRow);
            }
        }


        public static List<Employee> GetSearchEmployeeWithPaging(Employee obj, int startRowIndex, int maxRow, out int intTotalRowCount)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetSearchEmployeeWithPaging(out intTotalRowCount,obj, startRowIndex, maxRow);
            }
        }
       

        public static Int32 GetTotalEmployeeCount()
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetTotalEmployeeCount();
            }
        }
        
        #endregion

        #region Application
        public static List<Application> GetAllApplications()
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetAllApplications();
            }
        }


        public static Application GetApplicationById(int id)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetApplicationById(id);
            }
        }


        public static int CreateApplication(Application app)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.InsertApplicationData(app);
            }
        }

        public static int UpdateApplication(Application app)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.UpdateApplicationData(app);
            }
        }
        #endregion


        #region Module
        public static List<Module> GetAllModules()
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetAllModules();
            }
        }

        public static List<Module> GetModulesByApplicationId(int appId)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetModulesByApplicationId(appId);
            }
        }



        public static Module GetModuleById(int id)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.GetModuleById(id);

            }
        }


        public static int CreateModule(Module module)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.InsertModuleData(module);
            }
        }

        public static int UpdateModule(Module module)
        {
            using (AIMSUserManagementServiceClient umService = new AIMSUserManagementServiceClient())
            {
                return umService.UpdateModuleData(module);
            }
        }
        #endregion

        #region Validation

        //public static IEnumerable<RuleViolation> GetUserRuleViolations(User user)
        //{
        //    return null;
        //}

        public void ValidateUser(User user)
        {

        }

        #endregion
    }    
}