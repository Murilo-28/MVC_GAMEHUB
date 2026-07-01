using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_GAMEHUB.Models
{
    [Table("PedidoItens")]
    public class PedidoItem
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("PedidoId")]
        public int PedidoId { get; set; }

        [Column("JogoNome")]
        public string JogoNome { get; set; }

        [Column("JogoImagemUrl")]
        public string? JogoImagemUrl { get; set; }

        [Column("Preco")]
        public decimal Preco { get; set; }

        public Pedido Pedido { get; set; }
    }
}