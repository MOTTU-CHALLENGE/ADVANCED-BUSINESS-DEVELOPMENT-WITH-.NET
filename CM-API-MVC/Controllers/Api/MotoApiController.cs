using CM_API_MVC.Dtos.Moto;
using CM_API_MVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CM_API_MVC.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotoApiController : ControllerBase
    {
        private readonly MotoRepository _repository;

        public MotoApiController(MotoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotoDto>>> GetAll()
        {
            return Ok(await _repository.GetAllAsyncDto());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<MotoDto>> GetById(int id)
        {
            var wifi = await _repository.GetByIdAsyncDto(id);
            if (wifi == null)
                return NotFound();

            return Ok(wifi);
        }

        [HttpPost]
        public async Task<ActionResult<MotoDto>> Create(NovaMotoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Inserir no banco
            var novaMoto = await _repository.AddAsyncDto(dto);

            // Retornar o DTO com ID e demais dados
            var retorno = new MotoDto
            {

                IdMoto = novaMoto.IdMoto,
                CodTag = novaMoto.CodTag,
                TipoMoto = novaMoto.TipoMoto,
                Placa = novaMoto.Placa,
                Status = novaMoto.Status,
                DataCadastro = novaMoto.DataCadastro,
                AnoFabricacao = novaMoto.AnoFabricacao,
                Modelo = novaMoto.Modelo,
            };

            return CreatedAtAction(nameof(GetById), new { id = retorno.IdMoto }, retorno);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MotoDto dto)
        {
            if (id != dto.IdMoto)
                return BadRequest();

            var wifiExist = await _repository.GetByIdAsyncDto(id);
            if (wifiExist == null)
                return NotFound();

            await _repository.UpdateAsyncDto(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var wifi = await _repository.GetByIdAsync(id);
            if (wifi == null)
                return NotFound();

            await _repository.DeleteAsync(wifi);
            return NoContent();
        }


    }
}
