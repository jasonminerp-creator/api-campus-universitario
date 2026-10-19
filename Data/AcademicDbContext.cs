using Microsoft.EntityFrameworkCore;
using CampusApi.Domain;

namespace CampusApi.Data;

public class AcademicDbContext(DbContextOptions<AcademicDbContext> options)
    : DbContext(options)
{
    public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
    public DbSet<Curso> Cursos => Set<Curso>();
    public DbSet<Docente> Docentes => Set<Docente>();
    public DbSet<Asignacion> Asignaciones => Set<Asignacion>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Correo)
            .IsUnique();

        modelBuilder.Entity<Asignacion>(entity =>
        {
            entity.HasOne(a => a.Estudiante)
                .WithMany(e => e.Asignaciones)
                .HasForeignKey(a => a.EstudianteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Asignacion_Estudiante");

            entity.HasOne(a => a.Curso)
                .WithMany(c => c.Asignaciones)
                .HasForeignKey(a => a.CursoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Asignacion_Curso");

            entity.HasOne(a => a.Docente)
                .WithMany(d => d.Asignaciones)
                .HasForeignKey(a => a.DocenteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Asignacion_Docente");

        });
    }
}


