using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Simulado.Application.Interface;
using Simulado.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simulado.API.Controllers
{

    [Route("api/usuario")]
    public class UsuarioController : MainController
    {
        private readonly IUsuarioApplication _usuarioApplication;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioApplication usuarioApplication, ILogger<UsuarioController> logger)
        {
            _usuarioApplication = usuarioApplication;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] UsuarioViewModel usuarioViewModel)
        {
            try
            {
                _logger.LogInformation("Adicionar Cliente");

                var result = await _usuarioApplication.Adicionar(usuarioViewModel);
                return CustomResponse(result, result.Erros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] UsuarioViewModel usuarioViewModel)
        {
            try
            {
                var result = await _usuarioApplication.Atualizar(usuarioViewModel);
                return CustomResponse(result, result.Erros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("atualizarEmail")]
        public async Task<IActionResult> AtualizarEmail([FromBody] UsuarioViewModel usuarioViewModel)
        {
            try
            {
                var result = await _usuarioApplication.AtualizarEmail(usuarioViewModel);
                return CustomResponse(result, result.Erros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("ObterIgrejasDoUsuario")]
        public async Task<List<IgrejaViewModel>> ObterIgrejasDoUsuario(Guid usuarioId)
        {
            return await _usuarioApplication.ObterIgrejasDoUsuario(usuarioId);
        }

        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<List<UsuarioViewModel>> ObterTodos()
        {
            return await _usuarioApplication.ObterTodos();
        }

        
    }
}
