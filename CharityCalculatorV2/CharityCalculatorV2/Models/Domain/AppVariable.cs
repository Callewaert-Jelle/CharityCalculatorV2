using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharityCalculatorV2.Models.Domain
{
    public class AppVariable
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public AppVariable(string name, string value)
        {
            if (name.Length < 0 || value.Length < 0)
            {
                throw new ArgumentException();
            }
            Name = name;
            Value = value;
        }
    }
}
