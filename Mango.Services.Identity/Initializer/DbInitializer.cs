using IdentityModel;
using Mango.Services.Identity.DbContexts;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mango.Services.Identity.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            if (_roleManager.FindByNameAsync(SD.Admin).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
            }
            else { return;  }

            ApplicationUser adminUser = new ApplicationUser()
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                PhoneNumber = "00000000000",
                FirstName = "Admin",
                LastName = "Admin"
            };

            _userManager.CreateAsync(adminUser, "admin").GetAwaiter().GetResult();
            _userManager.CreateAsync(adminUser, SD.Admin).GetAwaiter().GetResult();

            var temp1 = _userManager.AddClaimsAsync(adminUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, adminUser.FirstName + " " + adminUser.LastName),
                new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
                new Claim(JwtClaimTypes.Role, SD.Admin),
            }).Result;

            ApplicationUser customerUser = new ApplicationUser()
            {
                UserName = "customer@customer.com",
                Email = "customer@customer.com",
                EmailConfirmed = true,
                PhoneNumber = "00000000000",
                FirstName = "Customer",
                LastName = "Customer"
            };

            _userManager.CreateAsync(customerUser, "customer").GetAwaiter().GetResult();
            _userManager.CreateAsync(customerUser, SD.Customer).GetAwaiter().GetResult();

            Claim claim1 = new Claim(JwtClaimTypes.Name, customerUser.FirstName + " " + customerUser.LastName);
            Claim claim2 = new Claim(JwtClaimTypes.GivenName, customerUser.FirstName);
            Claim claim3 = new Claim(JwtClaimTypes.FamilyName, customerUser.LastName);
            Claim claim4 = new Claim(JwtClaimTypes.Role, SD.Customer);

/*            Claim[] claims1 = new Claim[4];
            claims1.Append<Claim>(claim1);
            claims1.Append<Claim>(claim2);
            claims1.Append<Claim>(claim3);
            claims1.Append<Claim>(claim4);*/

            var temp2 = _userManager.AddClaimsAsync(customerUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, customerUser.FirstName + " " + customerUser.LastName),
                new Claim(JwtClaimTypes.GivenName, customerUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, customerUser.LastName),
                new Claim(JwtClaimTypes.Role, SD.Customer),
            }).Result;
        }
    }
}
