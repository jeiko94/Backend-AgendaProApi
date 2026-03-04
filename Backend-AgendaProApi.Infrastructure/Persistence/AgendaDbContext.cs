using Microsoft.EntityFrameworkCore;
using Backend_AgendaProApi.Domain.Entities;

namespace Backend_AgendaProApi.Infrastructure.Persistence
{
    public class AgendaDbContext: DbContext
    {
        public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base(options) {}

        public DbSet<Rol> Roles { get; set; }
    }
}