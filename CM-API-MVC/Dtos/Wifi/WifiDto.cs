namespace CM_API_MVC.Dtos.Wifi
{
    public class WifiDto
    {
        public int IdLeitor { get; set; }
        public int IdPatio { get; set; }
        public string LocalInstalacao { get; set; }
        public string EnderecoMac { get; set; }
        public string Status { get; set; }
        public DateTime DataInstalacao { get; set; }
        public string? Descricao { get; set; }
    }
}
