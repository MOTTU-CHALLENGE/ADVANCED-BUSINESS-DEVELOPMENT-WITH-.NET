using CM_API_MVC.Dtos.Patio;

namespace CM_API_MVC.Dtos.Filial
{
    public class FilialComPatioDto
    {
        public int IdFilial { get; set; }
        public required string NomeFilial { get; set; }
        public required string Endereco { get; set; }
        public required string Cidade { get; set; }
        public required string Estado { get; set; }
        public required string Pais { get; set; }
        public string? Cep { get; set; }
        public string? Telefone { get; set; }
        public DateTime? DataInauguracao { get; set; }
        public ICollection<NomePatioDto> Patios { get; set; } = new List<NomePatioDto>();
    }
}
