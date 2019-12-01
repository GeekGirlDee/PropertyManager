using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PropertyManager.Services;
using PropertyManger.Areas.Dashboard.ViewModels;
using PropertyManger.Entities;
using PropertyManger.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PropertyManger.Areas.Dashboard.Controllers
{
    public class UsersController : Controller
    {
        private PMSSignInManager _signInManager;
        private PMSUserManager _userManager;
        private PMSRolesManager _roleManager;

        public PMSSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<PMSSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public PMSUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<PMSUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public PMSRolesManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<PMSRolesManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }


        public UsersController()
        {
        }

        public UsersController(PMSUserManager userManager, PMSSignInManager signInManager, PMSRolesManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        AccommodationPackageService accommodationPackageService = new AccommodationPackageService();
        PropertyTypesService propertyTypesService = new PropertyTypesService();
        public async Task<ActionResult> Index(string searchTerm, string roleID, int? page)
        {
            int recordSize = 5;
            page = page ?? 1;

            UsersListingModel model = new UsersListingModel();

            model.SearchTerm = searchTerm;
            model.RoleID = roleID;
            model.Roles = RoleManager.Roles.ToList();

            model.Users = await SearchUsers(searchTerm, roleID, page.Value, recordSize);

            var totalRecords = await SearchUsersCount(searchTerm, roleID);

            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        public async Task<IEnumerable<PropertyManagerUser>> SearchUsers(string searchTerm, string roleID, int page, int recordSize)
        {

            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))   //Not functioning. throws ana exception
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }

            if (!string.IsNullOrEmpty(roleID))
            {
                var role = await RoleManager.FindByIdAsync(roleID);

                var userIDs = role.Users.Select(x =>x.UserId).ToList();

                users = users.Where(x => userIDs.Contains(x.Id));
            }

            var skip = (page - 1) * recordSize;

            return users.OrderBy(x => x.Email).Skip(skip).Take(recordSize).ToList();
        }

        public async Task<int> SearchUsersCount(string searchTerm, string roleID)
        {

            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }

            if (!string.IsNullOrEmpty(roleID))
            {
                var role = await RoleManager.FindByIdAsync(roleID);

                var userIDs = role.Users.Select(x => x.UserId).ToList();

                users = users.Where(x => userIDs.Contains(x.Id));
            }

            return users.Count();
        }



        [HttpGet]
        public async Task<ActionResult> Action(string ID)
        {
            UserActionModel model = new UserActionModel();

            if (!string.IsNullOrEmpty(ID))
            {
                var user = await UserManager.FindByIdAsync(ID);

                model.ID = user.Id;
                model.FullName = user.FullName;
                model.Email = user.Email;
                model.Username = user.UserName;
                model.Country = user.Country;
                model.City = user.City;
                model.Address = user.Address;

            }
            
            return PartialView("_Action", model);
        }


        [HttpPost]
        public async Task<JsonResult> Action(UserActionModel model)
        {
            JsonResult json = new JsonResult();

            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.ID))
            {
                var user = await UserManager.FindByIdAsync(model.ID);

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.Username;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;

                result = await UserManager.UpdateAsync(user);
            }
            else
            {
                var user = new PropertyManagerUser();

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.Username;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;

                result = await UserManager.CreateAsync(user);
            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(", ", result.Errors) };

            return json;
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string ID)
        {
            UserActionModel model = new UserActionModel();

            var user = await UserManager.FindByIdAsync(ID);

            model.ID = user.Id;


            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(UserActionModel model)
        {
            JsonResult json = new  JsonResult();

            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.ID))
            {
                var user = await UserManager.FindByIdAsync(model.ID);

                result = await UserManager.DeleteAsync(user);

                json.Data = new { Success = result.Succeeded, Message = string.Join(", ", result.Errors) };
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid User!" };
            }
            

            return json;
        }

        [HttpGet]
        public async Task<ActionResult> UserRoles(string ID)
        {
           
            UserRolesModel model = new UserRolesModel();
            
            model.UserID = ID;
            var user = await UserManager.FindByIdAsync(ID);
            var userRolesIDs = user.Roles.Select(x => x.RoleId).ToList();
                      
            model.UserRoles = RoleManager.Roles.Where(x => userRolesIDs.Contains(x.Id)).ToList();

            model.Roles = RoleManager.Roles.Where(x=> !userRolesIDs.Contains(x.Id)).ToList();

            return PartialView("_UserRoles", model);
        }


        [HttpPost]
        public async Task<JsonResult> UserRoleOperation(string userID, string roleID, bool isDelete=false)
        {
            JsonResult json = new JsonResult();

            var user = await UserManager.FindByIdAsync(userID);

            var role = await RoleManager.FindByIdAsync(roleID);

            if (user != null && role != null)
            {
                IdentityResult result = null;

                if (!isDelete)
                {
                     result = await UserManager.AddToRoleAsync(userID, role.Name);
                }
                else 
                {
                     result = await UserManager.RemoveFromRoleAsync(userID, role.Name);
                }

                json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid operation" };
            }


            return json;
        }
    }
}