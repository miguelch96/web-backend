using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PichangAppService.Dtos.POSTDtos
{
    public class RetoPostDto
    {
        public Int32? RetoId { get; set; }
        public Int32 EquipoRetadorId { get; set; }
        public Int32 EquipoRetadoId { get; set; }  
        public Int32? ReservaId { get; set; }

        //PARA LA RESERVA
        public Int32 HorarioId { get; set; }
        public Int32 UsuarioId { get; set; }
        public Int32 CanchaId { get; set; }
        public DateTime FechaEncuentro { get; set; }
    }
}

