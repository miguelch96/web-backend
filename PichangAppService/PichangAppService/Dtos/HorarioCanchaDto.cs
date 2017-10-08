using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PichangAppService.Dtos
{
    public class HorarioCanchaDto
    {
        public HorarioDto Horario { get; set; }
        public ReservaDto Reserva { get; set; }
    }
}