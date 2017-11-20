using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PichangAppDataAccess;
using PichangAppService.Dtos;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using PichangAppService.Dtos.POSTDtos;
using PichangAppService.SwaggerExamples;
using PichangAppService.SwaggerExamples.EquipoExamples;
using Swashbuckle.Examples;
using Swashbuckle.Swagger.Annotations;

namespace PichangAppService.Controllers
{
    public class EquiposController : ApiController
    {
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<EquipoDto>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(HttpResponseException))]
        public HttpResponseMessage Get(Int32? categoriaId=null,Int32? distritoId=null,String nombre=null)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    IQueryable<Equipo> equipos = entities.Equipo;
                    if (categoriaId != null)
                        equipos = equipos.Where(x => x.CategoriaId == categoriaId);
                    if (distritoId != null)
                        equipos = equipos.Where(x => x.DistritoId == distritoId);
                    if (nombre != null)
                        equipos = equipos.Where(x => x.Nombre.Contains(nombre));

                    var equiposDto = equipos.ProjectTo<EquipoDto>().ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        Equipos= equiposDto
                    });

                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }

        }

        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<EquipoDto>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(HttpResponseException))]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(String))]
        public HttpResponseMessage Get(Int32 id)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var equipo = entities.Equipo.SingleOrDefault(x => x.EquipoId == id);
                    if (equipo == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Equipo no encontrado");

                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        Equipo=Mapper.Map<Equipo, EquipoDto>(equipo)
                    });
                        
                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [Route("~/api/equipos/{equipoId:int}/reservas")]
        public HttpResponseMessage GetTeamMembers(Int32 equipoId)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var equipo = entities.Equipo.SingleOrDefault(x => x.EquipoId == equipoId);
                    if (equipo == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Equipo no encontrado");

                    var equipoDto = Mapper.Map<Equipo, EquipoDto>(equipo);
                    return Request.CreateResponse(HttpStatusCode.OK,new
                    {
                        equipoDto.EquipoId,
                        equipoDto.Miembros
                    });
                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [Route("~/api/equipos/{equipoId:int}/skills")]
        public HttpResponseMessage GetTeamSkills(Int32 equipoId)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var equipo = entities.Equipo.SingleOrDefault(x => x.EquipoId == equipoId);
                    if (equipo == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Equipo no encontrado");

                    var equipoDto = Mapper.Map<Equipo, EquipoDto>(equipo);
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        equipoDto.EquipoId,
                        equipoDto.Skills
                    });
                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [Route("~/api/equipos/{equipoId:int}/comentarios")]
        public HttpResponseMessage GetTeamComments(Int32 equipoId)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var equipo = entities.Equipo.SingleOrDefault(x => x.EquipoId == equipoId);
                    if (equipo == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Equipo no encontrado");

                    var comentarios = entities.ComentarioEquipo.Where(x => x.EquipoId == equipoId)
                        .ProjectTo<ComentarioEquipoDto>().ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        equipo.EquipoId,
                        comentarios
                    });
                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [Route("~/api/equipos/{equipoId:int}/retos")]
        public HttpResponseMessage GetTeamOffs(Int32 equipoId,String estado=null)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var equipo = entities.Equipo.SingleOrDefault(x => x.EquipoId == equipoId);
                    if (equipo == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Equipo no encontrado");

                    IQueryable<Reto> retosEnviados = entities.Reto.Where(x => x.EquipoRetadorId == equipoId);
                    IQueryable<Reto> retosRecibidos = entities.Reto.Where(x => x.EquipoRetadoId == equipoId);

                    
                    if (estado != null)
                    {
                        retosEnviados=retosEnviados.Where(x => x.Estado.Equals(estado));
                        retosRecibidos=retosRecibidos.Where(x => x.Estado.Equals(estado));
                    }

                    var retosEnviadosDto = retosEnviados.ProjectTo<RetoDto>().ToList();
                    var retosRecibidosDto = retosRecibidos.ProjectTo<RetoDto>().ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        equipo.EquipoId,
                        RetosEnviados= retosEnviadosDto,
                        RetosRecibidos= retosRecibidosDto
                    });
                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(EquipoPostDto),typeof(EquipoPostRequestExample))]
        public HttpResponseMessage Post([FromBody] EquipoPostDto equipoDto)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var equipo = Mapper.Map<EquipoPostDto, Equipo>(equipoDto);
                    var usuario = entities.Usuario.SingleOrDefault(x => x.UsuarioId == equipo.UsuarioCapitanId);

                    equipo.Estado = "ACT";
                    entities.Equipo.Add(equipo);
                    entities.SaveChanges();
                    equipoDto.EquipoId = equipo.EquipoId;
                    usuario.EquipoId = equipo.EquipoId;
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, equipoDto);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + equipo.EquipoId);
                    return message;
                }
            }

            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpPut]
        [SwaggerRequestExample(typeof(EquipoPutDto), typeof(EquipoPutRequestExample))]
        public HttpResponseMessage Put(int id, [FromBody] EquipoPutDto equipoDto)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var equipo = entities.Equipo.SingleOrDefault(x => x.EquipoId == id);
                    if (equipo == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Equipo no encontrado");
                    }

                    Mapper.Map(equipoDto, equipo);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, equipoDto);
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
                    var equipo = entities.Equipo.SingleOrDefault(x => x.EquipoId == id);
                    if (equipo == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Equipo con Id= " + id + " not found");
                    }
                    equipo.Estado = "INA";
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);

                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}
