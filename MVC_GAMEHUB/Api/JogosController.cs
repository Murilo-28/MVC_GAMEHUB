
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_GAMEHUB.Models;

namespace MVC_GAMEHUB.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogosController : ControllerBase
    {
        private readonly Contexto _contexto;

        public JogosController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> GetJogos()
        {
            var jogos = await _contexto.Jogos
                .AsNoTracking()
                .Select(jogo => new
                {
                    jogo.Id,
                    jogo.Nome,
                    jogo.Categoria,
                    jogo.ImagemUrl,
                    jogo.Preco
                })
                .ToListAsync();

            return Ok(jogos);
        }
    }
}
