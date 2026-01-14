using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class FotoResponseDto
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; }
        public string UrlImagen { get; set; }
        public bool EsEditada { get; set; }
        public bool EsFavorita { get; set; }
        public string NombreAmbiente { get; set; }
    }
}
