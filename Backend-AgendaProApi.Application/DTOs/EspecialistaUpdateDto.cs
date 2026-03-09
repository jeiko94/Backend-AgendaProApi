using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_AgendaProApi.Application.DTOs
{
    public class EspecialistaUpdateDto
    {
        public string? Nombre { get; set; }
        public string? Especialidad { get; set; }
        public string? Email { get; set; }
    }
}
