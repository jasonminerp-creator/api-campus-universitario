using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CampusApi.Data;
using CampusApi.Dtos;
using CampusApi.Domain;

namespace CampusApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AsignacionesController(AcademicDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AsignacionResponse>>> GetAll()
    {
        return await Query().ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AsignacionResponse>> GetById(int id)
    {
        var asignacion = await Query().FirstOrDefaultAsync(a => a.AsignacionId == id);
        return asignacion is null ? NotFound() : asignacion;
    }

    [HttpPost]
    public async Task<ActionResult<AsignacionResponse>> Create(AsignacionRequest request)
    {
        var validationResult = await ValidateReferences(request);
        if (validationResult is not null)
        {
            return validationResult;
        }

        var asignacion = new Asignacion
        {
            EstudianteId = request.EstudianteId,
            CursoId = request.CursoId,
            DocenteId = request.DocenteId
        };

        context.Asignaciones.Add(asignacion);
        await context.SaveChangesAsync();

        var response = await Query().FirstAsync(a => a.AsignacionId == asignacion.AsignacionId);
        return CreatedAtAction(nameof(GetById), new { id = asignacion.AsignacionId }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, AsignacionRequest request)
    {
        var asignacion = await context.Asignaciones.FindAsync(id);
        if (asignacion is null)
        {
            return NotFound();
        }

        var validationResult = await ValidateReferences(request);
        if (validationResult is not null)
        {
            return validationResult;
        }

        asignacion.EstudianteId = request.EstudianteId;
        asignacion.CursoId = request.CursoId;
        asignacion.DocenteId = request.DocenteId;
        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var asignacion = await context.Asignaciones.FindAsync(id);
        if (asignacion is null)
        {
            return NotFound();
        }

        context.Asignaciones.Remove(asignacion);
        await context.SaveChangesAsync();
        return NoContent();
    }

    private IQueryable<AsignacionResponse> Query()
    {
        return context.Asignaciones.AsNoTracking()
            .Select(a => new AsignacionResponse
            {
                AsignacionId = a.AsignacionId,
                EstudianteId = a.EstudianteId,
                Estudiante = a.Estudiante.Nombres + " " + a.Estudiante.Apellidos,
                CursoId = a.CursoId,
                Curso = a.Curso.Nombre,
                DocenteId = a.DocenteId,
                Docente = a.Docente.Nombres + " " + a.Docente.Apellidos
            });
    }

    private async Task<ActionResult?> ValidateReferences(AsignacionRequest request)
    {
        if (!await context.Estudiantes.AnyAsync(e => e.EstudianteId == request.EstudianteId))
        {
            return BadRequest(new { mensaje = "El estudiante indicado no existe." });
        }

        if (!await context.Cursos.AnyAsync(c => c.CursoId == request.CursoId))
        {
            return BadRequest(new { mensaje = "El curso indicado no existe." });
        }

        if (!await context.Docentes.AnyAsync(d => d.DocenteId == request.DocenteId))
        {
            return BadRequest(new { mensaje = "El docente indicado no existe." });
        }

        return null;
    }
}


