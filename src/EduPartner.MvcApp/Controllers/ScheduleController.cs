using EduPartner.MvcApp.Data;
using EduPartner.MvcApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace EduPartner.MvcApp.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly EduPartnerDbContext _context;

        public ScheduleController(EduPartnerDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}