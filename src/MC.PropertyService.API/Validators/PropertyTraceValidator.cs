using FluentValidation;
using MC.PropertyService.API.ClientModels;

namespace MC.PropertyService.API.Validators
{
    /// <summary>
    /// Validates the data of PropertyTraceRequest to ensure all required fields are correctly filled.
    /// This helps prevent errors by checking the data before processing it further.
    /// </summary>
    public class PropertyTraceValidator : AbstractValidator<PropertyTraceRequest>
    {
        public const string DateSaleValidator = "Invalid sale date provided, sale date must be in the past.";
        public const string NameValidator = "Invalid property trace name provided, please provide a valid name.";
        public const string ValueValidator = "Invalid value provided, value must be greater than 0.";
        public const string TaxValidator = "Invalid tax value provided, tax must be a positive number.";

        public PropertyTraceValidator()
        {
            // Check if the 'DateSale' is in the past
            RuleFor(trace => trace.DateSale)
                .LessThanOrEqualTo(DateTime.Now).WithMessage(DateSaleValidator);

            // Check if the 'Name' field is not empty
            RuleFor(trace => trace.Name)
                .NotEmpty().WithMessage(NameValidator);

            // Check if the 'Value' field is greater than 0
            RuleFor(trace => trace.Value)
                .GreaterThan(0).WithMessage(ValueValidator);

            // Check if the 'Tax' field is a positive number (tax can be 0 or higher)
            RuleFor(trace => trace.Tax)
                .GreaterThanOrEqualTo(0).WithMessage(TaxValidator);
        }
    }
}
