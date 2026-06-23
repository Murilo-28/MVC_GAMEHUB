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

        public async Task<IActionResult> Index()
        {
            var jogos = await _context.Jogos.ToListAsync();
            return View(jogos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Categoria,Preco,ImagemUrl,Descricao")] Jogo jogo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jogo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Loja", "Home", new { area = "ADM" });
            }
            return View(jogo);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var jogo = await _context.Jogos.FindAsync(id);
            if (jogo == null) return NotFound();
            _context.Jogos.Remove(jogo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Loja", "Home", new { area = "ADM" });
        }
    }
}