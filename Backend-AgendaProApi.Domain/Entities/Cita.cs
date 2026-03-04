namespace Backend_AgendaProApi.Domain.Entities
{
    public class Cita
    {
        public int IdCitas { get; set; }

        public int IdUsuario { get; set; }

        public int IdBloqueHorario { get; set; }

        public DateOnly Fecha { get; set; }

        public string Motivo { get; set; } = null!;

        public bool Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        // Navegación
        public Usuario Usuario { get; set; } = null!;

        public BloqueHorario BloqueHorario { get; set; } = null!;
    }
}