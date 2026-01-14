using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.UseCases.Fotos;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotoController : ControllerBase
    {
        private readonly SubirFotoRawInteractor _subirRawInteractor;
        private readonly SubirFotoEditadaInteractor _subirEditadaInteractor;
        private readonly GestionarFotoInteractor _gestionarInteractor;
        private readonly DescargarFotosInteractor _descargarInteractor;

        public FotoController(
            SubirFotoRawInteractor subirRaw,
            SubirFotoEditadaInteractor subirEditada,
            GestionarFotoInteractor gestionar,
            DescargarFotosInteractor descargar)
        {
            _subirRawInteractor = subirRaw;
            _subirEditadaInteractor = subirEditada;
            _gestionarInteractor = gestionar;
            _descargarInteractor = descargar;
        }

        // Endpoint para FOTOGRAFOS
        [HttpPost("subir-raw")]
        public async Task<IActionResult> SubirRaw([FromForm] SubirFotoRequest request)
        {
            if (request.Archivo == null || request.Archivo.Length == 0) return BadRequest("No se envió archivo");

            using var stream = request.Archivo.OpenReadStream();

            var dto = new FotoCargaDto
            {
                InmuebleId = request.InmuebleId,
                UsuarioId = request.UsuarioId,
                NombreArchivo = request.Archivo.FileName,
                ArchivoStream = stream
            };

            var result = await _subirRawInteractor.Handle(dto);
            return Ok(result);
        }

        // Endpoint para GRAFISTAS
        [HttpPost("subir-editada")]
        public async Task<IActionResult> SubirEditada([FromForm] SubirFotoRequest request)
        {
            if (request.Archivo == null || request.Archivo.Length == 0) return BadRequest("No se envió archivo");

            using var stream = request.Archivo.OpenReadStream();

            var dto = new FotoCargaDto
            {
                InmuebleId = request.InmuebleId,
                UsuarioId = request.UsuarioId,
                NombreArchivo = request.Archivo.FileName,
                ArchivoStream = stream
            };

            var result = await _subirEditadaInteractor.Handle(dto);
            return Ok(result);
        }

        // Endpoint para GRAFISTAS (Clasificar y Favoritas)
        [HttpPut("clasificar")]
        public async Task<IActionResult> Clasificar([FromBody] FotoUpdateDto dto)
        {
            try
            {
                await _gestionarInteractor.Handle(dto);
                return Ok("Foto actualizada correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint para GRAFISTAS (Ver que descargar)
        [HttpGet("pendientes/{inmuebleId}")]
        public async Task<IActionResult> GetPendientes(int inmuebleId)
        {
            var fotos = await _descargarInteractor.ObtenerRawsPorInmueble(inmuebleId);
            return Ok(fotos);
        }

        [HttpGet("img/{id}")]
        public async Task<IActionResult> GetImagen(int id, [FromServices] Domain.Interfaces.IFotoRepository fotoRepo)
        {
            try
            {
                var foto = await fotoRepo.GetByIdAsync(id);
                if (foto == null) return NotFound("La foto no existe en la base de datos.");

                if (string.IsNullOrEmpty(foto.RutaFisica)) return BadRequest("La ruta física está vacía en la BD.");

                if (!System.IO.File.Exists(foto.RutaFisica))
                    return NotFound($"El archivo no se encuentra en el disco: {foto.RutaFisica}");

                string extension = System.IO.Path.GetExtension(foto.RutaFisica).ToLower();
                string mimeType = "application/octet-stream";

                switch (extension)
                {
                    case ".jpg":
                    case ".jpeg": mimeType = "image/jpeg"; break;
                    case ".png": mimeType = "image/png"; break;
                    case ".gif": mimeType = "image/gif"; break;
                    case ".webp": mimeType = "image/webp"; break;
                }

                var image = System.IO.File.OpenRead(foto.RutaFisica);
                return File(image, mimeType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error Interno: {ex.Message}");
            }
        }

        [HttpGet("descargar-zip/{inmuebleId}")]
        public IActionResult DescargarZip(int inmuebleId, [FromServices] DescargarZipInteractor zipInteractor)
        {
            try
            {
                var archivoZip = zipInteractor.Handle(inmuebleId.ToString());
                return File(archivoZip, "application/zip", $"Inmueble_{inmuebleId}_Originales.zip");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}