using Backend_AgendaProApi.Application.DTOs;
using Backend_AgendaProApi.Application.Interface;
using Backend_AgendaProApi.Domain.Entities;
using Backend_AgendaProApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend_AgendaProApi.Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly AgendaDbContext _db;

        public AuthService(AgendaDbContext db)
        {
            _db = db;
        }

        public async Task<string> RegistrarUsuarioAsync(UsuarioRegisterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password) ||
                string.IsNullOrWhiteSpace(dto.Nombre))
                throw new Exception("Datos incompletos");

            var email = dto.Email.Trim();

            if( await _db.Usuarios.AnyAsync(u => u.Email == email))
                throw new Exception("El email ya está registrado");

            // Validar que exista el rol
            var rolExiste = await _db.Roles.AnyAsync(r => r.IdRol == dto.IdRol);
            if (!rolExiste)
                throw new Exception("El rol no existe");

            var hashed = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var usuario = new Usuario
            {
                Nombre = dto.Nombre.Trim(),
                Email = email,
                PasswordHash = hashed,
                IdRol = dto.IdRol,
                Estado = true,
                FechaCreacion = DateTime.UtcNow
            };

            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();

            // SIN JWT: retorno simple
            return "Registro exitoso";
        }

        public async Task<LoginResponseDto> LoginAsync(UsuarioLoginDto dto)
        {
            var email = dto.Email?.Trim();
            var password = dto.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                throw new Exception("Email y Password son obligatorios");

            var usuario = await _db.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario is null)
                throw new Exception("Credenciales inválidas");

            if (!usuario.Estado)
                throw new Exception("Usuario inactivo");

            if (!BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash))
                throw new Exception("Credenciales inválidas");

            return new LoginResponseDto
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                IdRol = usuario.IdRol,
                RolNombre = usuario.Rol?.Nombre ?? "N/A"
            };
        }
    }
}
