using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CharityCalculatorV2.Models.TaxRateViewModel
{
    public class TaxRateViewModel
    {
        [Required]
        [Range(0,100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public double TaxRate { get; set; }
    }
}
