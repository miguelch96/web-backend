using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using AutoMapper;
using PichangAppDataAccess;
using PichangAppService.Dtos;
using System.Data.Entity;
using AutoMapper.QueryableExtensions;

namespace PichangAppService.Controllers
{
    public class EstablecimientosController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var establecimientos = entities.Establecimiento.ProjectTo<EstablecimientoDto>().ToList();
                    return Request.CreateResponse(HttpStatusCode.OK,new
                    {
                        Establecimientos=establecimientos
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
                    var establecimiento = entities.Establecimiento.SingleOrDefault(x => x.EstablecimientoId == id);
                    if (establecimiento == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Establecimiento no encontrado");

                    return Request.CreateResponse(HttpStatusCode.OK,
                        Mapper.Map<Establecimiento, EstablecimientoDto>(establecimiento));
                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put([FromUri] int id, [FromBody] EstablecimientoDto establecimientoDto)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var establecimiento = entities.Establecimiento.SingleOrDefault(x => x.EstablecimientoId == id);
                    if (establecimiento == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Establecimiento no encontrado");
                    }

                    
                    Mapper.Map(establecimientoDto, establecimiento);

                    //ELIMINAR SERVICIOS ANTERIORES
                    var serviciosActuales = entities.EstablecimientoServicio.Where(x => x.EstablecimientoId == establecimiento.EstablecimientoId).DefaultIfEmpty().ToList();
                    if (serviciosActuales.Count != 0)
                    {
                        foreach (var servicio in serviciosActuales)
                        {
                            entities.EstablecimientoServicio.Remove(servicio);
                        }
                    }

                    //INSERTAR NUEVOS SERVICIOS
                    foreach (var servicio in establecimientoDto.Servicios)
                    {
                        entities.EstablecimientoServicio.Add(new EstablecimientoServicio()
                        {
                            EstablecimientoId = establecimiento.EstablecimientoId,
                            ServicioId = servicio.ServicioId
                        });
                    }

                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, establecimientoDto);
                }
            }

            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] EstablecimientoDto establecimientoDto)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var establecimiento = Mapper.Map<EstablecimientoDto, Establecimiento>(establecimientoDto);
                    establecimiento.Estado = "ACT";
                    entities.Establecimiento.Add(establecimiento);
                    entities.SaveChanges();
                    establecimientoDto.EstablecimientoId = establecimiento.EstablecimientoId;

                    var message = Request.CreateResponse(HttpStatusCode.Created, establecimientoDto);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + establecimiento.EstablecimientoId);
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
                    var establecimiento = entities.Establecimiento.SingleOrDefault(x => x.EstablecimientoId == id);
                    if (establecimiento == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Establecimiento con Id= " + id + " not found");
                    }
                    establecimiento.Estado = "INA";
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