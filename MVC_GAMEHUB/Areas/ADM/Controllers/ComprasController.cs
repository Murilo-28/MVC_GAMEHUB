using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_GAMEHUB.Models;

namespace MVC_GAMEHUB.Areas.ADM.Controllers
{
    [Area("ADM")]
    public class ComprasController : Controller
    {
        private readonly Contexto _context;

        public ComprasController(Contexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Itens)
                .OrderByDescending(p => p.DataPedido)
                .ToListAsync();

            return View(pedidos);
        }
    }
}