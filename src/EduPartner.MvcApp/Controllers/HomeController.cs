using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EduPartner.MvcApp.ViewModels;
using EduPartner.MvcApp.Data;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using System.Globalization;

namespace EduPartner.MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EduPartnerDbContext _context;

        public HomeController(ILogger<HomeController> logger, EduPartnerDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            ViewData["CurrentUserName"] = await _context.Parents
                .Select(p => p.Name)
                .FirstAsync();

            var enrollments = await _context.Enrollments
                .Include(e => e.Child)
                .Include(e => e.Subject)
                .Include(e => e.Teacher)
                .ToListAsync();

            var events = new List<DashboardUpcomingEventViewModel>();
            var startDateTime = DateTimeOffset.Now;

            foreach (var enrollment in enrollments)
            {
                int daysUntilNextOccurrence = ((int)enrollment.TimeslotDayOfWeek - (int)startDateTime.DayOfWeek + 7) % 7;
                var nextOccurrence = startDateTime.AddDays(daysUntilNextOccurrence);

                nextOccurrence = new DateTimeOffset(nextOccurrence.Date + new TimeSpan(enrollment.TimeslotTime.Hour, enrollment.TimeslotTime.Minute, 0), nextOccurrence.Offset);
                int idCounter = 1;

                events.Add(new DashboardUpcomingEventViewModel
                {
                    ChildName = enrollment.Child.Name,
                    SubjectName = enrollment.Subject.Name,
                    StartTimestamp = nextOccurrence,
                    IsHomeTutoring = enrollment.IsHomeTutoring
                });

                nextOccurrence = nextOccurrence.AddDays(7);
                idCounter++;
            }

            ViewData["Events"] = events.OrderBy(e => e.StartTimestamp).ToList();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
