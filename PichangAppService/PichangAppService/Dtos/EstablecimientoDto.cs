using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using PichangAppDataAccess;

namespace PichangAppService.Dtos
{
    public class EstablecimientoDto
    {
        public Int32 EstablecimientoId{ get; set; }
        public String Nombre{ get; set; }
        public String Direccion{ get; set; }
        public String Descripcion{ get; set; }
        public byte DistritoId { get; set; }
        public Int32 PropietarioId { get; set; }
        public DistritoDto Distrito { get; set; }
        public UsuarioDto Propietario { get; set; }

        public IEnumerable<ServicioDto> Servicios { get; set; }


    }
}