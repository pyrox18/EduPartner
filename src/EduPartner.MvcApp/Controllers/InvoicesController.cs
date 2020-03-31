using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduPartner.MvcApp.Data;
using EduPartner.MvcApp.Data.Models;
using EduPartner.MvcApp.ViewModels;
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

        public async Task<IActionResult> PaymentDummy(Guid invoiceId)
        {
            ViewData["Invoice"] = await _context.Invoices
                .Include(i => i.Items)
                .FirstAsync(i => i.Id == invoiceId);

            return View();
        }

        [HttpPost("{controller}/PaymentDummy/Pay")]
        public async Task<IActionResult> PaymentDummy([Bind] PaymentDummyViewModel model)
        {
            var invoice = await _context.Invoices
                .FirstAsync(i => i.Id == model.InvoiceId);

            invoice.Status = InvoiceStatus.Paid;

            await _context.SaveChangesAsync();

            TempData["IsPaid"] = true;

            return RedirectToAction(nameof(Invoice), new { id = model.InvoiceId });
        }
    }
}