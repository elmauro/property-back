using MC.PropertyService.API.Data.Models;

namespace MC.PropertyService.API.ClientModels
{
    /// <summary>
    /// This class holds information about a property trace that someone wants to add or change in the system.
    /// </summary>
    public class PropertyTraceRequest
    {
        /// <see cref="PropertyTrace.DateSale"/>
        public DateTime DateSale { get; set; }

        /// <see cref="PropertyTrace.Name"/>
        public string Name { get; set; } = string.Empty;

        /// <see cref="PropertyTrace.Value"/>
        public decimal Value { get; set; }

        /// <see cref="PropertyTrace.Tax"/>
        public decimal Tax { get; set; }
    }
}
