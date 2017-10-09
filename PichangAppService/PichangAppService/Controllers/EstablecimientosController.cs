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
                    return Request.CreateResponse(HttpStatusCode.OK, establecimientos);

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