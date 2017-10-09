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
    public class CanchasController : ApiController
    {
        public HttpResponseMessage Get()
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var canchas = entities.Cancha.Where(x => x.Estado == "ACT").ProjectTo<CanchaDto>().ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, canchas);
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
                    var cancha = entities.Cancha.SingleOrDefault(x => x.CanchaId == id);

                    if (cancha == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cancha con Id= " + id + " no encontrado");
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Cancha, CanchaDto>(cancha));
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);

            }
        }


        [HttpPost]
        public HttpResponseMessage Post([FromBody] CanchaDto canchaDto)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var cancha = Mapper.Map<CanchaDto, Cancha>(canchaDto);
                    cancha.Estado = "ACT";
                    entities.Cancha.Add(cancha);
                    entities.SaveChanges();
                    canchaDto.CanchaId = cancha.CanchaId;

                    var message = Request.CreateResponse(HttpStatusCode.Created, canchaDto);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + cancha.CanchaId);
                    return message;
                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }

        }    

        [HttpPut]
        public IHttpActionResult Put(CanchaDto canchaDto)
        {
            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                var cancha = Mapper.Map<CanchaDto, Cancha>(canchaDto);
                entities.Cancha.Add(cancha);
                entities.SaveChanges();

                canchaDto.CanchaId = cancha.CanchaId;
                return Created(new Uri(Request.RequestUri + "/" + cancha.CanchaId), canchaDto);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var canchaInDb = entities.Cancha.Where(x=>x.Estado=="ACT").SingleOrDefault(x => x.CanchaId == id);
                    if (canchaInDb == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Cancha no encontrada");
                    }

                    canchaInDb.Estado = "INA";
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
