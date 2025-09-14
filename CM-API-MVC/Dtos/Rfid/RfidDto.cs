using CM_API_MVC.Dtos.Moto;

namespace CM_API_MVC.Dtos.Rfid
{
    public class RfidDto
    {
        public string CodigoTag { get; set; }
        public string Frequencia { get; set; }
        public string Status { get; set; }
        public DateTime? DataAtivacao { get; set; }
        public string? Observacao { get; set; }
        public MotoDto? MotoDto { get; set; } = null;
    }
}
