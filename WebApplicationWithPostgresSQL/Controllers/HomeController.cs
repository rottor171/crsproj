using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationWithPostgresSQL.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult AdminPanel() // TODO: int login, int password
        {
            // TODO: staff login, password in DB
            //var deposit = await _context.Deposit
            //    .Include(d => d.DepClientNoNavigation)
            //    .Include(d => d.DepStaffNoNavigation)
            //    .Where(d => d.DepClientNo == passport)
            //    .SingleOrDefaultAsync(m => m.DepositNo == id);

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
