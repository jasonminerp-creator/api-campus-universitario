using System.ComponentModel.DataAnnotations;

namespace CampusApi.Dtos;

public class CrearUsuarioRequest
{
    [Required, StringLength(100, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(150)]
    public string Correo { get; set; } = string.Empty;

    [Required, StringLength(128, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;
}

public class ActualizarUsuarioRequest
{
    [Required, StringLength(100, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(150)]
    public string Correo { get; set; } = string.Empty;
}

public class CambiarPasswordRequest
{
    [Required]
    public string PasswordActual { get; set; } = string.Empty;

    [Required, StringLength(128, MinimumLength = 8)]
    public string PasswordNueva { get; set; } = string.Empty;
}

public class LoginRequest
{
    [Required, EmailAddress, StringLength(150)]
    public string Correo { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}

public record UsuarioResponse(int UsuarioId, string Nombre, string Correo, DateTime FechaCreacion);


