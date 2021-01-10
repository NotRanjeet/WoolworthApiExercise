using FluentValidation;
using Woolworth.Application.Trolley.Models;
//Example utilizing the Fluent Validation to Validate Commands Queries
//Used by the Mediator Behaviour pipeline
namespace Woolworth.Application.Trolley.Queries
{
    public class GetTrolleyTotalQueryValidator : AbstractValidator<GetTrolleyTotalQuery>
    {
        public GetTrolleyTotalQueryValidator()
        {
            RuleFor(v => v.Trolley).NotNull().SetValidator(new TrolleyValidator());
        }


    }

    public class TrolleyValidator : AbstractValidator<TrolleyDto>
    {
        public TrolleyValidator()
        {
            RuleForEach(v => v.Products).NotNull().NotEmpty().SetValidator(new TrolleyProductValidator());
            RuleForEach(v => v.Quantities).NotNull().NotEmpty().SetValidator(new TrolleyQuantityValidator());
            RuleForEach(v => v.Specials).NotNull().SetValidator(new TrolleySpecialValidator());
        }
    }

    public class TrolleySpecialValidator : AbstractValidator<TrolleySpecialDto>
    {
        public TrolleySpecialValidator()
        {
            RuleForEach(v => v.Quantities).NotNull().NotNull().SetValidator(new TrolleyQuantityValidator());
            //Assuming:We cannot give something for free even on special
            RuleFor(v => v.Total).GreaterThan(0); 
        }
    }

    public class TrolleyProductValidator : AbstractValidator<TrolleyProductDto>
    {
        public TrolleyProductValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(1000);//Putting some arbitrary upper limit for validation.

            RuleFor(v => v.Price)
                .GreaterThanOrEqualTo(0);//Assuming price cannot be a negative value.
        }
    }

    public class TrolleyQuantityValidator : AbstractValidator<TrolleyQuantityDto>
    {
        public TrolleyQuantityValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(1000)
                .MinimumLength(2);

            RuleFor(v => v.Quantity)
                .GreaterThanOrEqualTo(1);//For specials the Quantity should always be greater than 0

        }
    }
}
