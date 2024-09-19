using MC.PropertyService.API.Data.Models;

namespace MC.PropertyService.API.ClientModels
{
    /// <summary>
    /// This class holds information about an owner that someone wants to add or change in the system.
    /// </summary>
    public class OwnerRequest
    {
        /// <see cref="Owner.Name"/>
        public string Name { get; set; } = string.Empty;

        /// <see cref="Owner.Address"/>
        public string Address { get; set; } = string.Empty;

        /// <see cref="Owner.Photo"/>
        public string Photo { get; set; } = string.Empty;

        /// <see cref="Owner.Birthday"/>
        public DateTime Birthday { get; set; }
    }
}
