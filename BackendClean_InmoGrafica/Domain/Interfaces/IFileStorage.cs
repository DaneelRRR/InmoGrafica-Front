using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Domain.Interfaces
{
    public interface IFileStorage
    {
        Task<string> GuardarArchivoAsync(Stream archivoStream, string nombreArchivo, string inmuebleId, string carpetaTipo);

        Task<Stream> ObtenerArchivoAsync(string rutaCompleta);

        void CrearEstructuraCarpetas(string inmuebleId);
    }
}
