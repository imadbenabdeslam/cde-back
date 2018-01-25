using CoreTest.Context;
using CoreTest.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Get()
        {
            try
            {
                _context.Database.Migrate();

                if (await _context.EntitySet<AdminData>().AnyAsync() == false)
                {
                    DbInitializer.Initialize(_context);
                }

                return ProcessResponse(new { Result = true, Message = "Migrated !", IsAuthorized = base.IsAuthorized() });
            }
            catch (global::System.Exception ex)
            {
                return ProcessResponse(new { Result = false,  ex.Message, IsAuthorized = base.IsAuthorized() });
            }
        }
    }
}
