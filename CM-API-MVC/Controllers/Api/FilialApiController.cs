using CM_API_MVC.Dtos;
using CM_API_MVC.Models;
using CM_API_MVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CM_API_MVC.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilialApiController : Controller
    {

        private readonly FilialRepository _repository;

        public FilialApiController(FilialRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilialComPatioDto>>> GetAll()
        {
            return Ok(await _repository.GetAllAsyncDto());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<FilialComPatioDto>> GetById(int id)
        {
            var filial = await _repository.GetByIdAsyncDto(id);
            if (filial == null)
                return NotFound();

            return Ok(filial);
        }

        [HttpPost]
        public async Task<ActionResult<FilialDto>> Create(NovaFilialDto filialDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Inserir no banco
            var novaFilial = await _repository.AddAsyncDto(filialDto);

            // Retornar o DTO com ID e demais dados
            var retorno = new FilialDto
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

            return CreatedAtAction(nameof(GetById), new { id = retorno.IdFilial }, retorno);
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
