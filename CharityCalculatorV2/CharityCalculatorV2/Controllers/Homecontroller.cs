using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CharityCalculatorV2.Controllers
{
    public class Homecontroller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
