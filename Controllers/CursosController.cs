using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CampusApi.Data;
using CampusApi.Dtos;
using CampusApi.Domain;

namespace CampusApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CursosController(AcademicDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Curso>>> GetAll()
    {
        return await context.Cursos.AsNoTracking().ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Curso>> GetById(int id)
    {
        var curso = await context.Cursos.AsNoTracking()
            .FirstOrDefaultAsync(c => c.CursoId == id);

        return curso is null ? NotFound() : curso;
    }

    [HttpPost]
    public async Task<ActionResult<Curso>> Create(CursoRequest request)
    {
        var curso = new Curso { Nombre = request.Nombre.Trim() };
        context.Cursos.Add(curso);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = curso.CursoId }, curso);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CursoRequest request)
    {
        var curso = await context.Cursos.FindAsync(id);
        if (curso is null)
        {
            return NotFound();
        }

        curso.Nombre = request.Nombre.Trim();
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var curso = await context.Cursos.FindAsync(id);
        if (curso is null)
        {
            return NotFound();
        }

        context.Cursos.Remove(curso);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return Conflict(new { mensaje = "No se puede eliminar el curso porque tiene asignaciones." });
        }

        return NoContent();
    }
}


