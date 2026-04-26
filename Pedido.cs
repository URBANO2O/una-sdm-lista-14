using CacauShowApi324133695.Models;

namespace CacauShowApiSeuRa.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int UnidadeId { get; set; }
        public Franquia? Franquia { get; set; } // FK para Franquia (Unidade)
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
    }
}