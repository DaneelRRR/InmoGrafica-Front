using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.Usuarios
{
    public class LoginInteractor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoginInteractor(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UsuarioDto> Handle(LoginDto dto)
        {
            var usuario = await _unitOfWork.Usuarios.GetByEmailAsync(dto.Email);

            if (usuario == null || usuario.PasswordHash != dto.Password)
            {
                return null;
            }

            return _mapper.Map<UsuarioDto>(usuario);
        }
    }
}