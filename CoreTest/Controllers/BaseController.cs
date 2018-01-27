using CoreTest.Context;
using CoreTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Serilog;
using System;
using System.Linq;
using System.Net;

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

        /// <summary>
        /// Encapsulate the result for the typical response
        /// </summary>
        /// <param name="result"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected IActionResult ProcessResponse(object result, Exception ex = null)
        {
            OperationResult apiResult = new OperationResult();

            if (ex != null)
            {
                apiResult.ErrorMessage = BuildErrorMessage(ex);

                Log.Logger.Error("BaseController.ProcessResponse -- " + apiResult.ErrorMessage);

                apiResult.StatusCode = HttpStatusCode.InternalServerError;
            }
            else
                apiResult.Result = result;

            return Ok(apiResult);
        }

        /// <summary>
        /// Extract error message from exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected string BuildErrorMessage(Exception ex)
        {
            string errorMessage = ex.Message;

            if (ex.InnerException != null && string.IsNullOrWhiteSpace(ex.InnerException.Message) == false)
                errorMessage += " --- " + ex.InnerException.Message;

            return errorMessage;
        }
    }
}
