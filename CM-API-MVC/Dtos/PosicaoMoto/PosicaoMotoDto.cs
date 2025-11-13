namespace CM_API_MVC.Dtos.PosicaoMoto
{
    public class PosicaoMotoDto
    {
        public int IdDispositivo { get; set; }

        public int IdPatio { get; set; }

        public double CoordenadaX { get; set; }

        public double CoordenadaY { get; set; }

        public DateTime DataHoraRegistro { get; set; }
    }
}
