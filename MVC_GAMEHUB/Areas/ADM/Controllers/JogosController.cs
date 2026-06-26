using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_GAMEHUB.Models;

namespace MVC_GAMEHUB.Areas.ADM.Controllers
{
    [Area("ADM")]
    public class JogosController : Controller
    {
        private readonly Contexto _context;

        public JogosController(Contexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> Loja()
        {
            var jogos = await _context.Jogos.ToListAsync();
            return View(jogos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Jogo jogo)
        {
            if (!ModelState.IsValid)
            {
                foreach (var erro in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("ERRO: " + erro.ErrorMessage);
                }
                return View(jogo);
            }

            _context.Jogos.Add(jogo);
            await _context.SaveChangesAsync();

            return RedirectToAction("Loja", "Jogos", new { area = "ADM" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var jogo = await _context.Jogos.FindAsync(id);

            if (jogo == null)
            {
                return NotFound();
            }

            _context.Jogos.Remove(jogo);
            await _context.SaveChangesAsync();

            return RedirectToAction("Loja", "Jogos", new { area = "ADM" });
        }
    }
}