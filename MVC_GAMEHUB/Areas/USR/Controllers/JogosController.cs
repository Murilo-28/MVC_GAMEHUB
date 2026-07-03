using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_GAMEHUB.Models;

namespace MVC_GAMEHUB.Areas.USR.Controllers
{
    [Area("USR")]
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

        public async Task<IActionResult> Detalhes(int id)
        {
            var jogo = await _context.Jogos.FindAsync(id);
            if (jogo == null) return NotFound();
            return View(jogo);
        }

        public IActionResult AdicionarCarrinho(int id)
        {
            var jogo = _context.Jogos.Find(id);
            if (jogo == null) return NotFound();

            var carrinho = HttpContext.Session.GetString("Carrinho");
            var itens = carrinho != null
                ? System.Text.Json.JsonSerializer.Deserialize<List<CarrinhoItem>>(carrinho)
                : new List<CarrinhoItem>();

            if (!itens.Any(i => i.JogoId == id))
            {
                itens.Add(new CarrinhoItem
                {
                    JogoId = jogo.Id,
                    Nome = jogo.Nome,
                    ImagemUrl = jogo.ImagemUrl,
                    Preco = jogo.Preco
                });
            }

            HttpContext.Session.SetString("Carrinho",
                System.Text.Json.JsonSerializer.Serialize(itens));

            return RedirectToAction("Carrinho");
        }

        public IActionResult Carrinho()
        {
            var carrinho = HttpContext.Session.GetString("Carrinho");
            var itens = carrinho != null
                ? System.Text.Json.JsonSerializer.Deserialize<List<CarrinhoItem>>(carrinho)
                : new List<CarrinhoItem>();

            return View(itens);
        }

        public IActionResult RemoverCarrinho(int id)
        {
            var carrinho = HttpContext.Session.GetString("Carrinho");
            var itens = carrinho != null
                ? System.Text.Json.JsonSerializer.Deserialize<List<CarrinhoItem>>(carrinho)
                : new List<CarrinhoItem>();

            itens.RemoveAll(i => i.JogoId == id);

            HttpContext.Session.SetString("Carrinho",
                System.Text.Json.JsonSerializer.Serialize(itens));

            return RedirectToAction("Carrinho");
        }

        [HttpPost]
        public async Task<IActionResult> FinalizarCompra(string formaPagamento)
        {
            var carrinho = HttpContext.Session.GetString("Carrinho");
            var itens = carrinho != null
                ? System.Text.Json.JsonSerializer.Deserialize<List<CarrinhoItem>>(carrinho)
                : new List<CarrinhoItem>();

            if (!itens.Any()) return RedirectToAction("Carrinho");

            var usuarioNome = HttpContext.Session.GetString("UsuarioNome") ?? "Anônimo";
            var usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId") ?? "0");

            var pedido = new Pedido
            {
                UsuarioId = usuarioId,
                UsuarioNome = usuarioNome,
                FormaPagamento = formaPagamento,
                Total = itens.Sum(i => i.Preco),
                DataPedido = DateTime.Now,
                Itens = itens.Select(i => new PedidoItem
                {
                    JogoNome = i.Nome,
                    JogoImagemUrl = i.ImagemUrl,
                    Preco = i.Preco
                }).ToList()
            };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            HttpContext.Session.Remove("Carrinho");
            return View(pedido);
        }
    }
}