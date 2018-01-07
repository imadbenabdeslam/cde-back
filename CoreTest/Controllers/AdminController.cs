using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreTest.Context;
using Microsoft.AspNetCore.Mvc;

namespace CoreTest.Controllers
{
    [Route("api/admin")]
    public class AdminController : Controller
    {
        private readonly CDEContext _context;

        public AdminController(CDEContext context)
        {
            _context = context;
        }


        [HttpPost, Route("Authenticate/{pwd}")]
        public IActionResult Authenticate([FromRoute]string pwd)
        {
            {
                var adminData = _context.AdminData.FirstOrDefault();

                if (adminData == null)
                    return BadRequest("No value admin found in the database...");

                if (adminData.Password.Equals(pwd))
                {
                    var token = Guid.NewGuid().ToString();
                    adminData.Token = token;

                    _context.SaveChangesAsync();

                    return Ok(new { Restult = true, Value = token });
                }
            }

            return Ok(new { Result = false, Value = string.Empty});
        }
    }
}
