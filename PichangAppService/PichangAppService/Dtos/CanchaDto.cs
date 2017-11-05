using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using PichangAppDataAccess;

namespace PichangAppService.Dtos
{
    public class CanchaDto
    {
        public Int32 CanchaId{ get; set; }
        public String Nombre{ get; set; }
        public String Descripcion{ get; set; }
        public Decimal Precio{ get; set; }
        public String Direccion { get; set; }
        public Decimal Calificacion { get; set; }
        public String NombreTipoSuperficie { get; set; }
        public String NombreDistrito { get; set; }
        public String ImagenPortadaUrl { get; set; }
        public IEnumerable<ImagenCanchaDto> Imagenes { get; set; }
        public IEnumerable<ServicioDto> Servicios { get; set; }
        public IEnumerable<ComentarioCanchaDto> Comentarios { get; set; }
    }
}