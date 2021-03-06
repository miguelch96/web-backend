﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PichangAppService.Dtos
{
    public class EquipoDto
    {
        public Int32 EquipoId { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public Decimal Calificacion { get; set; }

        public CategoriaDto Categoria { get; set; }
        public DistritoDto Distrito { get; set; }
        public String ImagenPortadaUrl { get; set; }

        public IEnumerable<ImagenEquipoDto> Imagenes { get; set; }
        public IEnumerable<SkillEquipoDto> Skills { get; set; }

        public UsuarioDto Capitan { get; set; }
        public IEnumerable<UsuarioDto> Miembros { get; set; }
        public IEnumerable<ComentarioEquipoDto> Comentarios { get; set; }

        public RetosEquipoAux Retos { get; set; }


    }
}