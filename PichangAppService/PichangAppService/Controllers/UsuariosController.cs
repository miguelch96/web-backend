using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PichangAppDataAccess;
using PichangAppService.Dtos;
using PichangAppService.Dtos.POSTDtos;
using PichangAppService.SwaggerExamples;
using Swashbuckle.Examples;
using Swashbuckle.Swagger;
using Swashbuckle.Swagger.Annotations;

namespace PichangAppService.Controllers
{
    public class UsuariosController : ApiController
    {
        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var usuarios =
                        entities.Usuario.Where(x => x.Estado == "ACT").ProjectTo<UsuarioDto>()
                            .ToList();
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            Usuarios = usuarios
                        });
                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        public HttpResponseMessage Get(Int32 id)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var usuario = entities.Usuario.SingleOrDefault(x => x.UsuarioId == id);

                    if (usuario == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Usuario con Id= " + id + " no encontrado");
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Usuario, UsuarioDto>(usuario));
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);

            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] UsuarioPostDto usuarioDto)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var usuario = Mapper.Map<UsuarioPostDto, Usuario>(usuarioDto);
                    usuario.Estado = "ACT";
                    entities.Usuario.Add(usuario);
                    entities.SaveChanges();
                    usuarioDto.UsuarioId = usuario.UsuarioId;

                    var message = Request.CreateResponse(HttpStatusCode.Created, usuarioDto);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + usuario.UsuarioId);
                    return message;
                }
            }

            catch (DbEntityValidationException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
        
        public HttpResponseMessage Put(int id, [FromBody] UsuarioPostDto usuarioDto)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var usuario = entities.Usuario.SingleOrDefault(x => x.UsuarioId == id);
                    if (usuario == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Usuario no encontrado");
                    }

                    Mapper.Map(usuarioDto, usuario);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, usuarioDto);
                }
            }

            catch (DbEntityValidationException e)
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
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Usuario con Id= " + id + " not found");
                    }
                    usuario.Estado = "INA";
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK,new
                    {
                        Message="Usuario eliminado correctamente"
                    });
                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [Route("~/api/usuarios/{usuarioId:int}/reservas")]
        public HttpResponseMessage GetTeamMembers(Int32 usuarioId)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var reservasRealizadas = entities.Reserva.Where(x => x.UsuarioId == usuarioId).ProjectTo<ReservaUsuarioDto>().ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        usuarioId,
                        reservasRealizadas
                    });
                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}