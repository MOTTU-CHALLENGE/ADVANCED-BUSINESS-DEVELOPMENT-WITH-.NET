using CM_API_MVC.Dtos.Moto;

namespace CM_API_MVC.Dtos.Iot
{
    public class IotDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataInstalacao { get; set; }
        public string? Descricao { get; set; }
        public MotoDto? MotoDto { get; set; }

    }
}
