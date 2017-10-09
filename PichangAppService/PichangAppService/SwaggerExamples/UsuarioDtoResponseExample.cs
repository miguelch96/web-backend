using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PichangAppService.Dtos;
using Swashbuckle.Examples;

namespace PichangAppService.SwaggerExamples
{
    public class UsuarioDtoResponseExample:IExamplesProvider
    {
        public object GetExamples()
        {
            return new UsuarioDto
            {
                UsuarioId = 1,
                Email = "miguel@gmail.com",
                Passwd = "A2gafdf24hF42G2G",
                Nombre = "Miguel",
                Apellido = "Chipana",
                RolId = 1,
                Rol=new RolDto()
                {
                    RolId = 1,
                    Nombre = "Jugador",
                    Descripcion = "Deportista que alquila espacios deportivos"
                }
            };
        }
    }
}