using CM_API_MVC.Dtos.Filial;
using CM_API_MVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CM_API_MVC.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilialApiController : Controller
    {

        private readonly FilialRepository _repository;

        private readonly FilialLinksHelper _linksHelper;

        public FilialApiController(FilialRepository repository, FilialLinksHelper linksHelper)
        {
            _repository = repository;
            _linksHelper = linksHelper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilialComPatioDto>>> GetAll()
        {
            return Ok(await _repository.GetAllAsyncDto());
        }


        [HttpGet("paginado")]
        public async Task<ActionResult<List<FilialComPatioDto>>> GetHalf([FromQuery] int pagina = 1, [FromQuery] int qtd = 10)
        {
            var limite = Math.Min(qtd, 100);
            var registros = await _repository.GetHalfAsync(pagina, limite);
            var total = await _repository.CountAsync();

            Response.Headers.Append("X-Total-Count", total.ToString());
            return registros;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<FilialComPatioHatDto>> GetById(int id)
        {
            var filial = await _repository.GetByIdAsyncDto(id);
            if (filial == null)
                return NotFound();

            var filialComLinks = _linksHelper.AddLinks(filial, HttpContext);

            return Ok(filialComLinks);
        }

        [HttpPost]
        public async Task<ActionResult<FilialComPatioHatDto>> Create(NovaFilialDto filialDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Inserir no banco
            var novaFilial = await _repository.AddAsyncDto(filialDto);

            // Retornar o DTO com ID e demais dados
            var retorno = new FilialComPatioDto
            {
                IdFilial = novaFilial.IdFilial,
                NomeFilial = novaFilial.NomeFilial,
                Endereco = novaFilial.Endereco,
                Cidade = novaFilial.Cidade,
                Estado = novaFilial.Estado,
                Pais = novaFilial.Pais,
                Cep = novaFilial.Cep,
                Telefone = novaFilial.Telefone,
                DataInauguracao = novaFilial.DataInauguracao
            };

            var filialComLinks = _linksHelper.AddLinks(retorno, HttpContext);

            return CreatedAtAction(nameof(GetById), new { id = filialComLinks.IdFilial }, filialComLinks);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FilialDto filial)
        {
            if (id != filial.IdFilial)
                return BadRequest();

            var filialExist = await _repository.GetByIdAsyncDto(id);
            if (filialExist == null)
                return NotFound();

            await _repository.UpdateAsyncDto(id, filial);
            return NoContent();
        }

        [HttpDelete("{id}")]
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
