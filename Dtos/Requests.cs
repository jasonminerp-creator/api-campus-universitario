using System.ComponentModel.DataAnnotations;

namespace CampusApi.Dtos;

public class EstudianteRequest
{
    [Required, StringLength(50, MinimumLength = 2)]
    public string Nombres { get; set; } = string.Empty;

    [Required, StringLength(50, MinimumLength = 2)]
    public string Apellidos { get; set; } = string.Empty;

    [Required, StringLength(200, MinimumLength = 2)]
    public string Direccion { get; set; } = string.Empty;
}

public class CursoRequest
{
    [Required, StringLength(100, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;
}

public class DocenteRequest
{
    [Required, StringLength(50, MinimumLength = 2)]
    public string Nombres { get; set; } = string.Empty;

    [Required, StringLength(50, MinimumLength = 2)]
    public string Apellidos { get; set; } = string.Empty;
}

public class AsignacionRequest
{
    [Range(1, int.MaxValue)]
    public int EstudianteId { get; set; }

    [Range(1, int.MaxValue)]
    public int CursoId { get; set; }

    [Range(1, int.MaxValue)]
    public int DocenteId { get; set; }
}


