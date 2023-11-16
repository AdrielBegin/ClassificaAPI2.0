using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Classifica3._0.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public AdminUsersController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet("UserAll")]
        public async Task<IEnumerable<IdentityUser>> GetAllUser()
        {
            var result = userManager.Users;
            return result;
        }
        
        [HttpPost("DeletarUsers")]
        public async Task<IActionResult> DeleteUser([FromBody] string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound($"Usuário com Id = {id} não foi encontrado");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return Ok();
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Ok("Excluido com sucesso");
        }

    }
}
