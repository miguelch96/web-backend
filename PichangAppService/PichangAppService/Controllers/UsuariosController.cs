using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PichangAppDataAccess;
using PichangAppService.Dtos;

namespace PichangAppService.Controllers
{
    public class UsuariosController : ApiController
    {
        public IEnumerable<Usuario> Get()
        {
            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                var usuarios = entities.Usuario.Where(x => x.Estado == "ACT").ToList();//.Select(Mapper.Map<Usuario, UsuarioDto>);
                return usuarios;
            }
        }

        public IHttpActionResult Get(Int32 id)
        {
            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                var usuario = entities.Usuario.SingleOrDefault(x => x.UsuarioId == id);
                if (usuario == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                return Ok(Mapper.Map<Usuario, UsuarioDto>(usuario));
            }
        }
    }
}
