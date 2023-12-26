using ASM2_AppDev.Data;
using ASM2_AppDev.Models;
using ASM2_AppDev.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASM2_AppDev.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHash;
        public UserController(ApplicationDBContext dBContext, UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> passwordHash)
        {
            _dbContext = dBContext;
            _userManager = userManager;
            _passwordHash = passwordHash;
        }
        public IActionResult Index()
        {

            List<ApplicationUser> usersList = _dbContext.Users.ToList();
            var userRoles = _dbContext.UserRoles.ToList();
            var roles = _dbContext.Roles.ToList();

            foreach (var user in usersList)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
            }
            return View(usersList);
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
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = _passwordHash.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    TempData["success"] = "User changed their password successfully";
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
