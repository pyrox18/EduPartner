using EduPartner.MvcApp.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EduPartner.MvcApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly EduPartnerDbContext _context;

        public AdminController(EduPartnerDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetDatabase()
        {
            SeedData.ResetContext(_context);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}