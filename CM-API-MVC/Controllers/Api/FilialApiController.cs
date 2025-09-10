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
        public async Task<ActionResult<IEnumerable<Filial>>> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Filial>> GetById(int id)
        {
            var filial = await _repository.GetByIdAsync(id);
            if (filial == null)
                return NotFound();

            return Ok(filial);
        }

        [HttpPost]
        public async Task<ActionResult<Filial>> Create(Filial filial)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.AddAsync(filial);
            return CreatedAtAction(nameof(GetById), new { id = filial.IdFilial }, filial);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Filial filial)
        {
            if (id != filial.IdFilial)
                return BadRequest();

            var filialExist = await _repository.GetByIdAsync(id);
            if (filialExist == null)
                return NotFound();

            await _repository.UpdateAsync(filial);
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
