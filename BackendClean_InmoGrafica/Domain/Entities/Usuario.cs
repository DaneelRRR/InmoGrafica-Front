using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public int RolId { get; set; }
        public Rol Rol { get; set; }
    }
}