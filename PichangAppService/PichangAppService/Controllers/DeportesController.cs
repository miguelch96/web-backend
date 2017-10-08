using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PichangAppDataAccess;
using System.Data.Entity;
using AutoMapper;
using PichangAppService.Dtos;
using PichangAppService.Models;


namespace PichangAppService.Controllers
{
    public class DeportesController : ApiController
    {
     
        public IEnumerable<DeporteDto> Get()
        {
            using (PichangAppEntities entities =new PichangAppEntities())
            {
                return entities.Deporte.ToList().Select(Mapper.Map<Deporte,DeporteDto>);
            }   
        }


        public DeporteDto Get(Int32 id)
        {
            using (PichangAppEntities entities = new PichangAppEntities())
            {
                var deporte = entities.Deporte.SingleOrDefault(x => x.DeporteId == id);
                if(deporte==null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                return Mapper.Map<Deporte, DeporteDto>(deporte);
            }
        }
    }
}
