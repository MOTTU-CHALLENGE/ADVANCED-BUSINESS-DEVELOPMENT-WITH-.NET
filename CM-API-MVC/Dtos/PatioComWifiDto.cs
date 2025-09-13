using CM_API_MVC.Models;

namespace CM_API_MVC.Dtos
{
    public class PatioComWifiDto
    {
        public int IdPatio { get; set; }
        public int IdFilial { get; set; }
        public required string NomePatio { get; set; }
        public int? CapacidadeMax { get; set; }
        public decimal? Area { get; set; }
        public string? Descricao { get; set; }
        public ICollection<WifiDto> ReceptorWifi { get; set; } = new List<WifiDto>();
    }
}
