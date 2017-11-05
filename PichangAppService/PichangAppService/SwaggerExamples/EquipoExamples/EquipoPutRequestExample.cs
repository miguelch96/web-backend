using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PichangAppService.Dtos.POSTDtos;
using Swashbuckle.Examples;

namespace PichangAppService.SwaggerExamples.EquipoExamples
{
    public class EquipoPutRequestExample:IExamplesProvider
    {
        public object GetExamples()
        {
            return new EquipoPutDto()
            {
                EquipoId=2,
                Nombre = "Toribianitos",
                Descripcion = "Equipo enfocado en Futbol 6",
                DeporteId = 1,
                CapitanId = 3,
                DistritoId = 2,
                CategoriaId = 1,
                ImagenPortadaUrl = "http://www.futboltorneos.com.ar/assets/futbol-7/_resampled/ResizedImage363319-futbol-7-torneo.jpg"
            };
        }
    }
}