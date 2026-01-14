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
    public class SubirFotoEditadaInteractor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorage _fileStorage;
        private readonly IMapper _mapper;

        public SubirFotoEditadaInteractor(IUnitOfWork unitOfWork, IFileStorage fileStorage, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileStorage = fileStorage;
            _mapper = mapper;
        }

        public async Task<FotoResponseDto> Handle(FotoCargaDto dto)
        {
            string inmuebleIdStr = dto.InmuebleId.ToString();

            // 1. Guardar físico en D:\Bienes\20001\200ok
            string rutaFisica = await _fileStorage.GuardarArchivoAsync(
                dto.ArchivoStream,
                dto.NombreArchivo,
                inmuebleIdStr,
                "200ok");

            // 2. Crear registro en BD
            var foto = new Foto
            {
                NombreArchivo = dto.NombreArchivo,
                RutaFisica = rutaFisica,
                EsEditada = true,
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
