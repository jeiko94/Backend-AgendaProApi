namespace Backend_AgendaProApi.Domain.Entities
{
    public class Especialista
    {
        public int IdEspecialista { get; set; }

        public string Nombre { get; set; } = null!;

        public string Especialidad { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool Estado { get; set; }

        // Navegación
        public ICollection<Horario> Horarios { get; set; } = new List<Horario>();
    }
}