using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PichangAppService.Dtos.POSTDtos
{
    public class ReservaUsuarioDto
    {
        public Int32 ReservaId { get; set; } 
        public DateTime Fecha { get; set; }
        public String Dia { get; set; }
        public String Horas { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public String Estado { get; set; }
        public Int32 CanchaId { get; set; }
        public String NombreCancha { get; set; }
    }
}