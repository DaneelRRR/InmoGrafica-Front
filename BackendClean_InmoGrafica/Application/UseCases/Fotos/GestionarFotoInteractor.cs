using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Interfaces;

namespace Application.UseCases.Fotos
{
    public class GestionarFotoInteractor
    {
        private readonly IUnitOfWork _unitOfWork;

        public GestionarFotoInteractor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(FotoUpdateDto dto)
        {
            var foto = await _unitOfWork.Fotos.GetByIdAsync(dto.Id);
            if (foto == null) throw new Exception("Foto no encontrada");

            // Solo permitimos clasificar fotos que YA son editadas
            if (!foto.EsEditada) throw new Exception("Solo se pueden clasificar fotos editadas (200ok).");

            foto.EsFavorita = dto.EsFavorita;

            if (dto.AmbienteId.HasValue)
            {
                foto.AmbienteId = dto.AmbienteId.Value;
            }

            _unitOfWork.Fotos.Update(foto);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
