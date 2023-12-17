using ASM2_AppDev.Data;
using ASM2_AppDev.Models;
using ASM2_AppDev.Models.ViewModels;
using ASM2_AppDev.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace ASM2_AppDev.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public UserController(ApplicationDBContext dBContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dBContext;
            _userManager = userManager;
        }
        public IActionResult Index()
        {

            List<ApplicationUser> usersList = _dbContext.applicationUsers.ToList();
            var userRoles = _dbContext.UserRoles.ToList();
            var roles = _dbContext.Roles.ToList();

            foreach (var user in usersList)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
            }
            return View(usersList);
        }
        
        public IActionResult Edit(string? id)
        {
            RoleManagementVM roleManagementVM = new()
            {
                ApplicationUser = _dbContext.applicationUsers.FirstOrDefault(u => u.Id.Equals(id)),
                RoleList = _dbContext.Roles.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Name
                })
            };
            var roleId = _dbContext.UserRoles.FirstOrDefault(u => u.UserId == id).RoleId;
            roleManagementVM.ApplicationUser.Role = _dbContext.Roles.FirstOrDefault(u => u.Id == roleId).Name;

            return View(roleManagementVM);

        }


        [HttpPost]
        public IActionResult Edit(RoleManagementVM obj)
        {
            var roleId = _dbContext.UserRoles.FirstOrDefault(u => u.UserId == obj.ApplicationUser.Id).RoleId;
            var oldRole = _dbContext.Roles.FirstOrDefault(u => u.Id == roleId).Name;
            if (!(obj.ApplicationUser.Role == oldRole))
            {
                ApplicationUser applicationUser = _dbContext.applicationUsers.FirstOrDefault(u => u.Id == obj.ApplicationUser.Id);
                _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(applicationUser, obj.ApplicationUser.Role).GetAwaiter().GetResult();
            }

            return RedirectToAction("Index", "User");

        }

       
    }
}
