using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreTest.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CoreTest.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        private readonly CDEContext _context;

        public BaseController(CDEContext context)
        {
            _context = context;
        }


        protected bool IsAuthorized()
        {
            StringValues token = new StringValues();

            if (ControllerContext.HttpContext.Request.Headers.TryGetValue("Authorization", out token))
            {
                return _context.AdminData.FirstOrDefault().Token == token;
            }
            
            return false;
        }
    }
}
