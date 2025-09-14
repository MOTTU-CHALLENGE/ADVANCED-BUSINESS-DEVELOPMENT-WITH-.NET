namespace CM_API_MVC.Dtos.Filial
{
    public class FilialDto
    {
        public int IdFilial { get; set; }
        public string NomeFilial { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string? Cep { get; set; }
        public string? Telefone { get; set; }
        public DateTime? DataInauguracao { get; set; }
    }
}
