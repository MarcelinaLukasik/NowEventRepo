﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using WebApplication2.Models;

namespace NowEvent.Validators
{
    public class OfferQueryValidator : AbstractValidator<OfferQuery>
    {
        private readonly int[] allowedPageSizes = new[] { 15, 20 };
        private readonly string[] allowedSortByColumns = { nameof(Event.Name), nameof(Event.Size), nameof(Event.Type) };
        public OfferQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", allowedPageSizes)}]");
                }
            });
            RuleFor(r => r.SortBy).Must(value => string.IsNullOrEmpty(value) || allowedSortByColumns.Contains(value))
                .WithMessage($"Must be sort in [{string.Join(",", allowedSortByColumns)}]");
        }
    }
}