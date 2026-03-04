namespace Backend_AgendaProApi.Domain.Entities
{
    public class Horario
    {
        public int IdHorarios { get; set; }

        public int IdEspecialista { get; set; }

        public int DiaSemana { get; set; }  // 1=Lunes ... 7=Domingo

        public TimeOnly HoraInicio { get; set; }

        public TimeOnly HoraFin { get; set; }

        public bool Estado { get; set; }

        // Navegación
        public Especialista Especialista { get; set; } = null!;

        public ICollection<BloqueHorario> BloquesHorario { get; set; } = new List<BloqueHorario>();
    }
}