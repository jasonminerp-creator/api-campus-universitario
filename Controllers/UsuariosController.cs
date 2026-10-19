using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CampusApi.Data;
using CampusApi.Dtos;
using CampusApi.Domain;
using CampusApi.Services;

namespace CampusApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController(AcademicDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioResponse>>> GetAll()
    {
        return await context.Usuarios.AsNoTracking()
            .OrderBy(u => u.UsuarioId)
            .Select(u => new UsuarioResponse(u.UsuarioId, u.Nombre, u.Correo, u.FechaCreacion))
            .ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsuarioResponse>> GetById(int id)
    {
        var usuario = await context.Usuarios.AsNoTracking()
            .FirstOrDefaultAsync(u => u.UsuarioId == id);

        return usuario is null ? NotFound() : ToResponse(usuario);
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioResponse>> Create(CrearUsuarioRequest request)
    {
        var correo = NormalizeEmail(request.Correo);
        if (await context.Usuarios.AnyAsync(u => u.Correo == correo))
        {
            return Conflict(new { mensaje = "Ya existe un usuario con ese correo." });
        }

        var usuario = new Usuario
        {
            Nombre = request.Nombre.Trim(),
            Correo = correo,
            PasswordHash = PasswordHashService.Hash(request.Password)
        };

        context.Usuarios.Add(usuario);
        await context.SaveChangesAsync();

        var response = ToResponse(usuario);
        return CreatedAtAction(nameof(GetById), new { id = usuario.UsuarioId }, response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UsuarioResponse>> Login(LoginRequest request)
    {
        var correo = NormalizeEmail(request.Correo);
        var usuario = await context.Usuarios.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Correo == correo);

        if (usuario is null || !PasswordHashService.Verify(request.Password, usuario.PasswordHash))
        {
            return Unauthorized(new { mensaje = "Correo o contraseña incorrectos." });
        }

        return Ok(ToResponse(usuario));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ActualizarUsuarioRequest request)
    {
        var usuario = await context.Usuarios.FindAsync(id);
        if (usuario is null)
        {
            return NotFound();
        }

        var correo = NormalizeEmail(request.Correo);
        if (await context.Usuarios.AnyAsync(u => u.Correo == correo && u.UsuarioId != id))
        {
            return Conflict(new { mensaje = "Ya existe un usuario con ese correo." });
        }

        usuario.Nombre = request.Nombre.Trim();
        usuario.Correo = correo;
        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id:int}/password")]
    public async Task<IActionResult> ChangePassword(int id, CambiarPasswordRequest request)
    {
        var usuario = await context.Usuarios.FindAsync(id);
        if (usuario is null)
        {
            return NotFound();
        }

        if (!PasswordHashService.Verify(request.PasswordActual, usuario.PasswordHash))
        {
            return Unauthorized(new { mensaje = "La contraseña actual es incorrecta." });
        }

        usuario.PasswordHash = PasswordHashService.Hash(request.PasswordNueva);
        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var usuario = await context.Usuarios.FindAsync(id);
        if (usuario is null)
        {
            return NotFound();
        }

        context.Usuarios.Remove(usuario);
        await context.SaveChangesAsync();
        return NoContent();
    }

    private static string NormalizeEmail(string email) => email.Trim().ToLowerInvariant();

    private static UsuarioResponse ToResponse(Usuario usuario) =>
        new(usuario.UsuarioId, usuario.Nombre, usuario.Correo, usuario.FechaCreacion);
}


