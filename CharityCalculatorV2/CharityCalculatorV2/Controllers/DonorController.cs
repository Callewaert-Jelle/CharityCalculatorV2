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

        [HttpGet]
        public IActionResult Index()
        {
            return View(new DonationViewModel());
        }
        [HttpPost]
        public IActionResult Index(DonationViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Result), new { amount = model.Amount, eventType = model.EventType });
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
            // hardcoded for now
            double supplementAmplifier = 1;
            switch (eventType)
            {
                case "Running": 
                    supplementAmplifier = 1.05;
                    break;
                case "Swimming": 
                    supplementAmplifier = 1.03;
                    break;
                default: 
                    supplementAmplifier = 1;
                    break;
            }
            double deductibleAmount = amount * supplementAmplifier * (taxRate / (100 - taxRate));
            deductibleAmount = Math.Round(deductibleAmount, 2, MidpointRounding.AwayFromZero);
            return deductibleAmount;
        }
    }
}
