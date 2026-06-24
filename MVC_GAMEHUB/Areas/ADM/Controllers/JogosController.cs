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
        public async Task<IActionResult> Create(Jogo jogo)
        {
            Console.WriteLine($"Nome: {jogo.Nome} | Categoria: {jogo.Categoria} | Preco: {jogo.Preco} | ImagemUrl: {jogo.ImagemUrl}");

            foreach (var erro in ModelState)
            {
                foreach (var e in erro.Value.Errors)
                {
                    Console.WriteLine($"CAMPO: {erro.Key} | ERRO: {e.ErrorMessage} | EXCEPTION: {e.Exception?.Message}");
                }
            }

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