using CoreTest.Context;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreTest.Controllers
{
    [Route("api/[controller]")]
    public class AppController : BaseController
    {
        private readonly CDEContext _context;

        public AppController(CDEContext context)
            : base(context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Get()
        {
            {
                var list = _context.AgendaEvents.FirstOrDefault();
            }

            return Ok(new { Result = " Everything is up and running !!", IsAuthorized = base.IsAuthorized() });
        }
    }
}
