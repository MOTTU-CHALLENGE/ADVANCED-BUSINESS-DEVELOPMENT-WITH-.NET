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
        private readonly PatioLinksHelper _linksHelper;

        public PatioApiController(PatioRepository repository, PatioLinksHelper linksHelper)
        {
            _repository = repository;
            _linksHelper = linksHelper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatioComWifiDto>>> GetAll()
        {
            return Ok(await _repository.GetAllAsyncDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatioComWifiHatDto>> GetById(int id)
        {
            var patio = await _repository.GetByIdAsyncDto(id);
            if (patio == null)
                return NotFound();

            var patioComLinks = _linksHelper.AddLinks(patio, HttpContext);


            return Ok(patioComLinks);
        }

        [HttpPost]
        public async Task<ActionResult<PatioComWifiHatDto>> Create(NovoPatioDto patioDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patio = await _repository.AddAsyncDto(patioDto);

            var retorno = new PatioComWifiDto
            {
                IdPatio = patio.IdPatio,
                IdFilial = patio.IdFilial,
                NomePatio = patio.NomePatio,
                CapacidadeMax = patio.CapacidadeMax,
                Area = patio.Area,
                Descricao = patio.Descricao
            };

            var patioComLinks = _linksHelper.AddLinks(retorno, HttpContext);

            return CreatedAtAction(nameof(GetById), new { id = patioComLinks.IdPatio }, patioComLinks);
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
