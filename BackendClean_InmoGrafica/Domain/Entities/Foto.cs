using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Foto : BaseEntity
    {
        public string NombreArchivo { get; set; }
        public string RutaFisica { get; set; }

        // Clasificacion
        public bool EsEditada { get; set; }
        public bool EsFavorita { get; set; }

        // Relaciones
        public int InmuebleId { get; set; }
        public Inmueble Inmueble { get; set; }

        public int? AmbienteId { get; set; }
        public Ambiente Ambiente { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
