using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using System.IO;
using System.IO.Compression;

namespace Application.UseCases.Fotos
{
    public class DescargarZipInteractor
    {
        private readonly IFileStorage _fileStorage;

        public byte[] Handle(string inmuebleId)
        {
            // Ruta a la carpeta de originales en D:
            string rutaOriginales = Path.Combine(@"D:\Bienes", inmuebleId, "100originals");

            if (!Directory.Exists(rutaOriginales)) throw new FileNotFoundException("No hay fotos para este bien.");

            using (var memoryStream = new MemoryStream())
            {
                ZipFile.CreateFromDirectory(rutaOriginales, memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}