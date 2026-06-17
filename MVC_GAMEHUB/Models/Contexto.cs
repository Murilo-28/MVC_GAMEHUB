using Microsoft.EntityFrameworkCore;

namespace MVC_GAMEHUB.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Jogo> Jogos { get; set; }
    }
}
