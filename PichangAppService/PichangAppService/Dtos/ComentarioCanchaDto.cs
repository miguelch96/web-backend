using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PichangAppService.Dtos
{
    public class ComentarioCanchaDto
    {
        public String Comentario { get; set; }
        public Decimal Calificacion { get; set; }
        public String NombreUsuario { get; set; }
        public String ImagenPerfilUrl { get; set; }
    }
}