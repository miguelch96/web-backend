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
            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                var canchas = entities.Cancha.Where(x => x.Estado == "ACT").ProjectTo<CanchaDto>().ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    StatusCode=HttpStatusCode.OK,
                    Canchas = canchas
                });
            }
        }


        public IHttpActionResult Get(Int32 id)
        {
            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                var cancha = entities.Cancha.SingleOrDefault(x => x.CanchaId == id);
                if (cancha == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                return Ok(Mapper.Map<Cancha, CanchaDto>(cancha));
            }
        }

        [HttpPost]
        public IHttpActionResult Post(CanchaDto canchaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                var cancha = Mapper.Map<CanchaDto, Cancha>(canchaDto);
                entities.Cancha.Add(cancha);
                entities.SaveChanges();

                canchaDto.CanchaId = cancha.CanchaId;
                return Created(new Uri(Request.RequestUri+"/"+cancha.CanchaId),canchaDto );
            }
        }

        [HttpPost]
        public IHttpActionResult Put(CanchaDto canchaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                var cancha = Mapper.Map<CanchaDto, Cancha>(canchaDto);
                entities.Cancha.Add(cancha);
                entities.SaveChanges();

                canchaDto.CanchaId = cancha.CanchaId;
                return Created(new Uri(Request.RequestUri + "/" + cancha.CanchaId), canchaDto);
            }
        }

        [HttpPost]
        public IHttpActionResult Delete(int CanchaId)
        {
            
  
            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                var canchaInDb = entities.Cancha.SingleOrDefault(x => x.CanchaId == CanchaId);
                if (canchaInDb==null)
                {
                    return NotFound();
                }

                canchaInDb.Estado = "INA";
                entities.SaveChanges();
                return Ok();
            }
        }
    }
}
