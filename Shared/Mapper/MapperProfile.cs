

using AutoMapper;
using DataBase.Models;
using Shared.Dto;

namespace Shared.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ProductEntity, ProductDto>().ReverseMap();
            CreateMap<CategoryEntity, CategoryDto>().ReverseMap();
            CreateMap<StorageEntity, StorageDto>().ReverseMap();

        }


    }
}
