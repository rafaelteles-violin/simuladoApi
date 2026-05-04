using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Simulado.Application;
using Simulado.Application.Commands.DisciplinaCommand;
using Simulado.Application.Interface;
using Simulado.Application.ViewModel;
using Simulado.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simulado.API.Controllers
{
    [Route("api/disciplina")]
    public class DisciplinaController : MainController
    {
        private readonly IDisciplinaApplication _disciplinaApplication;
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public DisciplinaController(IDisciplinaApplication disciplinaApplication,
                                    ILogger<DisciplinaController> logger,
                                    IMediator mediator)
        {
            _disciplinaApplication = disciplinaApplication;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Adicionar([FromBody] DisciplinaViewModel disciplinaVm)
        {
            try
            {
                var commando = new AdicionarDisciplinaCommand(disciplinaVm.Descricao, Simulado.Domain.Enum.TipoDisciplinaEnum.AVALIACAO, disciplinaVm.TotalExibicaoQuestao);

                var result = await _mediator.Send(commando);

                return CustomResponse(result,
                    result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Atualizar([FromBody] DisciplinaViewModel disciplinaVm)
        {
            try
            {
                var commando = new AtualizarDisciplinaCommand(disciplinaVm);

                var result = await _mediator.Send(commando);

                return CustomResponse(result,
                    result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<List<DisciplinaViewModel>> ObterTodos()
        {
            return await _disciplinaApplication.ObterTodasDisciplinas();
        }

        [HttpGet("{id:Guid}")]
        public async Task<DisciplinaViewModel> ObterPorId(Guid id)
        {
            return await _disciplinaApplication.ObterDisciplinaPorId(id);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            var result = await _disciplinaApplication.RemoverDisciplina(id);
            return CustomResponse(result, result.Erros);
        }
    }
}
