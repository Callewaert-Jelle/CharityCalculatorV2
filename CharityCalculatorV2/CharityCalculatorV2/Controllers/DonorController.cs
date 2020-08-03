using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharityCalculatorV2.Models.Domain;
using CharityCalculatorV2.Models.DonationViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityCalculatorV2.Controllers
{
    [Authorize(Policy = "Donor")]
    public class DonorController : Controller
    {
        private readonly IAppVariableRepository _appVariableRepository;
        public DonorController(
            IAppVariableRepository appVariableRepository)
        {
            _appVariableRepository = appVariableRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(DonationViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Result), new { amount = model.Amount });
            }
            return View(model);
        }

        public IActionResult Result(double amount)
        {
            var result = CalculateDeductableAmount(amount);
            ViewData["amount"] = result;
            return View();
        }

        private double CalculateDeductableAmount(double amount)
        {
            AppVariable taxRateVariable = _appVariableRepository.GetBy("TaxRate");
            try
            {
                double taxRate = Convert.ToDouble(taxRateVariable.Value);
                return amount * (taxRate / (100 - taxRate));
            }
            catch (Exception e)
            {
                // Handle exception
            }
            // Change this!!!
            return 0;
        }
    }
}
