using CM_API_MVC.Dtos.Patio;
using CM_API_MVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CM_API_MVC.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatioApiController : ControllerBase
    {
        private readonly PatioRepository _repository;

        public PatioApiController(PatioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatioComWifiDto>>> GetAll()
        {
            return Ok(await _repository.GetAllAsyncDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatioComWifiDto>> GetById(int id)
        {
            var patio = await _repository.GetByIdAsyncDto(id);
            if (patio == null)
                return NotFound();

            return Ok(patio);
        }

        [HttpPost]
        public async Task<ActionResult<PatioDto>> Create(NovoPatioDto patioDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patio = await _repository.AddAsyncDto(patioDto);

            var retorno = new PatioDto
            {
                IdPatio = patio.IdPatio,
                IdFilial = patio.IdFilial,
                NomePatio = patio.NomePatio,
                CapacidadeMax = patio.CapacidadeMax,
                Area = patio.Area,
                Descricao = patio.Descricao
            };

            return CreatedAtAction(nameof(GetById), new { id = retorno.IdPatio }, patioDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PatioDto patio)
        {
            if (id != patio.IdPatio)
                return BadRequest();

            var patioExist = await _repository.GetByIdAsync(id);
            if (patioExist == null)
                return NotFound();

            await _repository.UpdateAsyncDto(id, patio);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var patio = await _repository.GetByIdAsync(id);
            if (patio == null)
                return NotFound();

            await _repository.DeleteAsync(patio);
            return NoContent();
        }
    }
}
