using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharityCalculatorV2.Models.DonationViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CharityCalculatorV2.Controllers
{
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
                var result = CalculateDeductableAmount(model.Amount);
                return RedirectToAction(nameof(Result), new { amount = result });
            }
            return View(model);
        }

        public IActionResult Result(double amount)
        {
            ViewData["amount"] = amount;
            return View();
        }

        private double CalculateDeductableAmount(double amount)
        {
            const double taxRate = 20;
            return amount * (taxRate / (100 - taxRate));
        }
    }
}
