
using Application.Mapping;
using Application.UseCases.Fotos;
using Application.UseCases.Inmuebles;
using Application.UseCases.Usuarios;
using Application.UseCases.Ambientes;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowWebApp",
                    policy => policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            // 1. Configurar Base de Datos
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // 2. Inyección de Dependencias (Capas)
            // Repositorios y UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Servicio de Disco D:
            builder.Services.AddScoped<IFileStorage, LocalFileStorage>();
            builder.Services.AddScoped<Domain.Interfaces.IFotoRepository, Infrastructure.Repositories.FotoRepository>();

            // 3. Inyección de Casos de Uso (Interactors)
            builder.Services.AddScoped<RegistrarUsuarioInteractor>();
            builder.Services.AddScoped<LoginInteractor>();
            builder.Services.AddScoped<CrearInmuebleInteractor>();
            builder.Services.AddScoped<ListarInmueblesInteractor>();
            builder.Services.AddScoped<ObtenerInmuebleDetalleInteractor>();
            builder.Services.AddScoped<SubirFotoRawInteractor>();
            builder.Services.AddScoped<SubirFotoEditadaInteractor>();
            builder.Services.AddScoped<GestionarFotoInteractor>();
            builder.Services.AddScoped<DescargarFotosInteractor>();
            builder.Services.AddScoped<DescargarZipInteractor>();
            builder.Services.AddScoped<ListarAmbientesInteractor>();

            // 4. Configurar AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            // 5. Configurar Controladores y Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); 

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowWebApp");
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
