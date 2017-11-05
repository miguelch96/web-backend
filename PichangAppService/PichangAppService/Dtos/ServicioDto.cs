using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PichangAppService.Dtos
{
    public class ServicioDto
    {
        public Int16 ServicioId { get; set; }
        public String Nombre { get; set; }
        public String ImgServicioUrl { get; set; }
    }
}