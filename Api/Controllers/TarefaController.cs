using Microsoft.AspNetCore.Mvc;
using Application;
using Application.Command;
using MediatR;

namespace API
{
    public class TarefaController : ControllerBaseLocal
    {
        [HttpGet, Route("")]
        public async Task<IActionResult> Tarefas([FromQuery]TarefaQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet, Route("projeto/{idProjeto}")]
        public async Task<IActionResult> TarefasDoProjeto([FromRoute] TarefaProjetoQuery request)
        {
            return Ok(await Mediator.Send(request));
        }
       
        [HttpPost, Route("")]
        public async Task<IActionResult> Tarefa(string projeto, [FromBody] TarefaCriarComand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPost, Route("projeto/{idProjeto}")]
        public async Task<IActionResult> AdicionarProjeto(string projeto, [FromBody] TarefaAdicionarProjetoComand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpDelete, Route("projeto/{idProjeto}")]
        public async Task<IActionResult> RemoverProjeto(string projeto, [FromBody] TarefaRemoverProjetoComand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPut, Route("{idTarefa}")]
        public async Task<IActionResult> Tarefa(string projeto, [FromBody] TarefaAtualizarComand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPost, Route("comentario/tarefa/{idTarefa}/usuario/{idUsuario}")]
        public async Task<IActionResult> AdicionarProjeto(string projeto, [FromBody] TarefaAdicionarComentarioComand request)
        {
            return Ok(await Mediator.Send(request));
        }

    }
}
