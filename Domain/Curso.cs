using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusApi.Domain;

[Table("Curso")]
public class Curso
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CursoId { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    public ICollection<Asignacion> Asignaciones { get; set; } = [];
}


