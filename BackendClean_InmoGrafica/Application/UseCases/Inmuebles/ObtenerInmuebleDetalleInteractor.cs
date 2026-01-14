using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.Inmuebles
{
    public class ObtenerInmuebleDetalleInteractor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObtenerInmuebleDetalleInteractor(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<InmuebleDetalleDto> Handle(int id)
        {
            var inmueble = await _unitOfWork.Inmuebles.GetByIdAsync(id);
            if (inmueble == null) throw new Exception("Inmueble no encontrado");

            return _mapper.Map<InmuebleDetalleDto>(inmueble);
        }
    }
}
