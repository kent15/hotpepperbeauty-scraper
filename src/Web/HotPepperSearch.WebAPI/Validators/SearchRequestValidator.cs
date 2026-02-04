using FluentValidation;
using HotPepperSearch.Domain.Enums;
using HotPepperSearch.WebAPI.DTOs;

namespace HotPepperSearch.WebAPI.Validators;

public class SearchRequestValidator : AbstractValidator<SearchRequestDto>
{
    private static readonly string[] ValidGenres = Enum.GetNames<Genre>();

    public SearchRequestValidator()
    {
        RuleFor(x => x.Genre)
            .Must(BeValidGenreOrNull)
            .WithMessage($"ジャンルは次のいずれかを指定してください: {string.Join(", ", ValidGenres)}");

        RuleFor(x => x.Prefecture)
            .MaximumLength(20)
            .WithMessage("都道府県は20文字以内で入力してください");

        RuleFor(x => x.City)
            .MaximumLength(50)
            .WithMessage("市区町村は50文字以内で入力してください");

        RuleFor(x => x.MinPrice)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MinPrice.HasValue)
            .WithMessage("最低価格は0以上を指定してください");

        RuleFor(x => x.MaxPrice)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MaxPrice.HasValue)
            .WithMessage("最高価格は0以上を指定してください");

        RuleFor(x => x)
            .Must(x => !x.MinPrice.HasValue || !x.MaxPrice.HasValue || x.MinPrice <= x.MaxPrice)
            .WithMessage("最低価格は最高価格以下にしてください");

        RuleFor(x => x.MinRating)
            .InclusiveBetween(0, 5)
            .When(x => x.MinRating.HasValue)
            .WithMessage("最低評価は0から5の間で指定してください");
    }

    private static bool BeValidGenreOrNull(string? genre)
    {
        if (string.IsNullOrEmpty(genre))
            return true;

        return ValidGenres.Contains(genre, StringComparer.OrdinalIgnoreCase);
    }
}
