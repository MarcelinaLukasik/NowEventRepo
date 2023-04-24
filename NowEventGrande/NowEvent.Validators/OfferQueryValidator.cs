using FluentValidation;
using NowEvent.Models;

namespace NowEvent.Validators
{
    public class OfferQueryValidator : AbstractValidator<OfferQuery>
    {
        private readonly int[] _allowedPageSizes = { 15, 20 };
        private readonly string[] _allowedSortByColumns = { nameof(Event.Name), nameof(Event.Size), nameof(Event.Type) };
        public OfferQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (_allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", _allowedPageSizes)}]");
                }
            });
            RuleFor(r => r.SortBy).Must(value => string.IsNullOrEmpty(value) || _allowedSortByColumns.Contains(value))
                .WithMessage($"Must be sort in [{string.Join(",", _allowedSortByColumns)}]");
        }
    }
}
