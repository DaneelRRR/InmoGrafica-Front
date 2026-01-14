using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Ambientes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmbienteController : ControllerBase
    {
        private readonly ListarAmbientesInteractor _listarInteractor;

        public AmbienteController(ListarAmbientesInteractor listarInteractor)
        {
            _listarInteractor = listarInteractor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _listarInteractor.Handle();
            return Ok(result);
        }
    }
}
