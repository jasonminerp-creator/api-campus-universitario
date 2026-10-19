namespace CampusApi.Dtos;

public class AsignacionResponse
{
    public int AsignacionId { get; set; }
    public int EstudianteId { get; set; }
    public string Estudiante { get; set; } = string.Empty;
    public int CursoId { get; set; }
    public string Curso { get; set; } = string.Empty;
    public int DocenteId { get; set; }
    public string Docente { get; set; } = string.Empty;
}


