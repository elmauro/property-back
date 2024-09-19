using MC.PropertyService.API.Data.Models;

namespace MC.PropertyService.API.ClientModels
{
    /// <summary>
    /// This class holds information about a property image that someone wants to add or change in the system.
    /// </summary>
    public class PropertyImageRequest
    {
        /// <see cref="PropertyImage.File"/>
        public string File { get; set; } = string.Empty;

        /// <see cref="PropertyImage.Enabled"/>
        public int Enabled { get; set; } = 1;
    }
}
