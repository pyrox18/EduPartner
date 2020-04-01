using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using EduPartner.MvcApp.Data;
using EduPartner.MvcApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace EduPartner.MvcApp.Controllers
{
    [Route("api/schedule")]
    [ApiController]
    public class ScheduleApiController : ControllerBase
    {
        private readonly EduPartnerDbContext _context;

        public ScheduleApiController(EduPartnerDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("events")]
        public async Task<IActionResult> GetScheduleEvents(string start, string end)
        {
            DateTimeOffset startDateTime, endDateTime;

            if (DateTimeOffset.TryParse(start, out DateTimeOffset startDt))
            {
                startDateTime = startDt;
            }
            else
            {
                startDateTime = DateTimeOffset.Now;
            }

            if (DateTimeOffset.TryParse(end, out DateTimeOffset endDt))
            {
                endDateTime = endDt;
            }
            else
            {
                endDateTime = startDateTime.AddMonths(1);
            }

            var enrollments = await _context.Enrollments
                .Include(e => e.Child)
                .Include(e => e.Subject)
                .Include(e => e.Teacher)
                .ToListAsync();

            var events = new List<ScheduleEventViewModel>();

            foreach (var enrollment in enrollments)
            {
                int daysUntilNextOccurrence = ((int)enrollment.TimeslotDayOfWeek - (int)startDateTime.DayOfWeek + 7) % 7;
                var nextOccurrence = startDateTime.AddDays(daysUntilNextOccurrence);

                nextOccurrence = new DateTimeOffset(nextOccurrence.Date + new TimeSpan(enrollment.TimeslotTime.Hour, enrollment.TimeslotTime.Minute, 0), nextOccurrence.Offset);
                int idCounter = 1;

                do
                {
                    events.Add(new ScheduleEventViewModel
                    {
                        Id = $"{enrollment.Id}+{idCounter}",
                        Title = $"{enrollment.Subject.Name}: {enrollment.Child.Name} with {enrollment.Teacher.Name}",
                        Start = ZonedDateTime.FromDateTimeOffset(nextOccurrence).ToString("yyyy-MM-ddTHH:mm:sso<g>", CultureInfo.InvariantCulture),
                        End = ZonedDateTime.FromDateTimeOffset(nextOccurrence.AddMinutes(90)).ToString("yyyy-MM-ddTHH:mm:sso<g>", CultureInfo.InvariantCulture),
                        Url = "#"
                    });

                    nextOccurrence = nextOccurrence.AddDays(7);
                    idCounter++;
                } while (nextOccurrence <= endDateTime);
            }

            return Ok(events);
        }
    }
}