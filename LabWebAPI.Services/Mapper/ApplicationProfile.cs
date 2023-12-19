using AutoMapper;
using LabWebAPI.Contracts.Data.Entities;
using LabWebAPI.Contracts.DTO.AdminPanel;
using LabWebAPI.Contracts.DTO.Authentications;
using LabWebAPI.Contracts.DTO.Product;

namespace LabWebAPI.Services.Mapper
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<UserRegistrationDTO, User>();
            CreateMap<UserInfoDTO, User>();
            CreateMap<User, UserInfoDTO>();
            CreateMap<User, SimpleUserInfoDTO>();
            CreateMap<Product, ProductDTO>()
    .ForMember(dest => dest.UserWhoCreated, opt => opt.MapFrom(src => src.UserWhoCreated));



            CreateMap<User, UserWithProductsDTO>()  // Додали новий мапінг для користувацького DTO, що включає список продуктів
          .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            CreateMap<ProductDTO, Product>()  // Мапінг для створення продукту
                .ForMember(dest => dest.UserWhoCreatedId, opt => opt.Ignore()); // Ігноруємо поле, так як воно буде встановлено під час обробки у сервісі

        }
    }
}