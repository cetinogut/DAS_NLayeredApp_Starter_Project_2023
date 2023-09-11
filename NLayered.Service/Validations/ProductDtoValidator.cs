using FluentValidation;
using NLayered.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayered.Service.Validations
{
    public class ProductDtoValidator : AbstractValidator<ProductCreateRequestDto>
    {
        // originalde buraya ProductDto'ya göre validator yazıldı. Ancak benim Product Psot'umda ProductCreateRequestDto olduğundan Validation Filter'i ona göre update etmemiz gerekliydi ve yukarıda ProductCreateRequestDto ekleyerek bunu yaptık, ayrıca aşağıda CategoryId için de RuleFor yazmak gerekli.
        public ProductDtoValidator()
        {

            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0");
            RuleFor(x => x.Stock).InclusiveBetween(1, 1000).WithMessage("{PropertyName} must be greater 0");

        }


    }
}
