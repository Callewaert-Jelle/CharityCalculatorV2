using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharityCalculatorV2.Models.Domain;
using CharityCalculatorV2.Models.DonationViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CharityCalculatorV2.Controllers
{
    [Authorize(Policy = "Donor")]
    public class DonorController : Controller
    {
        private readonly IAppVariableRepository _appVariableRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationUser _user;
        public DonorController(
            IAppVariableRepository appVariableRepository,
            UserManager<ApplicationUser> userManager)
        {
            _appVariableRepository = appVariableRepository;
            _userManager = userManager;
            GetCurrentUserAsync();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new DonationViewModel());
        }

        [HttpPost]
        public IActionResult Index(DonationViewModel model)
        {
            var values = ModelState.Values;
            if (ModelState.IsValid)
            {
                try
                {
                    var taxRate = Convert.ToDouble(_appVariableRepository.GetBy("TaxRate"));
                    Calculation calc = new Calculation(_user, model.Amount, taxRate, model.EventType);
                    // store in repository
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
                return RedirectToAction(nameof(Result), new { amount = model.Amount, eventType = model.EventType });
                // change this to comply with new Calculation class
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Result(double amount, string eventType)
        {
            try
            {
                AppVariable taxRateVariable = _appVariableRepository.GetBy("TaxRate");
                double taxRate = Convert.ToDouble(taxRateVariable.Value);
                var result = CalculateDeductableAmount(taxRate, amount, eventType);
                ViewData["amount"] = result;
            }
            catch(Exception e)
            {
                // do something?
            }
            return View();
        }

        private double CalculateDeductableAmount(double taxRate, double amount, string eventType)
        {
            GetCurrentUserAsync();
            Calculation calc = new Calculation(_user, amount, taxRate, eventType);
            // Add calculation to database (via repository)
            // _calculationRepository.Add(calc);
            return calc.CalculateDeductableAmount();
        }

        private async void GetCurrentUserAsync()
        {
            ApplicationUser user = await _userManager?.GetUserAsync(User);
            _user = user;
        }
    }
}
