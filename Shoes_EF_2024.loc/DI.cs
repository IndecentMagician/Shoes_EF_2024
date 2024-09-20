using Shoes_EF_2024.Datos.Interfaces;
using Shoes_EF_2024.Datos;
using Shoes_EF_2024.Datos.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shoes_EF_2024.Servicios.Interfaces;
using Shoes_EF_2024.Servicios.Services;
using Shoes_EF_2024.Servicios.Servicios;
using Shoes_EF_2024.Servicios;


namespace Shoes_EF_2024.loc
{
    public static class DI
    {
        public static void ConfigurarServicios(IServiceCollection servicios, IConfiguration configuration)
        {
            

            servicios.AddScoped<IBrandsRepo, BrandsRepo>();
            servicios.AddScoped<IGenreRepo, GenreRepo>();
            servicios.AddScoped<ISportsRepo, SportsRepo>();
            servicios.AddScoped<IColorsRepo, ColorsRepo>();
            servicios.AddScoped<IRepositoryShoe, RepositoryShoe>();
            servicios.AddScoped<ISizeRepo, SizesRepo>();
            servicios.AddScoped<IShoeSizeRepo, ShoesSizeRepo>();




            servicios.AddScoped<IServiceBrands, ServiceBrands>();
            servicios.AddScoped<IServiceGenres, ServiceGenres>();
            servicios.AddScoped<IServiceSports, ServiceSports>();
            servicios.AddScoped<IServiceColors, ServiceColors>();
            servicios.AddScoped<IServiceShoes, ServiceShoes>();
            servicios.AddScoped<IServiceShoeSize, ServiceShoeSize>();

            servicios.AddScoped<IServiceSizes, ServiceSizes>();

            servicios.AddScoped<IUnitOfWork, UnitOfWork>();

            servicios.AddDbContext<ShoesDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MyConnection"));
            }); ;


        }

        public static async Task ConfigurarServiciosAsync(IServiceCollection servicios, IConfiguration configuration)
        {
            servicios.AddScoped<IBrandsRepo, BrandsRepo>();
            servicios.AddScoped<IGenreRepo, GenreRepo>();
            servicios.AddScoped<ISportsRepo, SportsRepo>();
            servicios.AddScoped<IColorsRepo, ColorsRepo>();
            servicios.AddScoped<IRepositoryShoe, RepositoryShoe>();
            servicios.AddScoped<ISizeRepo, SizesRepo>();
            servicios.AddScoped<IShoeSizeRepo, ShoesSizeRepo>();
           
            servicios.AddScoped<IServiceBrands, ServiceBrands>();
            servicios.AddScoped<IServiceGenres, ServiceGenres>();
            servicios.AddScoped<IServiceSports, ServiceSports>();
            servicios.AddScoped<IServiceColors, ServiceColors>();
            servicios.AddScoped<IServiceShoes, ServiceShoes>();
            servicios.AddScoped<IServiceSizes, ServiceSizes>();
            servicios.AddScoped<IServiceShoeSize, ServiceShoeSize>();

            servicios.AddScoped<IUnitOfWork, UnitOfWork>();

            await Task.Run(() =>
            {
                servicios.AddDbContext<ShoesDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("MyConnection"));
                });
            });
        }

    }
}
