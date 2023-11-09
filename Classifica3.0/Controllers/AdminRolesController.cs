using Classifica3._0.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Classifica3._0.Controllers
{
    [Area("Admin")]
    public class AdminRolesController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<IdentityUser> userManager;

        public AdminRolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet("GetAllRoles")]

        public async Task<IEnumerable<IdentityRole>> GetAllRoles()
        {
            var resul = roleManager.Roles;
            return resul;
        }


        [HttpPost("CreateUsersAdmin")]
        public async Task<IActionResult> Create([FromBody] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    Errors(result);
                }
            }
            return Ok();
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        [HttpGet("GetUpdate")]
        public async Task<IActionResult> Update(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);

            List<IdentityUser> members = new List<IdentityUser>();

            List<IdentityUser> nonMembers = new List<IdentityUser>();
            foreach (IdentityUser user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers; list.Add(user);
            }

            RoleEdit roleEdit = new RoleEdit { Role = role, Members = members, NonMembers = nonMembers };

            return View(roleEdit);
        }

    }
}
