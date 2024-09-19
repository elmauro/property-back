using MC.PropertyService.API.Data.Models;
using System.Linq.Expressions;

namespace MC.PropertyService.API.ClientModels
{
    public class PropertyImageView
    {
        public string File { get; set; } = string.Empty;
        public int Enabled { get; set; }

        public static Expression<Func<PropertyImage, PropertyImageView>> Project()
        {
            return propertyImage => new PropertyImageView
            {
                File = propertyImage.File,
                Enabled = propertyImage.Enabled
            };
        }
    }

}
