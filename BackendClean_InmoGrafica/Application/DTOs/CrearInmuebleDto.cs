using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class CrearInmuebleDto
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Direccion { get; set; }
    }
}
