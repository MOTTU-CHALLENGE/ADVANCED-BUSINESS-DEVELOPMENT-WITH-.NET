namespace CM_API_MVC.Dtos.Posicao
{
    public class PosicaoDto
    {
        public int IdPosicao { get; set; }
        public int IdPatio { get; set; }
        public double CoordenadaX { get; set; }
        public double CoordenadaY { get; set; }
        public DateTime DataHoraRegistro { get; set; }
        public string? Setor { get; set; }
    }
}
