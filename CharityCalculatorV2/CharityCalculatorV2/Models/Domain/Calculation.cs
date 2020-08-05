using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharityCalculatorV2.Models.Domain
{
    public class Calculation
    {
        public ApplicationUser User { get; set; }
        public double AmountInserted { get; set; }
        public double UsedTaxRate { get; set; }
        public string EventType { get; set; }

        public Calculation(ApplicationUser user, double amountInserted, double usedTaxRate, string eventType)
        {
            User = user;
            AmountInserted = amountInserted;
            UsedTaxRate = usedTaxRate;
            EventType = eventType;
        }

        public double CalculateDeductableAmount()
        {
            double supplementAmplifier = 1;
            switch (EventType)
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
            double deductibleAmount = AmountInserted * supplementAmplifier * (UsedTaxRate / (100 - UsedTaxRate));
            deductibleAmount = Math.Round(deductibleAmount, 2, MidpointRounding.AwayFromZero);
            return deductibleAmount;
        }
    }
}
