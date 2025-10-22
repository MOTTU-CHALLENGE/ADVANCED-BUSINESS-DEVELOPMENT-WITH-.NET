namespace CM_API_MVC.Dtos.Iot
{
    public class NovoIotDto
    {
        public required string Nome { get; set; }
        public DateTime DataInstalacao { get; set; }
        public string? Descricao { get; set; }
        public int IdMoto { get; set; }
    }
}
