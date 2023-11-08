using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

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

    }
}
