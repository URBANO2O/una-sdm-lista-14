using CacauShowApiSeuRa.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace CacauShowApiSeuRa.Controllers
{
    [ApiController]
    [Route("api/intelligence")]
    public class ChocolateIntelligenceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChocolateIntelligenceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("estoque-regional")]
        public async Task<IActionResult> GetEstoqueRegional()
        {
            // Simulação de Latência exigida na atividade
            Thread.Sleep(2000); 

            var estoquePorCidade = await _context.Pedidos
                .Include(p => p.Franquia)
                .GroupBy(p => p.Franquia!.Cidade)
                .Select(g => new
                {
                    Cidade = g.Key,
                    TotalVendidos = g.Sum(p => p.Quantidade)
                })
                .ToListAsync();

            return Ok(estoquePorCidade);
        }
    }
}