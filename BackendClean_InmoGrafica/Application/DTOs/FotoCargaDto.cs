using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Application.DTOs
{
    public class FotoCargaDto
    {
        public int InmuebleId { get; set; }
        public string NombreArchivo { get; set; }
        public Stream ArchivoStream { get; set; } // El contenido del archivo
        public int UsuarioId { get; set; }
    }
}
