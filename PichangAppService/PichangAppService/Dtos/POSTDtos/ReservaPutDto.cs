using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PichangAppService.Dtos.POSTDtos
{
    public class ReservaPutDto
    {
        public Int32 ReservaId { get; set; }
        public Int32 UsuarioId { get; set; }
        public String Estado { get; set; }
    }
}