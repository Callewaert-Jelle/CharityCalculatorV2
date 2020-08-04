using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CharityCalculatorV2.Models.DonationViewModel
{
    public class DonationViewModel
    {
        [Required]
        public double Amount { get; set; }
        [BindProperty, Required]
        public string EventType { get; set; }
        public string[] EventTypes { get; set; } = new[] { "Running", "Swimming", "Other" };
    }
}
