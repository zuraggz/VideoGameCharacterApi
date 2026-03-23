using AutoMapper;
using VideoGameCharacterApi.Dto;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Country, CountryDto>();
        }
    }
}
