using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Application.DTOs;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Usuarios
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Rol.Nombre));

            CreateMap<RegisterDto, Usuario>();
            // Inmuebles
            CreateMap<CrearInmuebleDto, Inmueble>();
            CreateMap<Inmueble, InmuebleDetalleDto>();
            CreateMap<Inmueble, InmuebleDto>()
                .ForMember(dest => dest.CantidadFotos, opt => opt.MapFrom(src => src.Fotos != null ? src.Fotos.Count : 0))
                // 1. Prioridad: Que el ambiente se llame "Fachada".
                // 2. Prioridad: Que sea Favorita.
                // 3. Prioridad: Que sea Editada.
                .ForMember(dest => dest.ImagenPortadaId, opt => opt.MapFrom(src =>
                    src.Fotos != null && src.Fotos.Count > 0
                        ? src.Fotos
                            .OrderByDescending(f => f.Ambiente != null && f.Ambiente.Nombre == "Fachada")
                            .ThenByDescending(f => f.EsFavorita)
                            .ThenByDescending(f => f.EsEditada)
                            .FirstOrDefault().Id
                        : (int?)null
                ));

            // Fotos
            CreateMap<Foto, FotoResponseDto>()
                .ForMember(dest => dest.NombreAmbiente, opt => opt.MapFrom(src => src.Ambiente != null ? src.Ambiente.Nombre : "Sin clasificar"))
                .ForMember(dest => dest.UrlImagen, opt => opt.MapFrom(src => src.NombreArchivo));

            // Ambientes
            CreateMap<Ambiente, AmbienteDto>();
        }
    }
}
