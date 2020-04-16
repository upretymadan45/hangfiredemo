using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using hangfire.Models;
using Hangfire;
using System;
using hangfire.Services;

namespace hangfire.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Fire and Forget job"));

            BackgroundJob.Schedule(() => Console.WriteLine("Scheduled job"), TimeSpan.FromMinutes(1));

            RecurringJob.AddOrUpdate(() =>Console.WriteLine("Recurring Job"),"30 */18 * * *",TimeZoneInfo.Local);

            RecurringJob.AddOrUpdate(() => new EmailService().SendEmail("Hello how are you"), "10 05 * * *", TimeZoneInfo.Local);
            
            return View();
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
