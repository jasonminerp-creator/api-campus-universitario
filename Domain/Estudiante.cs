using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusApi.Domain;

[Table("Estudiante")]
public class Estudiante
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EstudianteId { get; set; }

    [Required]
    [StringLength(50)]
    public string Nombres { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Apellidos { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Direccion { get; set; } = string.Empty;

    public ICollection<Asignacion> Asignaciones { get; set; } = [];
}


