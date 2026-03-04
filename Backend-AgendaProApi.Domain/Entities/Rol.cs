namespace Backend_AgendaProApi.Domain.Entities
{
    public class Rol
    {
        public int IdRol { get; set; }

        public string Nombre { get; set; } = null!;

        // Navegación
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}