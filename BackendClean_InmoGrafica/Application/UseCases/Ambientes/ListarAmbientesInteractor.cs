using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.Ambientes
{
    public class ListarAmbientesInteractor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListarAmbientesInteractor(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AmbienteDto>> Handle()
        {
            var ambientes = await _unitOfWork.Ambientes.GetAllAsync();
            return _mapper.Map<IEnumerable<AmbienteDto>>(ambientes);
        }
    }
}