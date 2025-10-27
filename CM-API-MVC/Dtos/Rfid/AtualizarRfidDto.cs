namespace CM_API_MVC.Dtos.Rfid
{
    public class AtualizarRfidDto
    {
        public required string CodigoTag { get; set; }
        public required string Status { get; set; }
        public string? Observacao { get; set; }
    }
}
