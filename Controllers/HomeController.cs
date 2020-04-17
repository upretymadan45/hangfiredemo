using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using hangfire.Models;
using Hangfire;
using System;
using hangfire.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace hangfire.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(ILogger<HomeController> logger,
                                UserManager<IdentityUser> userManager,
                                RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Fire and Forget job"));

            BackgroundJob.Schedule(() => Console.WriteLine("Scheduled job"), TimeSpan.FromMinutes(1));

            RecurringJob.AddOrUpdate(() =>Console.WriteLine("Recurring Job"),"30 */18 * * *",TimeZoneInfo.Local);

            RecurringJob.AddOrUpdate(() => new EmailService().SendEmail("Hello how are you"), "10 05 * * *", TimeZoneInfo.Local);
            
            return View();
        }

        public async Task<IActionResult> AddUserToAdminRole()
        {
            await roleManager.CreateAsync(new IdentityRole("HangfireAdmin"));
            var user = await userManager.FindByNameAsync("new@gmail.com");

            await userManager.AddToRoleAsync(user, "HangfireAdmin");

            return Ok();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
