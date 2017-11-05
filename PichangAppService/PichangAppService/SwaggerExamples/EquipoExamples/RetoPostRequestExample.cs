using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PichangAppService.Dtos.POSTDtos;
using Swashbuckle.Examples;

namespace PichangAppService.SwaggerExamples.EquipoExamples
{
    public class RetoPostRequestExample:IExamplesProvider
    {
        public object GetExamples()
        {
            return new RetoPostDto
            {
                EquipoRetadorId = 1,
                EquipoRetadoId = 2,
                HorarioId = 3,
                UsuarioId = 2,
                CanchaId = 1,
                FechaEncuentro = DateTime.Now
            };
        }
    }
}
