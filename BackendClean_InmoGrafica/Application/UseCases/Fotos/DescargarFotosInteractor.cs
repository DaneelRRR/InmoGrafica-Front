using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.Fotos
{
    public class DescargarFotosInteractor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DescargarFotosInteractor(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FotoResponseDto>> ObtenerRawsPorInmueble(int inmuebleId)
        {
            // Trae solo las NO editadas (RAWs)
            var fotos = await _unitOfWork.Fotos.GetByInmuebleYEstadoAsync(inmuebleId, esEditada: false);
            return _mapper.Map<IEnumerable<FotoResponseDto>>(fotos);
        }
    }
}
