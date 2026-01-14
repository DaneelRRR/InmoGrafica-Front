using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class FotoUpdateDto
    {
        public int Id { get; set; }
        public bool EsFavorita { get; set; }
        public int? AmbienteId { get; set; }
    }
}
