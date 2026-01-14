using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using System.IO;

namespace Infrastructure.Services
{
    public class LocalFileStorage : IFileStorage
    {
        // Ruta base fija en el disco D:
        private readonly string _basePath = @"D:\Bienes";

        public void CrearEstructuraCarpetas(string inmuebleId)
        {
            // Crea D:\Bienes\20001\100originals
            string pathOriginals = Path.Combine(_basePath, inmuebleId, "100originals");
            if (!Directory.Exists(pathOriginals)) Directory.CreateDirectory(pathOriginals);

            // Crea D:\Bienes\20001\200ok
            string pathOk = Path.Combine(_basePath, inmuebleId, "200ok");
            if (!Directory.Exists(pathOk)) Directory.CreateDirectory(pathOk);
        }

        public async Task<string> GuardarArchivoAsync(Stream archivoStream, string nombreArchivo, string inmuebleId, string carpetaTipo)
        {
            // carpetaTipo será "100originals" o "200ok"
            string carpetaDestino = Path.Combine(_basePath, inmuebleId, carpetaTipo);

            // Asegurarnos que la carpeta exista
            if (!Directory.Exists(carpetaDestino)) Directory.CreateDirectory(carpetaDestino);

            string rutaCompleta = Path.Combine(carpetaDestino, nombreArchivo);

            using (var fileStream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await archivoStream.CopyToAsync(fileStream);
            }

            return rutaCompleta;
        }

        public Task<Stream> ObtenerArchivoAsync(string rutaCompleta)
        {
            if (!File.Exists(rutaCompleta))
            {
                return Task.FromResult<Stream>(null);
            }

            // Abrimos el archivo para lectura
            var stream = new FileStream(rutaCompleta, FileMode.Open, FileAccess.Read);
            return Task.FromResult<Stream>(stream);
        }
    }
}
