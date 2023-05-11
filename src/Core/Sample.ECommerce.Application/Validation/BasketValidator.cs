using FluentValidation;
using Sample.ECommerce.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ECommerce.Application.Validation
{
    public class BasketValidator : AbstractValidator<BasketViewDto>
    {
        public BasketValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id field is required. Exp:'54872','84697' ");
            RuleForEach(x => x.Items).SetValidator(new BasketItemValidator());
        }
    }
}
