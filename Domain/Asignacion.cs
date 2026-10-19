using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusApi.Domain;

[Table("Asignacion")]
public class Asignacion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AsignacionId { get; set; }

    public int EstudianteId { get; set; }
    public int CursoId { get; set; }
    public int DocenteId { get; set; }

    [ForeignKey(nameof(EstudianteId))]
    public Estudiante Estudiante { get; set; } = null!;

    [ForeignKey(nameof(CursoId))]
    public Curso Curso { get; set; } = null!;

    [ForeignKey(nameof(DocenteId))]
    public Docente Docente { get; set; } = null!;
}


