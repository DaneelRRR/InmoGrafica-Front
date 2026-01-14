using Microsoft.AspNetCore.Http;

namespace WebApi.Models
{
    public class SubirFotoRequest
    {
        public int InmuebleId { get; set; }
        public int UsuarioId { get; set; }
        public IFormFile Archivo { get; set; }
    }
}
