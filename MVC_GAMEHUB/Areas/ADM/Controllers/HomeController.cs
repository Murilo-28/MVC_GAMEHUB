using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_GAMEHUB.Models;
using System.Diagnostics;

namespace MVC_GAMEHUB.Areas.ADM.Controllers
{
    [Area("ADM")]
    public class HomeController : Controller
    {
        private readonly Contexto _context;

        public HomeController(Contexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> Loja()
        {
            var jogos = await _context.Jogos.ToListAsync();
            return View(jogos);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
