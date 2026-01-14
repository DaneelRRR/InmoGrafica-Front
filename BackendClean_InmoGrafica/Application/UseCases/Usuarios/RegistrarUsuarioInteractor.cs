using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Usuarios
{
    public class RegistrarUsuarioInteractor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegistrarUsuarioInteractor(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UsuarioDto> Handle(RegisterDto dto)
        {
            // 1. Validar si ya existe
            if (await _unitOfWork.Usuarios.ExistsByEmailAsync(dto.Email))
            {
                throw new Exception("El email ya está registrado.");
            }

            // 2. Buscar el Rol (Fotografo, Grafista, etc.)
            var rol = await _unitOfWork.Roles.GetByNameAsync(dto.NombreRol);
            if (rol == null) throw new Exception($"El rol '{dto.NombreRol}' no existe.");

            // 3. Crear Entidad
            var usuario = _mapper.Map<Usuario>(dto);
            usuario.RolId = rol.Id;

            usuario.PasswordHash = dto.Password;

            // 4. Guardar
            await _unitOfWork.Usuarios.AddAsync(usuario);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UsuarioDto>(usuario);
        }
    }
}
