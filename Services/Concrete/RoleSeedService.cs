using EmployeeApi.Data;
using EmployeeApi.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmployeeApi.Services.Concrete
{
    public class RoleSeedService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public RoleSeedService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task SeedData()
        {
            if (!_context.UserRoles.Any())
            {
                await SeedRolesAndAdminAsync();
            }
        }
        public async Task SeedRolesAndAdminAsync()
        {
            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new ApplicationRole(role));

                    var email = role == "Admin" ? "admin@gmail.com" : "user@gmail.com";
                    var username = role.ToLower();
                    var password = role == "Admin" ? "Admin@123" : "User@123";

                    var existingUser = await _userManager.FindByEmailAsync(email);
                    if (existingUser == null)
                    {
                        var user = new ApplicationUser
                        {
                            UserName = username,
                            Email = email,
                            EmailConfirmed = true,
                            FullName = role == "Admin" ? "Admin User" : "Normal User"
                        };

                        var result = await _userManager.CreateAsync(user, password);
                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, role);
                        }
                    }
                    //if (role == "Admin")
                    //{
                    //    var user = new ApplicationUser
                    //    {
                    //        UserName = "admin",
                    //        Email = "admin@gmail.com",
                    //        EmailConfirmed = true,
                    //        PasswordHash = "Admin@123"
                    //    };
                    //    var result = await _userManager.CreateAsync(user, "Admin@123");
                    //    if (result.Succeeded)
                    //    {
                    //        await _userManager.AddToRoleAsync(user, "Admin");
                    //    }
                    //}
                    //else
                    //{
                    //    var user = new ApplicationUser
                    //    {
                    //        UserName = "user",
                    //        Email = "user@gmail.com",
                    //        EmailConfirmed = true
                    //    };
                    //    var result = await _userManager.CreateAsync(user, "User@123");
                    //    if (result.Succeeded)
                    //    {
                    //        await _userManager.AddToRoleAsync(user, "User");
                    //    }
                    //}
                }
            }

            //var adminEmail = "admin@gmail.com";
            //var userEmail = "user@gmail.com";
            //var adminUser = await _userManager.FindByEmailAsync(adminEmail);

            //if (adminUser == null)
            //{
            //    var user = new ApplicationUser
            //    {
            //        UserName = "admin",
            //        Email = adminEmail,
            //        FullName = "Admin User",
            //        EmailConfirmed = true
            //    };
            //    //Email: admin@gmail.com
            //    //Password: Admin@123
            //    //Role: Admin
            //    var result = await _userManager.CreateAsync(user, "Admin@123");
            //    if (result.Succeeded)
            //    {
            //        await _userManager.AddToRoleAsync(user, "Admin");
            //    }
            //}

            //var normalUser = await _userManager.FindByEmailAsync(userEmail);
            //if (normalUser == null)
            //{
            //    normalUser = new ApplicationUser
            //    {
            //        UserName = "user@gmail.com",
            //        Email = userEmail,
            //        EmailConfirmed = true
            //    };

            //    var result_user = await _userManager.CreateAsync(normalUser, "User@123");
            //    if (result_user.Succeeded)
            //    {
            //        await _userManager.AddToRoleAsync(normalUser, "User");
            //    }
            //}
        }
    }
}
