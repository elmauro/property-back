using FluentValidation;
using MC.PropertyService.API.ClientModels;

public class PriceValidator : AbstractValidator<PriceRequest>
{
    public PriceValidator()
    {
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("The price must be greater than 0.");
    }
}
