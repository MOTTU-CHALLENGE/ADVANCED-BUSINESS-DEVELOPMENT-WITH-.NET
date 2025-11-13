using CM_API_MVC.Dtos.PosicaoMoto;
using CM_API_MVC.Models;
using CM_API_MVC.Repositories;
using CM_API_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CM_API_MVC.Controllers.Api.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PosicaoApiV2Controller : ControllerBase
    {
        private readonly RegistroSinalRepository _registroRepo;
        private readonly WifiRepository _wifiRepo;
        private readonly PreditorPosicaoService _mlService;

        public PosicaoApiV2Controller(RegistroSinalRepository registroRepo, PreditorPosicaoService mlService, WifiRepository wifiRepo)
        {
            _registroRepo = registroRepo;
            _mlService = mlService;
            _wifiRepo = wifiRepo;
        }

        [HttpPost("treinar")]
        public IActionResult TreinarModelo()
        {
            try
            {
                var caminhoModelo = "Data/modelo-posicao.zip";

                if (System.IO.File.Exists(caminhoModelo))
                {
                    return Ok("Modelo já treinado. Nenhuma ação foi necessária.");
                }

                _mlService.TreinarComCsv();
                return Ok("Modelo treinado e salvo com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao treinar o modelo: {ex.Message}");
            }
        }

        [HttpPost("prever")]
        public async Task<IActionResult> PreverPosicao([FromBody] int IdDispositivo)
        {
            var sinal = await _registroRepo.ObterUltimoPorDispositivo(IdDispositivo);
            if (sinal == null) return NotFound("Nenhum registro de sinal encontrado.");

            var entrada = new EntradaPosicao
            {
                IdDispositivo = sinal.IdIot,
                Bssid = sinal.Bssid,
                Rssi = sinal.Rssi,
            };

            var wifi = await _wifiRepo.GetByMacAsyncDto(sinal.Bssid);
            if (wifi == null) return NotFound("Nenhum wifi registrado.");

            var predicao = _mlService.Prever(entrada);

            var posicao = new PosicaoMotoDto
            {
                IdDispositivo = sinal.IdIot,
                IdPatio = wifi.IdPatio,
                CoordenadaX = predicao.CoordenadaX,
                CoordenadaY = predicao.CoordenadaY,
                DataHoraRegistro = sinal.DateTime,
            };

            return Ok(posicao);
        }
    }

}
