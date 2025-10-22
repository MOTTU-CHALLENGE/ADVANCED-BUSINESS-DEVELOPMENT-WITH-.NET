namespace CM_API_MVC.Dtos.Wifi
{
    public class NovoWifiDto
    {
        public int IdPatio { get; set; }
        public required string LocalInstalacao { get; set; }
        public required string EnderecoMac { get; set; }
        public required string Status { get; set; }
        public DateTime DataInstalacao { get; set; }
        public string? Descricao { get; set; }
    }
}
