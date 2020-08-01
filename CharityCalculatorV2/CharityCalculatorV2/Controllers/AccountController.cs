using CharityCalculatorV2.Models.AccountViewModels;
using CharityCalculatorV2.Models.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace CharityCalculatorV2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            // Clear existing cookie
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            await _signInManager.SignOutAsync();
            _logger.LogInformation(User.ToString());
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    // get logged in user
                    var appUser = _signInManager.UserManager.Users.SingleOrDefault(u => u.Email == model.Email);
                    // get user claims
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                    var claims = claimsPrincipal.Claims.ToList();

                    // detect if "donor" claim is present, show donor page
                    bool donorClaim = claims.Any(c => c.Value == "donor");
                    if (donorClaim)
                    {
                        return RedirectToAction(nameof(DonorController.Index), "Donor");
                    }
                    // should not be able to get here (eventually)
                    // but if anything goes wrong, return Home Index
                    return RedirectToAction(nameof(Homecontroller.Index), "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model); // Not needed (catch return)
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
