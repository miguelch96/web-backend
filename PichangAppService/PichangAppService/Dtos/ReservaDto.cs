using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PichangAppService.Dtos
{
    public class ReservaDto
    {
        public Int32 ReservaId { get; set; }
        public UsuarioDto Usuario { get; set;}
        public DateTime FechaSolicitud { get; set; }
        public String Estado { get; set; }  
    }
}