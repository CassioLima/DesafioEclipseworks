using Microsoft.AspNetCore.Mvc;
using Application;

namespace API
{
    public class ProjetoController : ControllerBaseLocal
    {
        [HttpGet, Route("")]
        public async Task<IActionResult> Projetos([FromQuery]ProjetoQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet, Route("usuario/{idUsuario}")]
        public async Task<IActionResult> ProjetosDoUsuario([FromRoute] ProjetoUsuarioQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

    }
}
