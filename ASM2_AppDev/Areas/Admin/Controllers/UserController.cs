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
        private readonly IPasswordHasher<IdentityUser> _passwordHash;
        public UserController(ApplicationDBContext dBContext, UserManager<IdentityUser> userManager, IPasswordHasher<IdentityUser> passwordHash)
        {
            _dbContext = dBContext;
            _userManager = userManager;
            _passwordHash = passwordHash;
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
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string id, string password)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = _passwordHash.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "User");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
        }


    }
}
