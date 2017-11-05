using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PichangAppService.Dtos
{
    public class RetoDto
    {
        public Int32? RetoId { get; set; }
        public Int32? EquipoRetadorId { get; set; }
        public String EquipoRetador { get; set; }
        public Int32? EquipoRetadoId { get; set; }
        public String EquipoRetado { get; set; }
        public DateTime FechaEnvio { get; set; }
        public String Horario { get; set; }
        public DateTime FechaEncuentro { get; set; }
        public Int32 CanchaId { get; set; }
        public String Cancha { get; set; }
        public String Distrito { get; set; }
        public String Estado { get; set; }
    }
}