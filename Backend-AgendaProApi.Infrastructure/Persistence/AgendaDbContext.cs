using Backend_AgendaProApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend_AgendaProApi.Infrastructure.Persistence
{
    public class AgendaDbContext : DbContext
    {
        public AgendaDbContext(DbContextOptions<AgendaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Rol> Roles => Set<Rol>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Especialista> Especialistas => Set<Especialista>();
        public DbSet<Horario> Horarios => Set<Horario>();
        public DbSet<BloqueHorario> BloquesHorario => Set<BloqueHorario>();
        public DbSet<Cita> Citas => Set<Cita>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -------------------------
            // Rol
            // -------------------------
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol");

                entity.HasKey(e => e.IdRol);

                entity.Property(e => e.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasMany(e => e.Usuarios)
                      .WithOne(e => e.Rol)
                      .HasForeignKey(e => e.IdRol)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // -------------------------
            // Usuario
            // -------------------------
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");
 
                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.Nombre)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.HasIndex(e => e.Email)
                      .IsUnique();

                entity.Property(e => e.PasswordHash)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.Property(e => e.FechaCreacion)
                      .IsRequired();

                entity.Property(e => e.Estado)
                      .IsRequired();

                entity.HasMany(e => e.Citas)
                      .WithOne(e => e.Usuario)
                      .HasForeignKey(e => e.IdUsuario)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // -------------------------
            // Especialista
            // -------------------------
            modelBuilder.Entity<Especialista>(entity =>
            {
                entity.ToTable("Especialista");

                entity.HasKey(e => e.IdEspecialista);

                entity.Property(e => e.Nombre)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(e => e.Especialidad)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.HasIndex(e => e.Email)
                      .IsUnique();

                entity.Property(e => e.Estado)
                      .IsRequired();

                entity.HasMany(e => e.Horarios)
                      .WithOne(e => e.Especialista)
                      .HasForeignKey(e => e.IdEspecialista)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // -------------------------
            // Horario
            // -------------------------
            modelBuilder.Entity<Horario>(entity =>
            {
                entity.ToTable("Horarios");

                entity.HasKey(e => e.IdHorarios);

                entity.Property(e => e.DiaSemana)
                      .IsRequired();

                entity.Property(e => e.HoraInicio)
                      .IsRequired();

                entity.Property(e => e.HoraFin)
                      .IsRequired();

                entity.Property(e => e.Estado)
                      .IsRequired();

                entity.HasMany(e => e.BloquesHorario)
                      .WithOne(e => e.Horario)
                      .HasForeignKey(e => e.IdHorarios)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // -------------------------
            // BloqueHorario
            // -------------------------
            modelBuilder.Entity<BloqueHorario>(entity =>
            {
                entity.ToTable("BloquesHorario");

                entity.HasKey(e => e.IdBloqueHorario);

                entity.Property(e => e.HoraInicio)
                      .IsRequired();

                entity.Property(e => e.HoraFin)
                      .IsRequired();

                entity.Property(e => e.Disponibilidad)
                      .IsRequired();

                entity.HasMany(e => e.Citas)
                      .WithOne(e => e.BloqueHorario)
                      .HasForeignKey(e => e.IdBloqueHorario)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // -------------------------
            // Cita
            // -------------------------
            modelBuilder.Entity<Cita>(entity =>
            {
                entity.ToTable("Citas");

                entity.HasKey(e => e.IdCitas);

                entity.Property(e => e.Fecha)
                      .IsRequired();

                entity.Property(e => e.Motivo)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.Property(e => e.Estado)
                      .IsRequired();

                entity.Property(e => e.FechaCreacion)
                      .IsRequired();
            });
        }
    }
}