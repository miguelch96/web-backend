using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PichangAppService.Dtos
{
    public class ReservasCanchaAux
    {
        public IEnumerable<ReservaDto> Lunes { get; set; }
        public IEnumerable<ReservaDto> Martes { get; set; }
        public IEnumerable<ReservaDto> Miercoles { get; set; }
        public IEnumerable<ReservaDto> Jueves { get; set; }
        public IEnumerable<ReservaDto> Viernes { get; set; }
        public IEnumerable<ReservaDto> Sabado { get; set; }
        public IEnumerable<ReservaDto> Domingo { get; set; }

    }
}