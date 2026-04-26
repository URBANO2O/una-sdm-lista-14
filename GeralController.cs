using CacauShowApiSeuRa.Data;
using CacauShowApiSeuRa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CacauShowApiSeuRa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeralController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GeralController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("produto")]
        public async Task<IActionResult> CriarProduto(CacauShowApi324133695.Models.Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return Ok(produto);
        }

        [HttpPost("franquia")]
        public async Task<IActionResult> CriarFranquia(CacauShowApi324133695.Models.Franquia franquia)
        {
            _context.Franquias.Add(franquia);
            await _context.SaveChangesAsync();
            return Ok(franquia);
        }
    }
}