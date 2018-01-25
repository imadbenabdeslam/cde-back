using CoreTest.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                _context.Database.Migrate();

                return ProcessResponse(new { Result = true, Message = "Migrated !", IsAuthorized = base.IsAuthorized() });
            }
            catch (global::System.Exception ex)
            {
                return ProcessResponse(new { Result = false,  ex.Message, IsAuthorized = base.IsAuthorized() });
            }
        }
    }
}
