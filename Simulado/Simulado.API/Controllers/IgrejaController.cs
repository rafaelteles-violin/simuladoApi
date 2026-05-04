using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simulado.Application.Interface;
using Simulado.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simulado.API.Controllers
{
    [Route("api/igreja")]
    public class IgrejaController : MainController
    {
        private readonly IIgrejaApplication _igrejaApplication;

        public IgrejaController(IIgrejaApplication igrejaApplication)
        {
            _igrejaApplication = igrejaApplication;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        //[Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Adicionar([FromBody] IgrejaViewModel igrejaMv)
        {
            try
            {
                var result = await _igrejaApplication.Adicionar(igrejaMv);
                return CustomResponse(result, result.Erros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<List<IgrejaViewModel>> ObterTodos()
        {
            return await _igrejaApplication.ObterTodos();
        }
    }
}
