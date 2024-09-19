using FluentValidation;
using MC.PropertyService.API.ClientModels;

namespace MC.PropertyService.API.Validators
{
    /// <summary>
    /// Validates the data of PropertyRequest to ensure all required fields are correctly filled.
    /// This helps prevent errors by checking the data before processing it further.
    /// </summary>
    public class PropertyValidator : AbstractValidator<PropertyRequest>
    {
        public const string NameValidator = "Invalid property name provided, please provide a valid name.";
        public const string AddressValidator = "Invalid property address provided, please provide a valid address.";
        public const string PriceValidator = "Invalid property price provided, price must be greater than 0.";
        public const string YearValidator = "Invalid property year provided, year must be between 1800 and the current year.";
        public const string CodeInternalValidator = "Invalid property code internal number provided, please provide a valid code internal number.";
        public const string OwnerIdValidator = "Invalid owner ID provided, owner ID must be a valid GUID.";

        public PropertyValidator()
        {
            // Check if the 'Name' field is not empty.
            RuleFor(property => property.Name)
                .NotEmpty().WithMessage(NameValidator);

            // Check if the 'Address' field is not empty.
            RuleFor(property => property.Address)
                .NotEmpty().WithMessage(AddressValidator);

            // Check if the 'Price' field is greater than 0.
            RuleFor(property => property.Price)
                .GreaterThan(0).WithMessage(PriceValidator);

            // Check if the 'Year' field is between 1800 and the current year.
            RuleFor(property => property.Year)
                .InclusiveBetween(1800, DateTime.Now.Year).WithMessage(YearValidator);

            // Check if the 'CodeInternal' field is not empty.
            RuleFor(property => property.CodeInternal)
                .NotEmpty().WithMessage(CodeInternalValidator);

            // Check if the 'OwnerId' is a valid GUID.
            RuleFor(property => property.OwnerId)
                .Must(IsValidGuid).WithMessage(OwnerIdValidator);
        }

        /// <summary>
        /// Validates that a given string is a valid GUID.
        /// </summary>
        private bool IsValidGuid(string ownerId)
        {
            return Guid.TryParse(ownerId, out _);
        }
    }
}
