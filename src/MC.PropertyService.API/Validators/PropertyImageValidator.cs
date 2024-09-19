using FluentValidation;
using MC.PropertyService.API.ClientModels;

namespace MC.PropertyService.API.Validators
{
    /// <summary>
    /// Validates the data of PropertyImageRequest to ensure all required fields are correctly filled.
    /// This helps prevent errors by checking the data before processing it further.
    /// </summary>
    public class PropertyImageValidator : AbstractValidator<PropertyImageRequest>
    {
        public const string FileValidator = "Invalid image file provided, please provide a valid image file.";
        public const string EnabledValidator = "Invalid enabled status provided, status must be either 0 (disabled) or 1 (enabled).";

        public PropertyImageValidator()
        {
            // Check if the 'File' field is not empty and must represent a valid file name (you can customize this validation).
            RuleFor(image => image.File)
                .NotEmpty().WithMessage(FileValidator)
                .Must(IsValidFileName).WithMessage(FileValidator);

            // Check if the 'Enabled' field is either 0 or 1.
            RuleFor(image => image.Enabled)
                .InclusiveBetween(0, 1).WithMessage(EnabledValidator);
        }

        /// <summary>
        /// Validates that a given string is a valid file name.
        /// This method assumes the file name should have a valid extension such as .jpg, .png, etc.
        /// </summary>
        /// <param name="fileName">The file name to validate.</param>
        /// <returns>True if the file name is valid, otherwise false.</returns>
        private bool IsValidFileName(string fileName)
        {
            // Check if the file name ends with a valid image extension
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            return validExtensions.Any(ext => fileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }
    }
}
