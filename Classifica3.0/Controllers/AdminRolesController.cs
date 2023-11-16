using Classifica3._0.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Data;

namespace Classifica3._0.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]

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

        [HttpGet("MembersAndNomember")]
        public async Task<IActionResult> Update([FromBody] string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);

            List<IdentityUser> members = new List<IdentityUser>();

            List<IdentityUser> nonMembers = new List<IdentityUser>();
            foreach (IdentityUser user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers; list.Add(user);
            }

            RoleEdit roleEdit = new RoleEdit { Role = role, Members = members, NonMembers = nonMembers };

            return Ok(roleEdit);
        }

        [HttpPost("CreatePutRole")]
        public async Task<IActionResult> Update([FromBody] RoleModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    IdentityUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            Errors(result);
                        }
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    IdentityUser user = await userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            Errors(result);
                        }
                    }
                }

            }
            if (ModelState.IsValid)
            {
                return Ok();
            }
            else
            {
                return await Update(model.RoleId);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (!result.Succeeded)
                {
                    Errors(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "Não encontrado");
            }
            return Ok(roleManager.Roles);
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
