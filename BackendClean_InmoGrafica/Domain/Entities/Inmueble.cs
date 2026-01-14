using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Inmueble : BaseEntity
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }

        public ICollection<Foto> Fotos { get; set; }
    }
}
