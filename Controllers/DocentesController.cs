using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CampusApi.Data;
using CampusApi.Dtos;
using CampusApi.Domain;

namespace CampusApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocentesController(AcademicDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Docente>>> GetAll()
    {
        return await context.Docentes.AsNoTracking().ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Docente>> GetById(int id)
    {
        var docente = await context.Docentes.AsNoTracking()
            .FirstOrDefaultAsync(d => d.DocenteId == id);

        return docente is null ? NotFound() : docente;
    }

    [HttpPost]
    public async Task<ActionResult<Docente>> Create(DocenteRequest request)
    {
        var docente = new Docente
        {
            Nombres = request.Nombres.Trim(),
            Apellidos = request.Apellidos.Trim()
        };

        context.Docentes.Add(docente);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = docente.DocenteId }, docente);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, DocenteRequest request)
    {
        var docente = await context.Docentes.FindAsync(id);
        if (docente is null)
        {
            return NotFound();
        }

        docente.Nombres = request.Nombres.Trim();
        docente.Apellidos = request.Apellidos.Trim();
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var docente = await context.Docentes.FindAsync(id);
        if (docente is null)
        {
            return NotFound();
        }

        context.Docentes.Remove(docente);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return Conflict(new { mensaje = "No se puede eliminar el docente porque tiene asignaciones." });
        }

        return NoContent();
    }
}


