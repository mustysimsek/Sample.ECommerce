using FluentValidation;
using Sample.ECommerce.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ECommerce.Application.Validation
{
    public class BasketItemValidator : AbstractValidator<BasketItemViewDto>
    {
        public BasketItemValidator()
        {
            RuleFor(x => x.ProductId).NotNull().WithMessage("ProductId field is required");
            
            RuleFor(x=>x.ProductQuantity).NotNull().WithMessage("ProductQuantity field is required")
                .GreaterThan(0).WithMessage("Quantity should be greater than 0")
                .LessThan(251).WithMessage("Quantity should be less than 251");
        }
    }
}
