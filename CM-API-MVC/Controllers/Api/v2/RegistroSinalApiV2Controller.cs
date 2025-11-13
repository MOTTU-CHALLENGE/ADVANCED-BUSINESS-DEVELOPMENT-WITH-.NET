using CM_API_MVC.Dtos.Registro;
using CM_API_MVC.Models;
using CM_API_MVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CM_API_MVC.Controllers.Api.v1
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RegistroSinalApiV2Controller : ControllerBase
    {
        private readonly RegistroSinalRepository _repository;

        public RegistroSinalApiV2Controller(RegistroSinalRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<RegistroIntensidadeSinal>>> GetAll() =>
            await _repository.GetAllAsync();

        [HttpGet("paginado")]
        public async Task<ActionResult<List<RegistroIntensidadeSinal>>> GetHalf([FromQuery] int pagina = 1, [FromQuery] int qtd = 10)
        {

            var limite = Math.Min(qtd, 100);
            var registros = await _repository.GetHalfAsync(pagina, limite);
            var total = await _repository.CountAsync();

            Response.Headers.Append("X-Total-Count", total.ToString());
            return registros;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegistroIntensidadeSinal>> GetById(string id)
        {
            var registro = await _repository.GetByIdAsync(id);
            if (registro == null)
                return NotFound();
            return Ok(registro);
        }


        [HttpPost]
        public async Task<ActionResult<RegistroIntensidadeSinal>> Create([FromBody] RegistroDto registro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoRegistro = await _repository.AddAsync(registro);
            return Ok(novoRegistro);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var registroExist = await _repository.GetByIdAsync(id);
            if (registroExist == null)
                return NotFound();

            var removido = await _repository.DeleteAsync(id);
            return removido ? NoContent() : NotFound();
        }
    }
}
