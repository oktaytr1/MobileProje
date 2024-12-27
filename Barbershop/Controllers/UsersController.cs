using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Barbershop.Controllers
{
    public class UsersController:Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;

        }
        public IActionResult Index()
        {

            return View(_userManager.Users);
        }





        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");
        }

    }
}
