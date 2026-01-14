using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Inmuebles
{
    public class CrearInmuebleInteractor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;

        public CrearInmuebleInteractor(IUnitOfWork unitOfWork, IMapper mapper, IFileStorage fileStorage)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        public async Task<InmuebleDto> Handle(CrearInmuebleDto dto)
        {
            // 1. Convertir DTO a Entidad
            var inmueble = _mapper.Map<Inmueble>(dto);

            // 2. Guardar en Base de Datos
            await _unitOfWork.Inmuebles.AddAsync(inmueble);
            await _unitOfWork.SaveChangesAsync();

            // 3. Crear estructura de carpetas físicas en D:\ (100originals, 200ok)
            _fileStorage.CrearEstructuraCarpetas(inmueble.Id.ToString());

            // 4. Retornar resultado
            return _mapper.Map<InmuebleDto>(inmueble);
        }
    }
}
