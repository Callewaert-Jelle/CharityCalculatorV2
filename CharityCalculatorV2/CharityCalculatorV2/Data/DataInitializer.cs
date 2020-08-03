using CharityCalculatorV2.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CharityCalculatorV2.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public DataInitializer(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                // Seed database
                AppVariable taxRate = new AppVariable { Name = "TaxRate", Value = "20" };
                _dbContext.AppVariables.Add(taxRate);
                _dbContext.SaveChanges();
                // Seed users
                await InitializeUsers();
            }
        }

        private async Task InitializeUsers()
        {
            string emailAdress = "donor@donor.com";
            string password = "DonorPassword";
            ApplicationUser donorUser = new ApplicationUser { UserName = emailAdress, Email = emailAdress, UserId = emailAdress };
            var user = await _userManager.CreateAsync(donorUser, password);
            await _userManager.AddClaimAsync(donorUser, new Claim(ClaimTypes.Role, "donor"));

            emailAdress = "admin@admin.com";
            password = "AdminPassword";
            ApplicationUser adminUser = new ApplicationUser { UserName = emailAdress, Email = emailAdress, UserId = emailAdress };
            await _userManager.CreateAsync(adminUser, password);
            await _userManager.AddClaimAsync(adminUser, new Claim(ClaimTypes.Role, "admin"));

            emailAdress = "event@event.com";
            password = "EventPassword";
            ApplicationUser eventUser = new ApplicationUser { UserName = emailAdress, Email = emailAdress, UserId = emailAdress };
            await _userManager.CreateAsync(eventUser, password);
            await _userManager.AddClaimAsync(eventUser, new Claim(ClaimTypes.Role, "event"));

            _dbContext.SaveChanges();
        }
    }
}
