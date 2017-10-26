using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    public class EquipoMiembrosController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var miembros = entities.Equipo.ProjectTo<EquipoDto>().Select(x=>x.Miembros).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, miembros);

                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
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

                    var equipoDto = Mapper.Map<Equipo, EquipoDto>(equipo);

                    return Request.CreateResponse(HttpStatusCode.OK, new
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


        [HttpPut]
        public HttpResponseMessage Put(int equipoid, [FromBody] EquipoDto equipoDto)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var equipo = entities.Equipo.SingleOrDefault(x => x.EquipoId == equipoid);
                    if (equipo == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Equipo no encontrado");
                    }

                    if (equipoDto.Miembros.Count() != 0)
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
                                UsuarioId = miembro.MiembroId
                            });
                        }
                    }
                   

                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, equipoDto);
                }
            }

            catch (DbEntityValidationException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}
