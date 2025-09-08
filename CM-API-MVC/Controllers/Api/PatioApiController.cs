using CM_API_MVC.Contexts;
using CM_API_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CM_API_MVC.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatioApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatioApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patio>>> GetAll()
        {
            return Ok(await _context.Patios.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patio>> GetById(int id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();

            return Ok(patio);
        }

        private async Task<int> GetNextPatioIdAsync()
        {
            var connectionString = _context.Database.GetDbConnection().ConnectionString;

            using var connection = new OracleConnection(connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT PATIO_SEQ.NEXTVAL FROM DUAL";

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        [HttpPost]
        public async Task<ActionResult<Patio>> Create(Patio patio)
        {
            patio.IdPatio = await GetNextPatioIdAsync();
            await _context.Patios.AddAsync(patio);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = patio.IdPatio }, patio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Patio patio)
        {
            if (id != patio.IdPatio)
                return BadRequest();

            _context.Entry(patio).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();

            _context.Patios.Remove(patio);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
