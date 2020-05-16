using InnostageTest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace InnostageTest
{
    public class RoleInitializer
    {
        private readonly IConfiguration _configuration;

        public RoleInitializer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("operator") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("operator"));
            }

            foreach (var userInfo in _configuration.GetSection("PreferedUsers").GetChildren())
            {
                if(await userManager.FindByNameAsync(userInfo["Username"]) == null)
                {
                    var user = new User()
                    {
                        UserName = userInfo["Username"]
                    };

                    string password = userInfo["Password"];

                    var result = await userManager.CreateAsync(user, userInfo["Password"]);
                    if(result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "operator");
                    }
                }
            }
        }
    }
}
