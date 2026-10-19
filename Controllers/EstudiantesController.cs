using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CampusApi.Data;
using CampusApi.Dtos;
using CampusApi.Domain;

namespace CampusApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EstudiantesController(AcademicDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Estudiante>>> GetAll()
    {
        return await context.Estudiantes.AsNoTracking().ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Estudiante>> GetById(int id)
    {
        var estudiante = await context.Estudiantes.AsNoTracking()
            .FirstOrDefaultAsync(e => e.EstudianteId == id);

        return estudiante is null ? NotFound() : estudiante;
    }

    [HttpPost]
    public async Task<ActionResult<Estudiante>> Create(EstudianteRequest request)
    {
        var estudiante = new Estudiante
        {
            Nombres = request.Nombres.Trim(),
            Apellidos = request.Apellidos.Trim(),
            Direccion = request.Direccion.Trim()
        };

        context.Estudiantes.Add(estudiante);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = estudiante.EstudianteId }, estudiante);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, EstudianteRequest request)
    {
        var estudiante = await context.Estudiantes.FindAsync(id);
        if (estudiante is null)
        {
            return NotFound();
        }

        estudiante.Nombres = request.Nombres.Trim();
        estudiante.Apellidos = request.Apellidos.Trim();
        estudiante.Direccion = request.Direccion.Trim();
        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var estudiante = await context.Estudiantes.FindAsync(id);
        if (estudiante is null)
        {
            return NotFound();
        }

        context.Estudiantes.Remove(estudiante);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return Conflict(new { mensaje = "No se puede eliminar el estudiante porque tiene asignaciones." });
        }

        return NoContent();
    }
}


