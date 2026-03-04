namespace Backend_AgendaProApi.Domain.Entities
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}