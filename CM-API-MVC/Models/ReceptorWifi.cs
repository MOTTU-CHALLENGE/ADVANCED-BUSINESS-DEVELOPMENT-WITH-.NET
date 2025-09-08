using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM_API_MVC.Models
{
    [Table("T_CM_RECEPTOR_WIFI")]
    public class ReceptorWifi
    {
        [Key]
        [Column("ID_LEITOR")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLeitor { get; set; }

        [Column("ID_PATIO")]
        [Required]
        public int IdPatio { get; set; }

        [Column("DS_LOCAL_INSTALACAO")]
        [Required]
        [MaxLength(100)]
        public string LocalInstalacao { get; set; }

        [Column("DS_ENDERECO_MAC")]
        [Required]
        [MaxLength(17)]
        public string EnderecoMac { get; set; }

        [Column("ST_STATUS")]
        [Required]
        [StringLength(1)]
        public string Status { get; set; }

        [Column("DT_INSTALACAO")]
        [Required]
        public DateTime DataInstalacao { get; set; }

        [Column("DS_OBS")]
        public string? Descricao { get; set; }

        [ForeignKey("IdPatio")]
        public Patio Patio { get; set; }
    }
}
