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
    public class ChildrenController : Controller
    {
        private readonly EduPartnerDbContext _context;

        public ChildrenController(EduPartnerDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var children = await _context.Children.ToListAsync();

            ViewData["Children"] = children;

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind] RegisterViewModel model)
        {
            var child = new Child
            {
                Name = model.Name,
                DateOfBirth = model.DateOfBirth
            };

            var parent = await _context.Parents
                .Include(p => p.Children)
                .FirstAsync();

            parent.Children.Add(child);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}