using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simulado.Application.Interface;
using Simulado.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simulado.API.Controllers
{
    [Route("api/resposta")]
    public class RespostaController : MainController
    {
        private readonly IRespostaApplication _respostaApplication;

        public RespostaController(IRespostaApplication respostaApplication)
        {
            _respostaApplication = respostaApplication;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] List<RespostaViewModel> respostasVm)
        {
            try
            {
                var result = await _respostaApplication.Adicionar(respostasVm);
                return CustomResponse(result, result.Erros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] List<RespostaViewModel> respostasVm)
        {
            try
            {
                var result = await _respostaApplication.Atualizar(respostasVm);
                return CustomResponse(result, result.Erros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("obterRespostaUsuario/{usuarioId}")]
        public async Task<List<RespostaViewModel>> ObterRespostasUsuario(Guid usuarioId)
        {
            return await _respostaApplication.ObterTodasRespostasDoUsuario(usuarioId);
        }

        [HttpGet("{identificador:Guid}")]
        public async Task<List<RespostaViewModel>> ObterPorId(Guid identificador)
        {
            return await _respostaApplication.ObterRespostaPorIdentificador(identificador.ToString());
        }
    }
}
