using AutoMapper;
using MoviesApi.DTOS;
using MoviesApi.Entities;

namespace MoviesApi.Helper
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<GenreDTO, Genre>().ReverseMap();
            CreateMap<GenreCreationDTO, Genre>().ReverseMap();
        }
    }
}
