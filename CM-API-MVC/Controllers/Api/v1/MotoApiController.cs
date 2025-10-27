using CM_API_MVC.Dtos.Moto;
using CM_API_MVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CM_API_MVC.Controllers.Api.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MotoApiController : ControllerBase
    {
        private readonly MotoRepository _repository;
        private readonly MotoLinksHelper _linksHelper;

        public MotoApiController(MotoRepository repository, MotoLinksHelper linksHelper)
        {
            _repository = repository;
            _linksHelper = linksHelper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MotoDto>>> GetAll()
        {
            return Ok(await _repository.GetAllAsyncDto());
        }

        [HttpGet("paginado")]
        [Authorize]
        public async Task<ActionResult<List<MotoDto>>> GetHalf([FromQuery] int pagina = 1, [FromQuery] int qtd = 10)
        {
            var limite = Math.Min(qtd, 100);
            var registros = await _repository.GetHalfAsync(pagina, limite);
            var total = await _repository.CountAsync();

            Response.Headers.Append("X-Total-Count", total.ToString());
            return registros;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<MotoHatDto>> GetById(int id)
        {
            var moto = await _repository.GetByIdAsyncDto(id);
            if (moto == null)
                return NotFound();

            var motoComLinks = _linksHelper.AddLinks(moto, HttpContext);

            return Ok(motoComLinks);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<MotoHatDto>> Create(NovaMotoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Inserir no banco
            var novaMoto = await _repository.AddAsyncDto(dto);

            // Retornar o DTO com ID e demais dados
            var moto = new MotoDto
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

            var motoComLinks = _linksHelper.AddLinks(moto, HttpContext);

            return CreatedAtAction(nameof(GetById), new { id = motoComLinks.IdMoto }, motoComLinks);
        }


        [HttpPut("{id}")]
        [Authorize]
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
        [Authorize]
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
