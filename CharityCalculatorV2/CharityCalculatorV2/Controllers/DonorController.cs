using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharityCalculatorV2.Models.DonationViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityCalculatorV2.Controllers
{
    [Authorize(Policy = "Donor")]
    public class DonorController : Controller
    {
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
            const double taxRate = 20;
            return amount * (taxRate / (100 - taxRate));
        }
    }
}
