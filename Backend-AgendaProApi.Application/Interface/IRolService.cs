using Backend_AgendaProApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_AgendaProApi.Application.Interface
{
    public interface IRolService
    {
        Task<List<RolResponseDto>> ObtenerRolesAsync();
    }
}
