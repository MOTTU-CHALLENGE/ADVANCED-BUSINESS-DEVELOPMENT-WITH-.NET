using CM_API_MVC.Dtos.Iot;
using CM_API_MVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CM_API_MVC.Controllers.Api.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class IotApiController : ControllerBase
    {
        private readonly IotRepository _repository;

        public IotApiController(IotRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<IotDto>>> GetAll()
        {
            return Ok(await _repository.GetAllAsyncDto());
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<IotComPosicaoDto>> GetById(int id)
        {
            var filial = await _repository.GetByIdAsyncDto(id);
            if (filial == null)
                return NotFound();

            return Ok(filial);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<IotCadastradoDto>> Create(NovoIotDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Inserir no banco
            var novoIot = await _repository.AddAsyncDto(dto);

            // Retornar o DTO com ID e demais dados
            var retorno = new IotCadastradoDto
            {
                Id = novoIot.Id,
                Nome = novoIot.Nome,
                DataInstalacao = novoIot.DataInstalacao,
                Descricao = novoIot.Descricao,
                IdMoto = novoIot.IdMoto,
            };

            return CreatedAtAction(nameof(GetById), new { id = retorno.Id }, retorno);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var filial = await _repository.GetByIdAsync(id);
            if (filial == null)
                return NotFound();

            await _repository.DeleteAsync(filial);
            return NoContent();
        }
    }
}
