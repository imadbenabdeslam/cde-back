using CoreTest.Context;
using CoreTest.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
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

                Log.Logger.Information("AppController -- Migrated DB");

                if (await _context.EntitySet<AdminData>().AnyAsync() == false)
                {
                    Log.Logger.Information("AppController -- Add admin data because they are missing");
                    DbInitializer.Initialize(_context);
                }

                Log.Logger.Information("AppController -- Everything is Fine");

                return ProcessResponse(new { Result = true, Message = "Migrated !", IsAuthorized = base.IsAuthorized() });
            }
            catch (global::System.Exception ex)
            {
                Log.Logger.Error("AppController -- An error occured");
                return ProcessResponse(new { Result = false,  ex.Message, IsAuthorized = base.IsAuthorized() });
            }
        }
    }
}
