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
using AutoMapper.QueryableExtensions;

namespace PichangAppService.Controllers
{
    public class EquiposController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var equipos = entities.Equipo.ProjectTo<EquipoDto>().ToList();
                    return Request.CreateResponse(HttpStatusCode.OK,new
                    {
                        Equipos=equipos
                    });

                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }

        }

        [HttpGet]
        public HttpResponseMessage Get(Int32 id)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var equipo = entities.Equipo.SingleOrDefault(x => x.EquipoId == id);
                    if (equipo == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Equipo no encontrado");

                    return Request.CreateResponse(HttpStatusCode.OK,
                        Mapper.Map<Equipo, EquipoDto>(equipo));
                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] EquipoDto equipoDto)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var equipo = Mapper.Map<EquipoDto, Equipo>(equipoDto);
                    equipo.Estado = "ACT";
                    entities.Equipo.Add(equipo);
                    entities.SaveChanges();
                    equipoDto.EquipoId = equipo.EquipoId;

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
        public HttpResponseMessage Put(int id, [FromBody] EquipoDto equipoDto)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var equipo = entities.Equipo.SingleOrDefault(x => x.EquipoId == id);
                    if (equipo == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Equipo no encontrada");
                    }

                    Mapper.Map(equipoDto, equipo);

                    if (equipoDto.Skills.Count() != 0)
                    {
                        //CAPTURAR TODOS LOS SKILLS ACTUALES
                        var skillsActuales = entities.SkillEquipo.Where(x => x.EquipoId == equipo.EquipoId).DefaultIfEmpty().ToList();

                        //ELIMINAR SKILLS ACTUALES
                        if (skillsActuales.Count != 0)
                        {
                            foreach (var skill in skillsActuales)
                            {
                                entities.SkillEquipo.Remove(skill);
                            }
                        }

                        //INSERTAR NUEVOS SKILLS
                        foreach (var skill in equipoDto.Skills)
                        {
                            entities.SkillEquipo.Add(new SkillEquipo()
                            {
                                EquipoId = equipo.EquipoId,
                                SkillId = skill.SkillId
                            });
                        }
                    }

                    if (equipoDto.Miembros.Count()!=0)
                    {
                        //CAPTURAR TODOS LOS MIEMBROS ACTUALES
                        var miembrosActuales = entities.EquipoUsuario.Where(x => x.EquipoId == equipo.EquipoId).DefaultIfEmpty().ToList();

                        //ELIMINAR MIEMROS ACTUALES
                        if (miembrosActuales.Count != 0)
                        {
                            foreach (var miembro in miembrosActuales)
                            {
                                entities.EquipoUsuario.Remove(miembro);
                            }
                        }

                        //INSERTAR NUEVOS MIEMBROS
                        foreach (var miembro in equipoDto.Miembros)
                        {
                            entities.EquipoUsuario.Add(new EquipoUsuario()
                            {
                                EquipoId = equipo.EquipoId,
                                UsuarioId = miembro.UsuarioId
                            });
                        }
                    }
                    
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
