using CacauShowApiSeuRa.Data;
using CacauShowApiSeuRa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CacauShowApiSeuRa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesProducaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LotesProducaoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CriarLote(LoteProducao lote)
        {
            var produtoExiste = await _context.Produtos.AnyAsync(p => p.Id == lote.ProdutoId);
            if (!produtoExiste)
                return NotFound("Produto não encontrado.");

            if (lote.DataFabricacao > DateTime.Now)
                return Conflict("Lote inválido: Data de fabricação não pode ser maior que a data atual.");

            _context.LotesProducao.Add(lote);
            await _context.SaveChangesAsync();

            return Created("", lote);
        }

        public class UpdateStatusDto { public string NovoStatus { get; set; } = string.Empty; }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> AtualizarStatus(int id, [FromBody] UpdateStatusDto dto)
        {
            var lote = await _context.LotesProducao.FindAsync(id);
            if (lote == null) return NotFound("Lote não encontrado.");

            if (lote.Status == "Descartado" && 
               (dto.NovoStatus == "Qualidade Aprovada" || dto.NovoStatus == "Distribuído"))
            {
                return BadRequest("Regra de negócio violada: Um lote descartado não pode ser aprovado ou distribuído.");
            }

            lote.Status = dto.NovoStatus;
            await _context.SaveChangesAsync();

            return Ok(lote);
        }
    }
}