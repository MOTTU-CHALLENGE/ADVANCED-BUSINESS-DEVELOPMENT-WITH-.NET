using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM_API_MVC.Models
{
    [Table("T_CM_MOTO")]
    public class Moto
    {
        [Key]
        [Column("ID_MOTO")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMoto { get; set; }

        [Column("CD_TAG")]
        [MaxLength(50)]
        public string? CodTag { get; set; }

        [Column("TP_MOTO")]
        [Required]
        [MaxLength(20)]
        public string TipoMoto { get; set; }

        [Column("DS_PLACA")]
        [Required]
        [MaxLength(10)]
        public string Placa { get; set; }

        [Column("ST_STATUS")]
        [Required]
        [MaxLength(1)]
        public string Status { get; set; }

        [Column("DT_CADASTRO")]
        [Required]
        public DateTime DataCadastro { get; set; }

        [Column("NR_ANO_FABRICACAO")]
        public int? AnoFabricacao { get; set; }

        [Column("DS_MODELO")]
        [MaxLength(50)]
        public string? Modelo { get; set; }

        [ForeignKey("CodTag")]
        public Rfid? Rfid { get; set; }

    }
}
