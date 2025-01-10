using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WordsHeavenPrj.Models;
using Microsoft.Extensions.Logging;

namespace WordsHeavenPrj.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AdminController> _logger;

        public AdminController(UserManager<ApplicationUser> userManager, ILogger<AdminController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

       

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();

            var userWithRoles = new List<UserWithRolesViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userWithRoles.Add(new UserWithRolesViewModel
                {
                    User = user,
                    Roles = roles
                });
            }

            return View(userWithRoles);
        }

        public async Task<IActionResult> ToggleUserStatus(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.IsActive = !user.IsActive;
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }





        // Edit 

        public async Task<IActionResult> EditUser(string id) { 
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) {
                return NotFound(); 
            } 
            var model = new EditUserModel { 
                Id = user.Id, Name = user.Name, Email = user.Email, PhoneNumber = user.PhoneNumber, Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() 
            }; 
            
            return View(model); 
        }


        [HttpPost] 
        public async Task<IActionResult> EditUser(EditUserModel model) { 
            if (!ModelState.IsValid) { 
                return View(model); 
            } 
            var user = await _userManager.FindByIdAsync(model.Id); 
            if (user == null) { 
                return NotFound(); 
            } 
            user.Name = model.Name; 
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber; 
            
            var result = await _userManager.UpdateAsync(user); if (result.Succeeded) { 
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles); 
                await _userManager.AddToRoleAsync(user, model.Role); 
                return RedirectToAction("Index"); 
            } 
            
            foreach (var error in result.Errors) {
                ModelState.AddModelError(string.Empty, error.Description); 
            } 

            return View(model); 
        }


        // Delete 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View("Index", await _userManager.Users.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user {UserId}", id);
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
