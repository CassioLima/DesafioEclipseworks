using Microsoft.AspNetCore.Mvc;
using Application;

namespace API
{
    public class RelatorioController : ControllerBaseLocal
    {
        [HttpPost, Route("DesempenhoUltimos30Dias")]
        public async Task<IActionResult> DesempenhoUltimos30Dias([FromQuery] TarefaRelatorioQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

    }
}
