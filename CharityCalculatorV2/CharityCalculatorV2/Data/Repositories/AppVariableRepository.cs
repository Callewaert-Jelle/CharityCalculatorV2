using CharityCalculatorV2.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharityCalculatorV2.Data.Repositories
{
    public class AppVariableRepository : IAppVariableRepository
    {
        private readonly DbSet<AppVariable> _appVariables;

        public AppVariableRepository(ApplicationDbContext context)
        {
            _appVariables = context.AppVariables;
        }

        public AppVariable GetBy(string name)
        {
            return _appVariables.FirstOrDefault(av => av.Name == name);
        }
    }
}
