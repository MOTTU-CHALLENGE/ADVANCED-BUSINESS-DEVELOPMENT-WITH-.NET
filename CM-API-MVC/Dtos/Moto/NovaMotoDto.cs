namespace CM_API_MVC.Dtos.Moto
{
    public class NovaMotoDto
    {
        public string? CodTag { get; set; }
        public required string TipoMoto { get; set; }
        public required string Placa { get; set; }
        public required string Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public int? AnoFabricacao { get; set; }
        public string? Modelo { get; set; }
    }
}
