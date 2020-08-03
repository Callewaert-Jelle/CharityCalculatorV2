using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharityCalculatorV2.Models.Domain
{
    public interface IAppVariableRepository
    {
        AppVariable GetBy(string name);
        void SaveChanges();
    }
}
