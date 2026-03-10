using Backend_AgendaProApi.Application.DTOs;
using Backend_AgendaProApi.Application.Interface;
using Backend_AgendaProApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend_AgendaProApi.Application.Services
{
    public class RolService : IRolService
    {
        private readonly AgendaDbContext _db;

        public RolService(AgendaDbContext db)
        {
            _db = db;
        }

        public async Task<List<RolResponseDto>> ObtenerRolesAsync()
        {
            return await _db.Roles
                .AsNoTracking()
                .OrderBy(r => r.IdRol)
                .Select(r => new RolResponseDto
                {
                    IdRol = r.IdRol,
                    Nombre = r.Nombre
                })
                .ToListAsync();
        }
    }
}