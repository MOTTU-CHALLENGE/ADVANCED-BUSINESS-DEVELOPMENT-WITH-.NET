using CM_API_MVC.Dtos.Moto;

namespace CM_API_MVC.Dtos.Rfid
{
    public class RfidDto
    {
        public required string CodigoTag { get; set; }
        public required string Frequencia { get; set; }
        public required string Status { get; set; }
        public DateTime? DataAtivacao { get; set; }
        public string? Observacao { get; set; }
        public MotoDto? MotoDto { get; set; } = null;
    }
}
