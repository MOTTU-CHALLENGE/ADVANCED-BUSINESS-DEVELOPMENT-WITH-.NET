using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM_API_MVC.Models
{
    [Table("T_CM_POSICAO_MOTO")]
    public class PosicaoMoto
    {
        [Key]
        [Column("ID_POSICAO")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPosicao { get; set; }

        [Column("ID_DISPOSITIVO")]
        [Required]
        public int IdDispositivo { get; set; }

        [Column("ID_PATIO")]
        [Required]
        public int IdPatio { get; set; }

        [Column("COORD_X")]
        [Required]
        public double CoordenadaX { get; set; }

        [Column("COORD_Y")]
        [Required]
        public double CoordenadaY { get; set; }

        [Column("DT_REGISTRO")]
        [Required]
        public DateTime DataHoraRegistro { get; set; }

        [Column("DS_SETOR")]
        [MaxLength(50)]
        public string? Setor { get; set; }

        [ForeignKey("IdDispositivo")]
        public required DispositivoIot DispositivoIot { get; set; }

        [ForeignKey("IdPatio")]
        public required Patio Patio { get; set; }
    }
}
