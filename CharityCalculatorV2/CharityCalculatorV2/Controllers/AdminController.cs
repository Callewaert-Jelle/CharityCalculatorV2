using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharityCalculatorV2.Models.Domain;
using CharityCalculatorV2.Models.DonationViewModel;
using CharityCalculatorV2.Models.TaxRateViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityCalculatorV2.Controllers
{
    [Authorize(Policy ="AdminOnly")]
    public class AdminController : Controller
    {
        private readonly IAppVariableRepository _appVariableRepository;

        public AdminController(IAppVariableRepository appVariableRepository)
        {
            _appVariableRepository = appVariableRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            AppVariable taxRate = _appVariableRepository.GetBy("TaxRate");
            try
            {
                ViewBag.CurrentTaxRate = Convert.ToDouble(taxRate.Value);
            }
            catch(Exception e)
            {
                // do something
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(TaxRateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppVariable taxRate = _appVariableRepository.GetBy("TaxRate");
                    MapTaxRateViewModelToAppVariable(model, taxRate);
                    _appVariableRepository.SaveChanges();
                    ViewBag.CurrentTaxRate = model.TaxRate;
                }
                catch(Exception e)
                {
                    // do something?
                }
            }
            return View(model);
        }

        private void MapTaxRateViewModelToAppVariable(TaxRateViewModel taxRateViewModel, AppVariable appVariable)
        {
            appVariable.Value = taxRateViewModel.TaxRate.ToString();
        }
    }
}
