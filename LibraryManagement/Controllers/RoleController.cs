using LibraryManagementModels.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index() => View(_roleManager.Roles);

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (!string.IsNullOrWhiteSpace(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> ManageUserRoles(string userId)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);
        //    var model = new ManageUserRolesViewModel
        //    {
        //        UserId = userId,
        //        Roles = await _userManager.GetRolesAsync(user)
        //    };

        //    ViewBag.Roles = _roleManager.Roles.ToList();
        //    return View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> UpdateUserRoles(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var currentRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRolesAsync(user, roles);

            return RedirectToAction("Index");
        }
    }


}
