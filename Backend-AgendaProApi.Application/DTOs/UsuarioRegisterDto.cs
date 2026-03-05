namespace Backend_AgendaProApi.Application.DTOs
{
    public class UsuarioRegisterDto
    {
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int IdRol { get; set; }
    }
}
