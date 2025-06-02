using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TelemetriaMonitoramento.API.Enums;

namespace TelemetriaMonitoramento.API.Models
{
    public class Machine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "int")]
        public MachineStatus Status { get; set; } = MachineStatus.Desligada;
    }
}
