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
    [Route("api/respostaSaldo")]
    public class RespostaSaldoController : MainController
    {
        private readonly IRespostaSaldoApplication _respostaSaldoApplication;
        private readonly ILogger _logger;

        public RespostaSaldoController(IRespostaSaldoApplication respostaSaldoApplication,
                                    ILogger<DisciplinaController> logger)
        {
            _respostaSaldoApplication = respostaSaldoApplication;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Adicionar([FromBody] RespostaSaldoViewModel respostaVm)
        {
            try
            {
                _logger.LogInformation($"Executando adicionar resposta {DateTime.Now}");

                var result = await _respostaSaldoApplication.AdicionarRespostaSaldo(respostaVm);
                return CustomResponse(result, result.Erros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<List<RespostaSaldoGetViewModel>> ObterTodos(List<Guid> idsIgreja = null)
        {
            return await _respostaSaldoApplication.ObterRespostaSaldo(idsIgreja);
        }


        [HttpGet("respostaAvaliacao")]
        public async Task<List<RespostaSaldoAvaliacaoViewModel>> ObterRespostaSaldoAvaliacao()
        {
            return await _respostaSaldoApplication.ObterRespostaSaldoAvaliacao();
        }
    }
}
