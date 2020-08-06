using CharityCalculatorV2.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharityCalculator.Tests.Data
{
    class DummyDbContext
    {
        public IEnumerable<AppVariable> AppVariables { get; }
        public AppVariable TaxRate { get; }

        public DummyDbContext()
        {
            TaxRate = new AppVariable("TaxRate", "20");
            AppVariable dummyVariable = new AppVariable("Dummy", "variable");
            AppVariables = new[] { TaxRate, dummyVariable };
        }
    }
}
