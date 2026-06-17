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
        public async Task<IActionResult> TelaDeLogin([Bind("Nome,Telefone,Email,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var usuarioExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == usuario.Email && u.Senha == usuario.Senha);
                if (usuarioExistente != null)
                {
                    // Login bem-sucedido, redirecionar para a página desejada
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Credenciais inválidas, exibir mensagem de erro
                    ModelState.AddModelError(string.Empty, "Credenciais inválidas.");
                }
            }
            return View(usuario);
        }

        public IActionResult TelaDeCadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TelaDeCadastro([Bind("Email,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // Verificar se o email já está em uso
                var usuarioExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == usuario.Email);
                if (usuarioExistente != null)
                {
                    ModelState.AddModelError(string.Empty, "Este email já está em uso.");
                    return View(usuario);
                }
                // Adicionar o novo usuário ao banco de dados
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                // Redirecionar para a página de login após o cadastro bem-sucedido
                return RedirectToAction("TelaDeLogin");
            }
            return View(usuario);
        }
    }
}