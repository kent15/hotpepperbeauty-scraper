using AutoMapper;
using HotPepperSearch.Domain.Enums;
using HotPepperSearch.Domain.ValueObjects;
using HotPepperSearch.WebAPI.DTOs;

namespace HotPepperSearch.WebAPI.Mappings;

public class SearchMappingProfile : Profile
{
    public SearchMappingProfile()
    {
        CreateMap<SearchRequestDto, SearchCondition>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ParseGenre(src.Genre)));
    }

    private static Genre? ParseGenre(string? genre)
    {
        if (string.IsNullOrEmpty(genre))
            return null;

        if (Enum.TryParse<Genre>(genre, ignoreCase: true, out var result))
            return result;

        return null;
    }
}
