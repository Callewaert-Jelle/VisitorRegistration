using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VisitorRegistration.Models.Domain;

namespace VisitorRegistration.Data
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public DbInitializer(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                await InitializeUsers();

                Visitor v1 = new Visitor { 
                    Name = "John", 
                    LastName = "Doe", 
                    VisitorType = VisitorType.Supplier, 
                    Entered = DateTime.Now, 
                    Company = "CompanyX", 
                    LicensePlate = "abc - 123" };
                Visitor v2 = new Visitor { 
                    Name = "Jane", 
                    LastName = "Doe", 
                    VisitorType = VisitorType.Applicant, 
                    Entered = DateTime.Now, 
                    Company = "CompanyY", 
                    LicensePlate = "123 - xyz" };
                Visitor v3 = new Visitor { 
                    Name = "Patrick", 
                    LastName = "Star", 
                    VisitorType = VisitorType.Visitor, 
                    Entered = DateTime.Now, 
                    Left = DateTime.Now.AddMinutes(10.0)
                };

                _dbContext.Visitors.Add(v1);
                _dbContext.Visitors.Add(v2);
                _dbContext.Visitors.Add(v3);

                _dbContext.SaveChanges();
            }
        } 

        private async Task InitializeUsers()
        {
            string userName = "Admin";
            IdentityUser user = new IdentityUser { UserName = userName };
            await _userManager.CreateAsync(user, "Admin123!");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "admin"));
        }
    }
}
