namespace CM_API_MVC.Models
{
    public class EntradaPosicao
    {
        public float IdDispositivo { get; set; }
        public required string Bssid { get; set; }
        public double Rssi { get; set; }
    }
}
