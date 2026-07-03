using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_GAMEHUB.Models
{
    [Table("Pedidos")]
    public class Pedido
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("UsuarioNome")]
        public string UsuarioNome { get; set; }

        [Column("FormaPagamento")]
        public string FormaPagamento { get; set; }

        [Column("Total")]
        public decimal Total { get; set; }

        [Column("UsuarioId")]
        public int UsuarioId { get; set; }

        [Column("DataPedido")]
        public DateTime DataPedido { get; set; } = DateTime.Now;

        public List<PedidoItem> Itens { get; set; } = new();
    }
}