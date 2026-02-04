using AutoMapper;
using HotPepperSearch.Domain.Entities;
using HotPepperSearch.Domain.Enums;
using HotPepperSearch.Domain.ValueObjects;
using HotPepperSearch.WebAPI.DTOs;

namespace HotPepperSearch.WebAPI.Mappings;

public class SearchMappingProfile : Profile
{
    public SearchMappingProfile()
    {
        // Request DTO → Domain
        CreateMap<SearchRequestDto, SearchCondition>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ParseGenre(src.Genre)));

        // Domain → Response DTO
        CreateMap<Salon, SalonResponseDto>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => FormatGenre(src.Genre)))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => FormatRating(src.Rating)))
            .ForMember(dest => dest.PriceRange, opt => opt.MapFrom(src => NormalizeString(src.PriceRange)))
            .ForMember(dest => dest.Area, opt => opt.MapFrom(src => NormalizeString(src.Area)))
            .ForMember(dest => dest.NearestStation, opt => opt.MapFrom(src => NormalizeString(src.NearestStation)));
    }

    private static Genre? ParseGenre(string? genre)
    {
        if (string.IsNullOrEmpty(genre))
            return null;

        if (Enum.TryParse<Genre>(genre, ignoreCase: true, out var result))
            return result;

        return null;
    }

    private static string FormatGenre(Genre genre)
    {
        return genre switch
        {
            Genre.HairSalon => "ヘアサロン",
            Genre.Nail => "ネイル",
            Genre.Esthe => "エステ",
            Genre.Relaxation => "リラク",
            Genre.EyeBeauty => "アイビューティー",
            _ => genre.ToString()
        };
    }

    private static decimal? FormatRating(decimal? rating)
    {
        if (!rating.HasValue)
            return null;

        return Math.Round(rating.Value, 2);
    }

    private static string? NormalizeString(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        return value.Trim();
    }
}
