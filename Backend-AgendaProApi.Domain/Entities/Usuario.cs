namespace Backend_AgendaProApi.Domain.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }
        public Rol Rol { get; set; } = null!;
        public ICollection<Cita> Cita {get; set;} = new List<Cita>();
    }
}