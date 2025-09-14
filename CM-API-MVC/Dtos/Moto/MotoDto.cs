namespace CM_API_MVC.Dtos.Moto
{
    public class MotoDto
    {
        public int IdMoto { get; set; }
        public string? CodTag { get; set; }
        public string TipoMoto { get; set; }
        public string Placa { get; set; }
        public string Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public int? AnoFabricacao { get; set; }
        public string? Modelo { get; set; }
    }
}
