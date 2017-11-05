using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PichangAppService.Dtos
{
    public class RetosEquipoAux
    {
        public IEnumerable<RetoDto> Enviados { get; set; }
        public IEnumerable<RetoDto> Recibidos { get; set; }
    }
}