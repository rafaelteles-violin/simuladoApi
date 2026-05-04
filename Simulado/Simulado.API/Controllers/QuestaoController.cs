using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simulado.Application.Interface;
using Simulado.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simulado.API.Controllers
{
    [Route("api/questao")]
    public class QuestaoController : MainController
    {
        private readonly IQuestaoApplication _questaoApplication;

        public QuestaoController(IQuestaoApplication questaoApplication)
        {
            _questaoApplication = questaoApplication;
        }

        [HttpPost]
        //[Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Adicionar([FromBody] QuestaoViewModel questaoVm)
        {
            try
            {
                var result = await _questaoApplication.Adicionar(questaoVm);
                return CustomResponse(result, result.Erros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        //[Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Atualizar([FromBody] QuestaoViewModel questaoVm)
        {
            try
            {
                var result = await _questaoApplication.Atualizar(questaoVm);
                return CustomResponse(result, result.Erros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{questaoId:Guid}")]
        //[Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> RemoverQuestao(Guid questaoId)
        {
            try
            {
                var result = await _questaoApplication.RemoverQuestao(questaoId);
                return CustomResponse(result, result.Erros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<List<QuestaoViewModel>> ObterTodos()
        {
            return await _questaoApplication.ObterTodasQuestoes();
        }

        [HttpGet("{id:Guid}")]
        //[Authorize(Roles = "ADMINISTRADOR")]
        public async Task<QuestaoViewModel> ObterPorId(Guid id)
        {
            return await _questaoApplication.ObterQuestaoPorId(id);
        }

        [HttpGet("ObterTotalQuestaoPorDisciplina")]
        public async Task<List<DisciplinaQuestaoViewModel>> ObterTotalQuestaoPorDisciplina()
        {
            return await _questaoApplication.ObterTotalQuestaoPorDisciplina();
        }

        [HttpGet("ObterQuestoesPorDisciplina/{disciplinaId:Guid}")]
        [AllowAnonymous]
        public async Task<List<QuestaoViewModel>> ObterQuestoesPorDisciplina(Guid disciplinaId)
        {
            return await _questaoApplication.ObterQuestoesPorDisciplina(disciplinaId);
        }

        [HttpGet("ObterQuestoesParaRealizar/{disciplinaId:Guid}/{quantidade:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterQuestoesParaRealizar(Guid disciplinaId, int quantidade)
        {
            try
            {
                var result = await _questaoApplication.ObterQuestoesParaRealizar(disciplinaId, quantidade);
                return CustomResponse(result, result.Erros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        
        }
    }
}
