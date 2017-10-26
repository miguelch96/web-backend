using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PichangAppService.Dtos
{
    public class EquipoDto
    {
        public Int32 EquipoId { get; set; }
        public byte DeporteId { get; set; }
        public Int32 CapitanId { get; set; }
        public Int32 DistritoId { get; set; }
        public byte CategoriaId { get; set; }

        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public Decimal Calificacion { get; set; }

        public CategoriaDto Categoria { get; set; }
        public DistritoDto Distrito { get; set; }
        public DeporteDto Deporte { get; set; }
        public String ImagenPortadaUrl { get; set; }

        public IEnumerable<ImagenEquipoDto> Imagenes { get; set; }
        public IEnumerable<SkillEquipoDto> Skills { get; set; }

        public UsuarioDto Capitan { get; set; }
        public IEnumerable<EquipoUsuarioDto> Miembros { get; set; }
        public IEnumerable<ComentarioEquipoDto> Comentarios { get; set; }
       

       


    }
}