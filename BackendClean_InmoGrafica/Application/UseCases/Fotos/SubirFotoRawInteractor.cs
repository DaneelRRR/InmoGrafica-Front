using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Fotos
{
    public class SubirFotoRawInteractor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorage _fileStorage;
        private readonly IMapper _mapper;

        public SubirFotoRawInteractor(IUnitOfWork unitOfWork, IFileStorage fileStorage, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileStorage = fileStorage;
            _mapper = mapper;
        }

        public async Task<FotoResponseDto> Handle(FotoCargaDto dto)
        {
            string inmuebleIdStr = dto.InmuebleId.ToString();

            // 1. Guardar en D:\Bienes\20001\100originals
            string rutaFisica = await _fileStorage.GuardarArchivoAsync(
                dto.ArchivoStream,
                dto.NombreArchivo,
                inmuebleIdStr,
                "100originals");

            // 2. Crear registro en BD
            var foto = new Foto
            {
                NombreArchivo = dto.NombreArchivo,
                RutaFisica = rutaFisica,
                EsEditada = false,
                EsFavorita = false,
                InmuebleId = dto.InmuebleId,
                UsuarioId = dto.UsuarioId,
                FechaCreacion = System.DateTime.Now
            };

            await _unitOfWork.Fotos.AddAsync(foto);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<FotoResponseDto>(foto);
        }
    }
}
