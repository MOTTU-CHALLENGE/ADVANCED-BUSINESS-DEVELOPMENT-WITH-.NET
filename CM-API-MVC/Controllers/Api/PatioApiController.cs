using CM_API_MVC.Models;
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
        public async Task<ActionResult<IEnumerable<Patio>>> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patio>> GetById(int id)
        {
            var patio = await _repository.GetByIdAsync(id);
            if (patio == null)
                return NotFound();

            return Ok(patio);
        }

        [HttpPost]
        public async Task<ActionResult<Patio>> Create(Patio patio)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.AddAsync(patio);

            return CreatedAtAction(nameof(GetById), new { id = patio.IdPatio }, patio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Patio patio)
        {
            if (id != patio.IdPatio)
                return BadRequest();

            var patioExist = await _repository.GetByIdAsync(id);
            if (patioExist == null)
                return NotFound();

            await _repository.UpdateAsync(patio);
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
