using CoreTest.Context;
using CoreTest.Core;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;

namespace CoreTest.Controllers
{
    [Route("api/admin")]
    public class AdminController : BaseController
    {
        private readonly CDEContext _context;

        public AdminController(CDEContext context) : base(context)
        {
            _context = context;
        }


        [HttpPost, Route("Authenticate")]
        public IActionResult Authenticate([FromBody]PwdClass body)
        {
            try
            {
                Log.Information("AdminController.Authenticate -- Started with password " + body.PwdMessage);

                var adminData = _context.AdminData.FirstOrDefault();

                if (adminData == null)
                    throw new Exception("No value admin found in the database...");

                var token = string.Empty ;

                var hashedPwd = body.PwdMessage.GetHashString();
                var correctPwd = adminData.Password.Equals(hashedPwd);
                if (correctPwd)
                {
                    token = Guid.NewGuid().ToString();
                    adminData.Token = token;

                    _context.SaveChangesAsync();
                }

                return ProcessResponse(new { Result = correctPwd, Value = token });
            }
            catch (Exception ex)
            {
                Log.Error("AdminController.Authenticate -- An error occured");
                return ProcessResponse(null, ex);
            }
        }
    }

    public class PwdClass
    {
        public string PwdMessage { get; set; }
    }
}
