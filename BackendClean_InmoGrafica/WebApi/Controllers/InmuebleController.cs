using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.UseCases.Inmuebles;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InmuebleController : ControllerBase
    {
        private readonly CrearInmuebleInteractor _crearInteractor;
        private readonly ListarInmueblesInteractor _listarInteractor;
        private readonly ObtenerInmuebleDetalleInteractor _detalleInteractor;

        public InmuebleController(
            CrearInmuebleInteractor crearInteractor,
            ListarInmueblesInteractor listarInteractor,
            ObtenerInmuebleDetalleInteractor detalleInteractor)
        {
            _crearInteractor = crearInteractor;
            _listarInteractor = listarInteractor;
            _detalleInteractor = detalleInteractor;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearInmuebleDto dto)
        {
            var resultado = await _crearInteractor.Handle(dto);
            // Esto creará la BD y las carpetas en D:\Bienes\ID
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inmuebles = await _listarInteractor.Handle();
            return Ok(inmuebles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _detalleInteractor.Handle(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
