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
        [Range(0, int.MaxValue, ErrorMessage = "{0} may not be a negative value.")]
        public double Amount { get; set; }
        [BindProperty, Required(ErrorMessage = "Please select a type of event.")]
        [Display(Name = "Event type")]
        public string EventType { get; set; }
        public string[] EventTypes { get; set; } = new[] { "Running", "Swimming", "Other" };
    }
}
