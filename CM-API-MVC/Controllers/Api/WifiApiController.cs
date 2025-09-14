using CM_API_MVC.Dtos.Wifi;
using CM_API_MVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CM_API_MVC.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class WifiApiController : ControllerBase

    {
        private readonly WifiRepository _repository;

        public WifiApiController(WifiRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WifiDto>>> GetAll()
        {
            return Ok(await _repository.GetAllAsyncDto());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<WifiDto>> GetById(int id)
        {
            var wifi = await _repository.GetByIdAsyncDto(id);
            if (wifi == null)
                return NotFound();

            return Ok(wifi);
        }

        [HttpPost]
        public async Task<ActionResult<WifiDto>> Create(NovoWifiDto wifiDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Inserir no banco
            var novoWifi = await _repository.AddAsyncDto(wifiDto);

            // Retornar o DTO com ID e demais dados
            var retorno = new WifiDto
            {
                IdLeitor = novoWifi.IdLeitor,
                IdPatio = wifiDto.IdPatio,
                LocalInstalacao = wifiDto.LocalInstalacao,
                EnderecoMac = wifiDto.EnderecoMac,
                Status = wifiDto.Status,
                DataInstalacao = wifiDto.DataInstalacao,
                Descricao = wifiDto.Descricao,
            };

            return CreatedAtAction(nameof(GetById), new { id = retorno.IdLeitor }, retorno);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, WifiDto wifiDto)
        {
            if (id != wifiDto.IdLeitor)
                return BadRequest();

            var wifiExist = await _repository.GetByIdAsyncDto(id);
            if (wifiExist == null)
                return NotFound();

            await _repository.UpdateAsyncDto(id, wifiDto);
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
