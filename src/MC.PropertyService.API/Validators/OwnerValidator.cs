using FluentValidation;
using MC.PropertyService.API.ClientModels;

namespace MC.PropertyService.API.Validators
{
    /// <summary>
    /// Validates the data of OwnerRequest to ensure all required fields are correctly filled.
    /// This helps prevent errors by checking the data before processing it further.
    /// </summary>
    public class OwnerValidator : AbstractValidator<OwnerRequest>
    {
        public const string OwnerNameValidator = "Invalid owner name provided, please provide a valid name.";
        public const string OwnerAddressValidator = "Invalid owner address provided, please provide a valid address.";
        public const string OwnerPhotoValidator = "Invalid photo URL provided, please provide a valid photo URL.";
        public const string OwnerBirthdayValidator = "Invalid birthday provided, birthday must be a past date.";

        public OwnerValidator()
        {
            // Checks if the 'Name' field of the owner is not empty.
            RuleFor(owner => owner.Name)
                .NotEmpty().WithMessage(OwnerNameValidator);

            // Checks if the 'Address' field of the owner is not empty.
            RuleFor(owner => owner.Address)
                .NotEmpty().WithMessage(OwnerAddressValidator);

            // Checks if the 'Photo' field of the owner is a valid URL (assuming it's a URL for the photo).
            RuleFor(owner => owner.Photo)
                .NotEmpty().WithMessage(OwnerPhotoValidator)
                .Must(IsValidUrl).WithMessage(OwnerPhotoValidator);

            // Checks if the 'Birthday' field of the owner is a valid date in the past.
            RuleFor(owner => owner.Birthday)
                .LessThan(DateTime.Now).WithMessage(OwnerBirthdayValidator);
        }

        /// <summary>
        /// Validates that a given string is a valid URL.
        /// </summary>
        /// <param name="photoUrl">The URL to validate.</param>
        /// <returns>True if the URL is valid, otherwise false.</returns>
        private bool IsValidUrl(string photoUrl)
        {
            return Uri.TryCreate(photoUrl, UriKind.Absolute, out _);
        }
    }
}
