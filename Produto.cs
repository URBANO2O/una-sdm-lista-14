namespace CacauShowApi324133695.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty; // Gourmet, Linha Regular, Sazonal
        public decimal PrecoBase { get; set; }
    }
}