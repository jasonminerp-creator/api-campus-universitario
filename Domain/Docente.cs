using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusApi.Domain;

[Table("Docente")]
public class Docente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DocenteId { get; set; }

    [Required]
    [StringLength(50)]
    public string Nombres { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Apellidos { get; set; } = string.Empty;

    public ICollection<Asignacion> Asignaciones { get; set; } = [];
}


