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
    public class ListarInmueblesInteractor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListarInmueblesInteractor(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InmuebleDto>> Handle()
        {
            // Usamos el repositorio para traer todo
            var inmuebles = await _unitOfWork.Inmuebles.GetAllAsync();

            // Convertimos la lista de Entidades a DTOs
            return _mapper.Map<IEnumerable<InmuebleDto>>(inmuebles);
        }
    }
}