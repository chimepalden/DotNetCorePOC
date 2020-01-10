using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotnetcorePoc.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        /*
         * Injecting ILogger service to log custom messagessages, warnings and exceptions
         * Using ILogger service methods, Loginformation, LogWarning, LogError... to log
         */
        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        } 

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you have requested could not be found.";
                    // logging 404 error as LogWarning
                    logger.LogWarning($"404 Error occured. Path = {statusCodeResult.OriginalPath}" +
                        $" and QueryString = {statusCodeResult.OriginalQueryString}");
                    break;
            }
            return View("NotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            //logging exception details using string interpolation
            logger.LogError($"The path here {exceptionDetails.Path} threw an exceptioin " +
                $"{exceptionDetails.Error}");
            /*
             * ViewBag.ExceptionPath = exceptionDetails.Path;
             * ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
             * ViewBag.Stacktrace = exceptionDetails.Error.StackTrace;
            */
            return View();
        }

        [Route("/foo/bar", Name = "urlError")]
        public IActionResult UrlError()
        {
            return View();
        }

    }
}
