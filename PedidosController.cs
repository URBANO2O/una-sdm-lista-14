using CacauShowApiSeuRa.Data;
using CacauShowApiSeuRa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CacauShowApiSeuRa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CriarPedido(Pedido pedido)
        {
            var franquia = await _context.Franquias.FindAsync(pedido.UnidadeId);
            var produto = await _context.Produtos.FindAsync(pedido.ProdutoId);

            if (franquia == null || produto == null)
                return NotFound("Franquia ou Produto não encontrado.");

            // 1. Validação de Estoque
            var quantidadeJaPedida = await _context.Pedidos
                .Where(p => p.UnidadeId == pedido.UnidadeId)
                .SumAsync(p => p.Quantidade);

            if ((quantidadeJaPedida + pedido.Quantidade) > franquia.CapacidadeEstoque)
            {
                return BadRequest("Capacidade logística da loja excedida. Não é possível receber mais produtos.");
            }

            // Calculando o valor total base
            pedido.ValorTotal = produto.PrecoBase * pedido.Quantidade;

            // 2. Lógica de Promoção Sazonal
            if (produto.Tipo.Equals("Sazonal", StringComparison.OrdinalIgnoreCase))
            {
                pedido.ValorTotal += 15.00m;
                Console.WriteLine("Produto sazonal detectado: Adicionando embalagem de presente premium!");
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return Created("", pedido);
        }
    }
}