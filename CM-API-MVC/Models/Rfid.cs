using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM_API_MVC.Models
{
    [Table("T_CM_RFID")]
    public class Rfid
    {
        [Key]
        [Column("CD_TAG")]
        [MaxLength(50)]
        public string CodigoTag { get; set; }

        [Column("VL_FREQUENCIA")]
        [Required]
        [MaxLength(20)]
        public string Frequencia { get; set; }

        [Column("ST_STATUS")]
        [Required]
        [MaxLength(1)]
        public string Status { get; set; }

        [Column("DT_ATIVACAO")]
        public DateTime? DataAtivacao { get; set; }

        [Column("DS_OBS")]
        public string? Observacao { get; set; }

        [ForeignKey("CodTag")]
        public Moto? Moto { get; set; }
    }
}
