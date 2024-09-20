using AutoMapper;
using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.Web.ViewModels;
using Shoes_EF_2024.Web.ViewModels.Brands;
using Shoes_EF_2024.Web.ViewModels.Colors;
using Shoes_EF_2024.Web.ViewModels.Genres;
using Shoes_EF_2024.Web.ViewModels.Shoes;
using Shoes_EF_2024.Web.ViewModels.ShoeSizes;
using Shoes_EF_2024.Web.ViewModels.Sports;

namespace Shoes_EF_2024.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            LoadShoesMapping();
            LoadBrandsMapping();
            LoadColorsMapping();
            LoadGenresMapping();
            LoadSportsMapping();
            LoadSizesMapping();
            LoadShoeSizeMapping();
        }

        private void LoadShoesMapping()
        {
            CreateMap<Shoes, ShoeListVm>()
                .ForMember(dest => dest.ShoeId, opt => opt.MapFrom(src => src.ShoeId))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand != null ? src.Brand.BrandName : string.Empty))
                .ForMember(dest => dest.SportName, opt => opt.MapFrom(src => src.Sport != null ? src.Sport.SportName : string.Empty))
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre != null ? src.Genre.GenreName : string.Empty))
                .ForMember(dest => dest.ColorName, opt => opt.MapFrom(src => src.Color != null ? src.Color.ColorName : string.Empty))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock))
                .ForMember(dest => dest.Suspended, opt => opt.MapFrom(src => src.Suspended));

            CreateMap<Shoes, ShoeEditVm>().ReverseMap();

            CreateMap<Shoes, ShoeDetailsVm>()
                .ForMember(dest => dest.ShoeId, opt => opt.MapFrom(src => src.ShoeId))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand != null ? src.Brand.BrandName : string.Empty))
                .ForMember(dest => dest.SportName, opt => opt.MapFrom(src => src.Sport != null ? src.Sport.SportName : string.Empty))
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre != null ? src.Genre.GenreName : string.Empty))
                .ForMember(dest => dest.ColorName, opt => opt.MapFrom(src => src.Color != null ? src.Color.ColorName : string.Empty))
                .ForMember(dest => dest.ShoeSizes, opt => opt.MapFrom(src => src.ShoeSizes.Select(ss => new ShoeSizeListVm
                {
                    SizeId = ss.SizeId,
                    ShoeId = ss.ShoeId,
                    QuantityInStock = ss.QuantityInStock
                }).ToList()))
                .ForMember(dest => dest.Suspended, opt => opt.MapFrom(src => src.Suspended))
                .ForMember(dest => dest.NumberOfSizes, opt => opt.MapFrom(src => src.ShoeSizes.Count));
        }


        private void LoadShoeSizeMapping()
        {
            CreateMap<ShoeSize, ShoeSizeListVm>()
                .ForMember(dest => dest.ShoeModel, opt => opt.MapFrom(src => src.Shoe.Model))
                .ForMember(dest => dest.SizeNumber, opt => opt.MapFrom(src => src.Size.SizeNumber))
                .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock));

            CreateMap<ShoeSize, ShoeSizeEditVm>().ReverseMap();
        }

        private void LoadBrandsMapping()
        {
            CreateMap<Brands, BrandListVm>()
                .ForMember(dest => dest.shoesQuantity, opt => opt.Ignore()); 

            CreateMap<Brands, BrandEditVm>().ReverseMap();

            CreateMap<Brands, BrandDetailsVm>()
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.BrandId))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.BrandName))
                .ForMember(dest => dest.ShoesQuantity, opt => opt.Ignore()); 
        }

        private void LoadColorsMapping()
        {
            CreateMap<Colors, ColorlistVm>()
                .ForMember(dest => dest.shoesQuantity, opt => opt.Ignore());

            CreateMap<Colors, ColorEditVm>().ReverseMap();

            CreateMap<Colors, ColorDetailsVm>()
                .ForMember(dest => dest.ColorId, opt => opt.MapFrom(src => src.ColorId))
                .ForMember(dest => dest.ColorName, opt => opt.MapFrom(src => src.ColorName))
                .ForMember(dest => dest.ShoesQuantity, opt => opt.Ignore());
        }

        private void LoadGenresMapping()
        {
            CreateMap<Genre, GenrelistVm>();
            CreateMap<Genre, GenreEditVm>().ReverseMap();
        }

        private void LoadSportsMapping()
        {
            CreateMap<Sports, SportListVm>()
                .ForMember(dest => dest.shoesQuantity, opt => opt.Ignore());
            CreateMap<Sports, SportEditVm>().ReverseMap();

            CreateMap<Sports, SportDetailsVm>()
                .ForMember(dest => dest.SportId, opt => opt.MapFrom(src => src.SportId))
                .ForMember(dest => dest.SportName, opt => opt.MapFrom(src => src.SportName))
                .ForMember(dest => dest.ShoesQuantity, opt => opt.Ignore());
        }

        private void LoadSizesMapping()
        {
            CreateMap<Sizes, SizeEditVm>()
                .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.SizeId))
                .ForMember(dest => dest.SizeNumber, opt => opt.MapFrom(src => src.SizeNumber))
                .ReverseMap();

            CreateMap<Sizes, SizeListVm>()
                .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.SizeId))
                .ForMember(dest => dest.SizeNumber, opt => opt.MapFrom(src => src.SizeNumber))
                .ReverseMap();
        }
    }
}

