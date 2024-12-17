using Microsoft.AspNetCore.Mvc;
using testasp.net_core.Models;

namespace testasp.net_core.Controllers
{
    public class HomeController : Controller
    {
        // Opretter menuen med retter og priser
        private readonly List<Vare> Menu = new List<Vare>
        {
            new Vare { ret = "Pasta", pris = 90 },
            new Vare { ret = "Pizza", pris = 70 },
            new Vare { ret = "Ris", pris = 50 }
        };

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Menu = Menu; // Sender menuen til view
            return View();
        }

        [HttpPost]
        public IActionResult Index(string valgtRet, int alder)
        {
            ViewBag.Menu = Menu;

            // Finder den valgte ret
            Vare? valgtVare = Menu.FirstOrDefault(v => v.ret == valgtRet);
            if (valgtVare != null)
            {
                int pris = valgtVare.GetPris(alder);
                ViewBag.PrisAtBetale = pris;
                ViewBag.ValgtRet = valgtRet;
            }
            return View();
        }
    }
}