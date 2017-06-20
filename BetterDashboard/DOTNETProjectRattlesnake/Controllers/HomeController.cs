using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DOTNETProjectRattlesnake.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload()
        {
            ViewData["Message"] = "Your upload page.";

            return View();
        }

        public IActionResult Record()
        {
            ViewData["Message"] = "Your recording page.";

            return View();
        }

        public IActionResult Transcripts()
        {
            ViewData["Message"] = "Your transcripts page.";

            return View();
        }

        public IActionResult Settings()
        {
            ViewData["Message"] = "Your settings page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
