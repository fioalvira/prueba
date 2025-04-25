using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public sealed class ObjectSimulatorDbContext(DbContextOptions<ObjectSimulatorDbContext> options)
    : DbContext(options)
{
    public DbSet<Domain.Type> Types { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Atribute> Atributes { get; set; }
    public DbSet<Method> Methods { get; set; }
    public DbSet<Parameter> Parameters { get; set; }
    public DbSet<LocalVariable> LocalVariables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>()
            .HasMany(c => c.ClassMethods)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Class>()
            .HasMany(c => c.ClassAtributes)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Class>()
            .HasOne(c => c.BaseClass)
            .WithOne()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Method>()
            .HasMany(m => m.MethodParameters)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Method>()
            .HasMany(m => m.MethodLocalVariables)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Method>()
            .HasMany(m => m.InvokedMethods)
            .WithMany(); // self-referencing many-to-many
    }
}
