namespace CM_API_MVC.Dtos
{
    public class NovoPatioDto
    {
        public int IdFilial { get; set; }
        public required string NomePatio { get; set; }
        public int? CapacidadeMax { get; set; }
        public decimal? Area { get; set; }
        public string? Descricao { get; set; }
    }
}
