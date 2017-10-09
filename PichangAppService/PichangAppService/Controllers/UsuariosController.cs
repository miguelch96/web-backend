using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PichangAppDataAccess;
using PichangAppService.Dtos;

namespace PichangAppService.Controllers
{
    public class UsuariosController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var usuarios =
                        entities.Usuario.Where(x => x.Estado == "ACT").ProjectTo<UsuarioDto>()
                            .ToList();
                    return Request.CreateResponse(HttpStatusCode.OK,usuarios);
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
           
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var usuario = Mapper.Map<UsuarioDto, Usuario>(usuarioDto);
                    usuario.Estado = "ACT";
                    entities.Usuario.Add(usuario);
                    entities.SaveChanges();
                    usuarioDto.UsuarioId = usuario.UsuarioId;

                    var message = Request.CreateResponse(HttpStatusCode.Created,usuarioDto);
                    message.Headers.Location = new Uri(Request.RequestUri +"/"+ usuario.UsuarioId);
                    return message;
                }
            }

            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var usuario = entities.Usuario.SingleOrDefault(x => x.UsuarioId == id);
                    if (usuario == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Usuario con Id= " + id + " not found");
                    }
                    usuario.Estado = "INA";
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);

                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,e);
            }
        }
    }
}