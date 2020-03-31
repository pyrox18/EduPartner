using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduPartner.MvcApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduPartner.MvcApp.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly EduPartnerDbContext _context;

        public InvoicesController(EduPartnerDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Invoices"] = await _context.Invoices
                .Include(i => i.Items)
                .ToListAsync();

            return View();
        }

        public async Task<IActionResult> Invoice(Guid id)
        {
            ViewData["Invoice"] = await _context.Invoices
                .Include(i => i.Items)
                .FirstAsync(i => i.Id == id);

            return View();
        }
    }
}