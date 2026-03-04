namespace Backend_AgendaProApi.Domain.Entities
{
    public class BloqueHorario
    {
        public int IdBloqueHorario { get; set; }

        public int IdHorarios { get; set; }

        public TimeOnly HoraInicio { get; set; }

        public TimeOnly HoraFin { get; set; }

        public bool Disponibilidad { get; set; }

        // Navegación
        public Horario Horario { get; set; } = null!;

        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}