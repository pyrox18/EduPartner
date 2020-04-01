using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduPartner.MvcApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduPartner.MvcApp.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly EduPartnerDbContext _context;

        public EnrollmentsController(EduPartnerDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Enrollment(Guid id)
        {
            ViewData["Enrollment"] = await _context.Enrollments
                .Include(e => e.Child)
                .Include(e => e.Subject)
                .Include(e => e.Teacher)
                .FirstAsync(e => e.Id == id);

            return View();
        }
    }
}