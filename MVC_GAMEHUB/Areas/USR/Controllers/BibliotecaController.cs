using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_GAMEHUB.Models;

namespace MVC_GAMEHUB.Areas.USR.Controllers
{
    [Area("USR")]
    public class BibliotecaController : Controller
    {
        private readonly Contexto _context;

        public BibliotecaController(Contexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> Biblioteca()
        {
            var usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId") ?? "0");

            var pedidos = await _context.Pedidos
                .Include(p => p.Itens)
                .Where(p => p.UsuarioId == usuarioId)
                .OrderByDescending(p => p.DataPedido)
                .ToListAsync();

            var jogosComprados = pedidos
                .SelectMany(p => p.Itens)
                .GroupBy(i => i.JogoNome)
                .Select(g => g.First())
                .ToList();

            return View(jogosComprados);
        }
    }
}