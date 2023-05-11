using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Sample.ECommerce.Application.Interfaces;
using Sample.ECommerce.Application.Services.Abstracts;
using Sample.ECommerce.Application.Services.Concretes;
using Sample.ECommerce.Application.Services.Dto;
using Sample.ECommerce.Application.Validation;
using Sample.ECommerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ECommerce.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IProductService, ProductService>();
            serviceCollection.AddTransient<IBasketService, BasketService>();

            serviceCollection.AddTransient<IValidator<BasketViewDto>, BasketValidator>();
            serviceCollection.AddTransient<IValidator<BasketItemViewDto>, BasketItemValidator>();
        }
    }
}
