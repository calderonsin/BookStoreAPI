using AutoMapper;
using BookStoreAPI.DTOS;
using BookStoreAPI.Models;

namespace BookStoreAPI
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Genre, GenreDTO>();
            CreateMap<GenreDTO, Genre>();

        }
    }
}
