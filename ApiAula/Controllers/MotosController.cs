using ApiAula.Data;
using ApiAula.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAula.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotosController : Controller
    {
        private readonly AppDbContext _context;

        public MotosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/motos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moto>>> GetMoto()
        {
            return await _context.Motos.ToListAsync(); 
        }
        // GET: api/motos/filtrar?marca=Honda
        [HttpGet("filtrar")]
        public IActionResult GetPorMarca([FromQuery] string marca)
        {
            if (string.IsNullOrWhiteSpace(marca))
                return BadRequest("Marca não informada.");

            var motos = _context.Motos.Where(m => m.Modelo == marca).ToList();
            if (motos.Count == 0)
                return NotFound($"Nenhuma moto encontrada para a marca: {marca}");

            return Ok(motos);
        }

        // GET: api/motos/andar/2
        [HttpGet("andar/{ano}")]
        public IActionResult GetPorAndar([FromRoute] int andar)
        {
            var motos = _context.Motos.Where(m => m.Andar == andar).ToList();
            if (motos.Count == 0)
                return NotFound($"Nenhuma moto encontrada para o ano: {andar}");

            return Ok(motos);
        }


        // GET: api/motos/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Moto>> GetMoto(int id)
        {
            var produto = await _context.Motos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return produto;
        }

        // PUT: api/motos/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoto(int id, Moto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }
            _context.Entry(produto).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        private bool MotoExists(int id)
        {
            return _context.Motos.Any(e => e.Id == id);
        }


        //POST: api/motos
        [HttpPost]
        public async Task<ActionResult<Moto>> PostProduto(Moto moto) {
            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMoto", new { id = moto.Id }, moto);
        
        }

        //DELETE: api/motos/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Moto>> DeleteProduto(int id) {
            var produto = await _context.Motos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }


            _context.Motos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
       
        }

      


    }
}
