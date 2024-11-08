using Microsoft.AspNetCore.Mvc;
using Application;

namespace API
{
    public class UsuarioController : ControllerBaseLocal
    {
        [HttpGet, Route("")]
        public async Task<IActionResult> Usuarios([FromQuery]UsuarioQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

    }
}
