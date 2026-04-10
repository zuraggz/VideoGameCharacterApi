using AutoMapper;
using VideoGameCharacterApi.Dto;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Owner, OwnerDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Reviewer, ReviewerDto>().ReverseMap();
        }
    }
}
