using Microsoft.ML.Data;

namespace CM_API_MVC.Models
{
    public class DadosTreinoPosicao
    {
        [LoadColumn(1)] 
        public float IdDispositivo { get; set; }

        [LoadColumn(6)] 
        public string Bssid { get; set; } = string.Empty;

        [LoadColumn(7)] 
        public float Rssi { get; set; }

        [LoadColumn(2)] 
        public float CoordenadaX { get; set; }
    }
}
