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

        // Response DTO → Domain (for sort)
        CreateMap<SalonResponseDto, Salon>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ParseGenreFromDisplay(src.Genre)))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        // Sort Request DTO → Domain
        CreateMap<SortRequestDto, SortOption>()
            .ForMember(dest => dest.Field, opt => opt.MapFrom(src => ParseSortField(src.Field)))
            .ForMember(dest => dest.Order, opt => opt.MapFrom(src => ParseSortOrder(src.Order)));
    }

    private static Genre ParseGenreFromDisplay(string? genre)
    {
        return genre switch
        {
            "ヘアサロン" => Genre.HairSalon,
            "ネイル" => Genre.Nail,
            "エステ" => Genre.Esthe,
            "リラク" => Genre.Relaxation,
            "アイビューティー" => Genre.EyeBeauty,
            _ => Genre.HairSalon
        };
    }

    private static SortField ParseSortField(string? field)
    {
        if (Enum.TryParse<SortField>(field, ignoreCase: true, out var result))
            return result;
        return SortField.Rating;
    }

    private static SortOrder ParseSortOrder(string? order)
    {
        if (Enum.TryParse<SortOrder>(order, ignoreCase: true, out var result))
            return result;
        return SortOrder.Descending;
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
