using FluentValidation;
using MC.PropertyService.API.ClientModels;

namespace MC.PropertyService.API.Validators
{
    /// <summary>
    /// Validates the data of PropertyFilterRequest to ensure the filtering criteria are valid.
    /// </summary>
    public class PropertyFilterValidator : AbstractValidator<PropertyFilterRequest>
    {
        public const string PriceRangeValidator = "The minimum price must be less than or equal to the maximum price.";
        public const string YearRangeValidator = "The minimum year must be less than or equal to the maximum year.";
        public const string DateRangeValidator = "The 'CreatedAfter' date must be before the 'CreatedBefore' date.";
        public const string OwnerIdValidator = "Invalid owner ID provided.";
        public const string NameValidator = "Property name must not be empty if provided.";
        public const string AddressValidator = "Property address must not be empty if provided.";
        public const string CadastralNumberValidator = "Property cadastral number must not be empty if provided.";

        public PropertyFilterValidator()
        {
            // Check if MinPrice is less than or equal to MaxPrice
            RuleFor(request => request)
                .Must(HaveValidPriceRange)
                .WithMessage(PriceRangeValidator);

            // Check if MinYear is less than or equal to MaxYear
            RuleFor(request => request)
                .Must(HaveValidYearRange)
                .WithMessage(YearRangeValidator);

            // Check if CreatedAfter is less than or equal to CreatedBefore
            RuleFor(request => request)
                .Must(HaveValidDateRange)
                .WithMessage(DateRangeValidator);

            // Check if OwnerId is a valid GUID (assuming it's a GUID)
            RuleFor(request => request.OwnerId)
                .Must(IsValidGuid)
                .When(request => !string.IsNullOrEmpty(request.OwnerId))
                .WithMessage(OwnerIdValidator);

            // Validate if the Name is not empty when provided
            RuleFor(request => request.Name)
                .NotEmpty().When(request => !string.IsNullOrEmpty(request.Name))
                .WithMessage(NameValidator);

            // Validate if the Address is not empty when provided
            RuleFor(request => request.Address)
                .NotEmpty().When(request => !string.IsNullOrEmpty(request.Address))
                .WithMessage(AddressValidator);

            // Validate if the CadastralNumber is not empty when provided
            RuleFor(request => request.CodeInternal)
                .NotEmpty().When(request => !string.IsNullOrEmpty(request.CodeInternal))
                .WithMessage(CadastralNumberValidator);
        }

        /// <summary>
        /// Validates that MinPrice is less than or equal to MaxPrice.
        /// </summary>
        private bool HaveValidPriceRange(PropertyFilterRequest request)
        {
            return !(request.MinPrice.HasValue && request.MaxPrice.HasValue) || request.MinPrice <= request.MaxPrice;
        }

        /// <summary>
        /// Validates that MinYear is less than or equal to MaxYear.
        /// </summary>
        private bool HaveValidYearRange(PropertyFilterRequest request)
        {
            return !(request.MinYear.HasValue && request.MaxYear.HasValue) || request.MinYear <= request.MaxYear;
        }

        /// <summary>
        /// Validates that CreatedAfter is less than or equal to CreatedBefore.
        /// </summary>
        private bool HaveValidDateRange(PropertyFilterRequest request)
        {
            return !(request.CreatedAfter.HasValue && request.CreatedBefore.HasValue) || request.CreatedAfter <= request.CreatedBefore;
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
