using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PichangAppService.Dtos
{
    public class EstablecimientoServicioDto
    {
        public Int32 EstablecimientoId { get; set; }
        [JsonIgnore]
        public EstablecimientoDto Establecimiento { get; set; }
        public Int16 ServicioId { get; set; }
        [JsonIgnore]
        public ServicioDto Servicio { get; set; }
        public String Nombre { get; set; }
    }
}