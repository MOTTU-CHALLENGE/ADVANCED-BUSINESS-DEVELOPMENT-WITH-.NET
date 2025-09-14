namespace CM_API_MVC.Dtos.Iot
{
    public class IotComPosicaoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataInstalacao { get; set; }
        public string? Descricao { get; set; }
        public int? IdMoto { get; set; }
        public ICollection<PosicaoDto> Posicoes { get; set; } = new List<PosicaoDto>();
    }
}
