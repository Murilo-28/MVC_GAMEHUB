using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_GAMEHUB.Models;

namespace MVC_GAMEHUB.Controllers
{
    public class LoginsController : Controller
    {
        private readonly Contexto _context;

        public LoginsController(Contexto context)
        {
            _context = context;
        }
        public IActionResult TelaDeLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TelaDeLogin([Bind("Email,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var usuarioExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == usuario.Email && u.Senha == usuario.Senha);

                if (usuarioExistente == null)
                {
                    ModelState.AddModelError(string.Empty, "Email ou senha inválidos.");
                    return View(usuario);
                }

                HttpContext.Session.SetString("UsuarioNome", usuarioExistente.Nome);
                HttpContext.Session.SetString("UsuarioPerfil", usuarioExistente.Perfil);

                if (usuarioExistente.Perfil == "ADM")
                    return RedirectToAction("Loja", "PgPrincipal", new { area = "ADM" });
                else
                    return RedirectToAction("Loja", "PgPrincipal", new { area = "USR" });
            }
            return View(usuario);
        }

        public IActionResult TelaDeCadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TelaDeCadastro([Bind("Nome,Telefone,Cpf,Perfil,Email,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var usuarioExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == usuario.Email);

                if (usuarioExistente != null)
                {
                    ModelState.AddModelError(string.Empty, "Este email já está em uso.");
                    return View(usuario);
                }

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("TelaDeLogin");
            }
            return View(usuario);
        }
    }
}