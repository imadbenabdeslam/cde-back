using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreTest.Context;
using Microsoft.AspNetCore.Mvc;

namespace CoreTest.Controllers
{
    [Route("api/[controller]")]
    public class AppController : Controller
    {
        private readonly CDEContext _context;

        public AppController(CDEContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Get()
        {
            {
                var list = _context.AgendaEvents.FirstOrDefault();
            }

            return Ok(new { Result = " Everything is up and running !!" });
        }
    }
}
