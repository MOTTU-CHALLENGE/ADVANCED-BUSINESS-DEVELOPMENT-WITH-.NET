namespace CM_API_MVC.Dtos.Registro
{
    public class RegistroDto
    {
        public int IdIot { get; set; }

        public required string Bssid { get; set; }

        public double Rssi { get; set; }
    }
}
