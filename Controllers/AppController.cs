using AutoMapper;
using BackEnd.Context;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace BackEnd.Controllers
{
    [RoutePrefix("api/app")]
    public class AppController : BaseController
    {
        [HttpGet, Route()]
        public IHttpActionResult Get()
        {
            using (CDEContext context = new CDEContext())
            {
                var firstAgendaEvent = context.AgendaEvents.FirstOrDefault();
            }

            return Ok(" Everything is up and running !!");
        }
    }
}
