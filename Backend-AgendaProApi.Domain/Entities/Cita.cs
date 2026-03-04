namespace Backend_AgendaProApi.Domain.Entities
{
    public class Cita
    {
        public int IdCita { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaHora { get; set; }
        public string Descripcion { get; set; } = null!;
        public Usuario Usuario { get; set; } = null!;
    }
}