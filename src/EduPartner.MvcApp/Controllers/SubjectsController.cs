using EduPartner.MvcApp.Data;
using EduPartner.MvcApp.Data.Models;
using EduPartner.MvcApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EduPartner.MvcApp.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly EduPartnerDbContext _context;

        public SubjectsController(EduPartnerDbContext context)
        {
            _context = context;
        }

        [HttpGet("{controller}/Enroll/1")]
        public async Task<IActionResult> EnrollmentSubjectSelection([FromQuery] Guid childId)
        {
            ViewData["ChildId"] = childId;

            ViewData["Children"] = await _context.Children
                .Include(c => c.Enrollments)
                    .ThenInclude(c => c.Subject)
                .ToListAsync();

            ViewData["Subjects"] = await _context.Subjects
                .ToListAsync();

            return View();
        }

        [HttpGet("{controller}/Enroll/2")]
        public async Task<IActionResult> EnrollmentTimeslotSelection([FromQuery(Name = "ChildId")] Guid childId, [FromQuery(Name = "SubjectId")] Guid subjectId)
        {
            var child = await _context.Children
                .FirstOrDefaultAsync(c => c.Id == childId);

            var subject = await _context.Subjects
                .Include(s => s.Teachers)
                .FirstOrDefaultAsync(s => s.Id == subjectId);

            var unavailableTimeslots = await _context.Enrollments
                .Include(e => e.Subject)
                .Include(e => e.Teacher)
                .Where(e => e.Subject.Id == subjectId)
                .Select(e => new TimeslotViewModel
                {
                    TeacherId = e.Teacher.Id,
                    DayOfWeek = e.TimeslotDayOfWeek,
                    Time = e.TimeslotTime
                })
                .ToListAsync();

            ViewData["Child"] = child;
            ViewData["Subject"] = subject;
            ViewData["UnavailableTimeslots"] = unavailableTimeslots;

            return View();
        }

        [HttpGet("{controller}/Enroll/3")]
        public async Task<IActionResult> EnrollmentConfirmation(
            [FromQuery(Name = "ChildId")] Guid childId,
            [FromQuery(Name = "SubjectId")] Guid subjectId,
            [FromQuery(Name = "TimeslotSelection")] string timeslotSelection,
            [FromQuery(Name = "IsHomeTutoring")] bool isHomeTutoring)
        {
            var child = await _context.Children
                .FirstOrDefaultAsync(c => c.Id == childId);

            var subject = await _context.Subjects
                .Include(s => s.Teachers)
                .FirstOrDefaultAsync(s => s.Id == subjectId);

            var timeslotData = timeslotSelection.Split('|'); // "teacherId|dayOfWeekInt|time (h:mm tt)"

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(t => t.Id == Guid.Parse(timeslotData[0]));

            var timeslotDayOfWeek = (DayOfWeek)int.Parse(timeslotData[1]);

            var timeslotTime = DateTime.Parse(timeslotData[2]);

            ViewData["Child"] = child;
            ViewData["Subject"] = subject;
            ViewData["Teacher"] = teacher;
            ViewData["TimeslotDayOfWeek"] = timeslotDayOfWeek;
            ViewData["TimeslotTime"] = timeslotTime;
            ViewData["IsHomeTutoring"] = isHomeTutoring;

            return View();
        }

        [HttpPost("{controller}/Enroll")]
        public async Task<IActionResult> Enroll([Bind] SubjectEnrollmentViewModel model)
        {
            var enrollment = new Enrollment
            {
                Child = await _context.Children.FirstAsync(c => c.Id == model.ChildId),
                Subject = await _context.Subjects.FirstAsync(s => s.Id == model.SubjectId),
                Teacher = await _context.Teachers.FirstAsync(t => t.Id == model.TeacherId),
                TimeslotDayOfWeek = model.TimeslotDayOfWeek,
                TimeslotTime = model.TimeslotTime,
                IsHomeTutoring = model.IsHomeTutoring
            };

            await _context.Enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();

            TempData["IsEnrolled"] = true;

            return RedirectToAction(nameof(ChildrenController.Child), "Children", new { id = model.ChildId });
        }
    }
}