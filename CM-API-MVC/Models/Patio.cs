using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM_API_MVC.Models
{
    [Table("T_CM_PATIO")]
    public class Patio
    {
        [Key]
        [Column("ID_PATIO")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPatio { get; set; }

        [Column("ID_FILIAL")]
        [Required]
        public int IdFilial { get; set; }

        [Column("NM_PATIO")]
        [Required]
        [MaxLength(100)]
        public string NomePatio { get; set; }

        [Column("NR_CAP_MAX")]
        public int? CapacidadeMax { get; set; }

        [Column("VL_AREA_PATIO", TypeName = "decimal(6,2)")]
        public decimal? Area { get; set; }

        [Column("DS_OBS")]
        public string? Descricao { get; set; }

        [ForeignKey("IdFilial")]
        public Filial Filial { get; set; }

        public ICollection<ReceptorWifi> ReceptorWifi { get; set; } = new List<ReceptorWifi>();

        public ICollection<PosicaoMoto> PosicaoMoto { get; set; } = new HashSet<PosicaoMoto>();
    }
}
