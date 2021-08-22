using FreeGym.API.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeGym.API.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    public class ErrorController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
