using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using oars.Data;
using oars.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace oars.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public RolesController(
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context) : base()
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var roles = _roleManager.Roles;
            return View(await roles.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FormCollection collection)
        {
            string roleName = Request.Form["RoleName"];
            if (string.IsNullOrEmpty(roleName))
                ViewBag.FailedMessage = "Unable to create role, an empty name was entered";
            else
            {
                if (await _roleManager.RoleExistsAsync(roleName))
                    ViewBag.FailedMessage = "Unable to create role as role already exists";
                else
                {
                    IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                    if (result.Succeeded)
                        ViewBag.SuccessMessage = "Role created successfully !";
                    else
                        ViewBag.FailedMessage = "Role creation failed !";
                }
            }
            return View();

        }

        public async Task<IActionResult> Delete(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                TempData["FailedMessage"] = "Unable to delete role, empty role name entered";
            else
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                    TempData["FailedMessage"] = "Unable to delete role, role does not exist";
                else
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    IdentityResult result = await _roleManager.DeleteAsync(role);
                    if (!result.Succeeded)
                        TempData["FailedMessage"] = "Role deletion failed !";
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string roleName)
        {
            var thisRole = await _roleManager.FindByNameAsync(roleName);
            return View(thisRole);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IdentityRole role)
        {
            if (role != null)
            {
                IdentityResult result = await _roleManager.SetRoleNameAsync(await _roleManager.FindByIdAsync(role.Id), role.Name);
                await _context.SaveChangesAsync();
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    ViewBag.FailedMessage = "Failed to edit role";
            }
            return View();

        }

        public ActionResult ManageUserRoles()
        {

            ViewBag.Users = new SelectList(_context.Users.ToList());
            ViewBag.Roles = new SelectList(_context.Roles.ToList());

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RoleAddToUser(string UserName, string RoleName)
        {
            if (string.IsNullOrEmpty(RoleName) || string.IsNullOrEmpty(UserName))
                TempData["FailedMessage"] = "Entered Empty Values";
            else
            {
                var user = await _userManager.FindByEmailAsync(UserName);
                if (user != null)
                {
                    if (await _userManager.IsInRoleAsync(user, RoleName))
                    {
                        TempData["FailedMessage"] = "User already in role";
                    }
                    else
                    {
                        IdentityResult result = await _userManager.AddToRoleAsync(user, RoleName);
                        if (result.Succeeded)
                            TempData["SuccessMessage"] = "Succeeded adding role to user.";
                        else
                            TempData["FailedMessage"] = "Failed to add role to user.";
                    }
                }
                else
                    TempData["FailedMessage"] = "User does not exist.";
            }
            return RedirectToAction("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetRoles(string UserName)
        {
            if (!string.IsNullOrEmpty(UserName))
            {
                ViewBag.RolesUser = UserName;
                var user = await _userManager.FindByEmailAsync(UserName);
                if (user == null)
                {
                    TempData["FailedMessage"] = "Unable to retrieve roles, user does not exist.";
                    return RedirectToAction("ManageUserRoles");
                }
                ViewBag.RolesForThisUser = await _userManager.GetRolesAsync(user);
            }
            else
            {
                TempData["FailedMessage"] = "Unable to retrieve roles, empty username entered.";
                return RedirectToAction("ManageUserRoles");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteRoleForUser(string UserName, string RoleName)
        {
            if (string.IsNullOrEmpty(RoleName) || string.IsNullOrEmpty(UserName))
                TempData["FailedMessage"] = "Unable to delete role, empty role or username entered";
            else
            {
                var user = await _userManager.FindByEmailAsync(UserName);
                if (user == null)
                {
                    TempData["FailedMessage"] = "Unable to delete role, user does not exist.";
                    return RedirectToAction("ManageUserRoles");
                }
                else
                {
                    if (await _userManager.IsInRoleAsync(user, RoleName))
                    {
                        await _userManager.RemoveFromRoleAsync(user, RoleName);
                        @TempData["SuccessMessage"] = "Role removed from this user successfully !";
                    }
                    else
                    {
                        TempData["FailedMessage"] = "This user doesn't belong to selected role.";
                    }
                }
            }
            return RedirectToAction("ManageUserRoles");
        }
    }

}