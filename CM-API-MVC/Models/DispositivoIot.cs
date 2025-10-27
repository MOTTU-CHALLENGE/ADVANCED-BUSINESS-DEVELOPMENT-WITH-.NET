using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM_API_MVC.Models
{
    [Table("T_CM_DISPOSITIVO_IOT")]
    public class DispositivoIot
    {
        [Key]
        [Column("ID_DISPOSITIVO")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("NM_DISPOSITIVO")]
        [Required]
        [MaxLength(50)]
        public required string Nome { get; set; }

        [Column("DT_INSTALACAO")]
        [Required]
        public DateTime DataInstalacao { get; set; }

        [Column("DS_OBS")]
        public string? Descricao { get; set; }

        [Column("ID_MOTO")]
        public int? IdMoto { get; set; }

        [ForeignKey("IdMoto")]
        public Moto? Moto { get; set; }

        public ICollection<PosicaoMoto> Posicoes { get; set; } = new List<PosicaoMoto>();

    }
}
