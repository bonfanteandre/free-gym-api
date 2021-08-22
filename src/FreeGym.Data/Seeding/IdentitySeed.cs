using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeGym.Data.Seeding
{
    public class IdentitySeed
    {
        public static async Task SeedUserAsync(UserManager<IdentityUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new IdentityUser
                {
                    Email = "admin@freegym.com",
                    UserName = "admin@freegym.com"
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
