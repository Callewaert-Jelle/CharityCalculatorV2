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
        [Required(ErrorMessage = "Please enter a number.")]
        [RegularExpression(@"[0-9]*[,]?[0-9]*", ErrorMessage = "No characters allowed.")]
        public double Amount { get; set; }
        [BindProperty, Required(ErrorMessage = "Please select a type of event.")]
        public string EventType { get; set; }
        public string[] EventTypes { get; set; } = new[] { "Running", "Swimming", "Other" };
    }
}
