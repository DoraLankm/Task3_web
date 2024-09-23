using AutoMapper;
using GraphQL_lesson.Models;
using GraphQL_lesson.Models.Dto;

namespace GraphQL_lesson.Mapper
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
