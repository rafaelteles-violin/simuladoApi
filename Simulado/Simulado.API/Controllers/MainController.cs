using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Simulado.API.Controllers
{
    public abstract class MainController : ControllerBase
    {
        protected ActionResult CustomResponse(object result, List<string> erros)
        {
            if (erros is null || erros.Count == 0)
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = erros.ToArray()
            });
        }
    }
}
