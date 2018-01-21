using CoreTest.Context;
using CoreTest.Models;
using Microsoft.AspNetCore.Mvc;
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


        [HttpPost, Route("Authenticate/{pwd}")]
        public IActionResult Authenticate([FromRoute]string pwd)
        {
            try
            {
                var adminData = _context.AdminData.FirstOrDefault();

                if (adminData == null)
                    throw new Exception("No value admin found in the database...");

                var token = string.Empty ;
                var correctPwd = adminData.Password.Equals(pwd);
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
                return ProcessResponse(null, ex);
            }
        }
    }
}
