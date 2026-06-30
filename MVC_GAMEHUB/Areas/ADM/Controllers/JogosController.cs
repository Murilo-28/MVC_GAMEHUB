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

        public async Task<IActionResult> Editar(int id)
        {
            var jogo = await _context.Jogos.FindAsync(id);
            if (jogo == null) return NotFound();
            return View(jogo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, Jogo jogo)
        {
            if (!ModelState.IsValid) return View(jogo);

            var jogoBanco = await _context.Jogos.FindAsync(id);
            if (jogoBanco == null) return NotFound();

            jogoBanco.Nome = jogo.Nome;
            jogoBanco.Categoria = jogo.Categoria;
            jogoBanco.Preco = jogo.Preco;
            jogoBanco.ImagemUrl = jogo.ImagemUrl;

            await _context.SaveChangesAsync();

            return RedirectToAction("Loja", "Jogos", new { area = "ADM", id = id });
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

        public IActionResult FinalizarCompra()
        {
            var carrinho = HttpContext.Session.GetString("Carrinho");
            var itens = carrinho != null
                ? System.Text.Json.JsonSerializer.Deserialize<List<CarrinhoItem>>(carrinho)
                : new List<CarrinhoItem>();

            if (!itens.Any()) return RedirectToAction("Carrinho");

            HttpContext.Session.Remove("Carrinho");

            return View();
        }
    }
}