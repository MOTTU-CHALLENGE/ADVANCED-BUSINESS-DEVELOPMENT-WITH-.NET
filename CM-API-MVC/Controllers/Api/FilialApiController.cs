using CM_API_MVC.Contexts;
using CM_API_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CM_API_MVC.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilialApiController : Controller
    {

        private readonly AppDbContext _context;

        public FilialApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filial>>> GetAll()
        {
            return Ok(await _context.Filiais.ToListAsync());
        }

        private async Task<int> GetNextFilialIdAsync()
        {
            var connectionString = _context.Database.GetDbConnection().ConnectionString;

            using var connection = new OracleConnection(connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT FILIAL_SEQ.NEXTVAL FROM DUAL";

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Filial>> GetById(int id)
        {
            var filial = await _context.Filiais.FindAsync(id);
            if (filial == null)
                return NotFound();

            return Ok(filial);
        }

        [HttpPost]
        public async Task<ActionResult<Filial>> Create(Filial filial)
        {

            filial.IdFilial = await GetNextFilialIdAsync();

            await _context.Filiais.AddAsync(filial);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = filial.IdFilial }, filial);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Filial filial)
        {
            if (id != filial.IdFilial)
                return BadRequest();

            _context.Entry(filial).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var filial = await _context.Filiais.FindAsync(id);
            if (filial == null)
                return NotFound();

            _context.Filiais.Remove(filial);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
