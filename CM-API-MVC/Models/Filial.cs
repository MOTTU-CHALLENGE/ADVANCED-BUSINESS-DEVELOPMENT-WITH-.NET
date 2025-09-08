using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM_API_MVC.Models
{
    [Table("T_CM_FILIAL")]
    public class Filial
    {
        [Key]
        [Column("ID_FILIAL")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFilial { get; set; }

        [Column("NM_FILIAL")]
        [Required]
        [MaxLength(100)]
        public string NomeFilial { get; set; }

        [Column("DS_ENDERECO")]
        [Required]
        [MaxLength(255)]
        public string Endereco { get; set; }

        [Column("DS_CIDADE")]
        [Required]
        [MaxLength(100)]
        public string Cidade { get; set; }

        [Column("DS_ESTADO")]
        [Required]
        [MaxLength(50)]
        public string Estado { get; set; }

        [Column("DS_PAIS")]
        [Required]
        [MaxLength(50)]
        public string Pais { get; set; }

        [Column("CD_CEP")]
        [MaxLength(20)]
        public string? Cep { get; set; }

        [Column("DS_TELEFONE")]
        [MaxLength(20)]
        public string? Telefone { get; set; }

        [Column("DT_INAUGURACAO")]
        [DataType(DataType.Date)]
        public DateTime? DataInauguracao { get; set; }

        public ICollection<Patio> Patios { get; set; } = new List<Patio>();
    }
}
