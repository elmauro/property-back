using MC.PropertyService.API.Data.Models;
using System.Linq.Expressions;

namespace MC.PropertyService.API.ClientModels
{
    public class OwnerView
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public string Photo { get; set; } = string.Empty;

        public static Expression<Func<Owner, OwnerView>> Project()
        {
            return owner => new OwnerView
            {
                Name = owner.Name,
                Address = owner.Address,
                Photo = owner.Photo
            };
        }
    }

}
