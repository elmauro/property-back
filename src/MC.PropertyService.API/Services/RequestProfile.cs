using AutoMapper;
using MC.PropertyService.API.ClientModels;
using MC.PropertyService.API.Data.Models;

namespace MC.PropertyService.API.Services
{
    /// <summary>
    /// Configures mappings for the property data transfer object to the property entity model.
    /// This mapping configuration is used by AutoMapper to convert between <see cref="PropertyRequest"/> DTOs
    /// and <see cref="Property"/> entities, ensuring that data is transferred correctly and efficiently
    /// between different layers of the application.
    /// </summary>
    public class PropertyRequestProfile : Profile
    {
        public PropertyRequestProfile()
        {
            CreateMap<PropertyRequest, Property>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.CodeInternal, opt => opt.MapFrom(src => src.CodeInternal))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId));
        }
    }

    /// <summary>
    /// Defines an AutoMapper profile for mapping between different instances of the <see cref="Property"/> class.
    /// This configuration is typically used for cloning objects or applying updates to existing objects without
    /// affecting certain system-managed properties like timestamps and record statuses.
    /// </summary>
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<Property, Property>()
                .ForMember(dest => dest.PropertyId, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.CodeInternal, opt => opt.MapFrom(src => src.CodeInternal))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedAt, opt => opt.Ignore());
        }
    }

    /// <summary>
    /// Configures mappings for the property image data transfer object to the property image entity model.
    /// This mapping configuration is used by AutoMapper to convert between <see cref="PropertyImageRequest"/> DTOs
    /// and <see cref="PropertyImage"/> entities, ensuring that data is transferred correctly and efficiently
    /// between different layers of the application.
    /// </summary>
    public class PropertyImageRequestProfile : Profile
    {
        public PropertyImageRequestProfile()
        {
            CreateMap<PropertyImageRequest, PropertyImage>()
                .ForMember(dest => dest.File, opt => opt.MapFrom(src => src.File))
                .ForMember(dest => dest.Enabled, opt => opt.MapFrom(src => src.Enabled));
        }
    }

    /// <summary>
    /// Defines an AutoMapper profile for mapping between different instances of the <see cref="PropertyImage"/> class.
    /// This configuration is typically used for cloning objects or applying updates to existing objects without
    /// affecting certain system-managed properties like timestamps and record statuses.
    /// </summary>
    public class PropertyImageProfile : Profile
    {
        public PropertyImageProfile()
        {
            CreateMap<PropertyImage, PropertyImage>()
                .ForMember(dest => dest.PropertyImageId, opt => opt.Ignore())
                .ForMember(dest => dest.File, opt => opt.MapFrom(src => src.File))
                .ForMember(dest => dest.Enabled, opt => opt.MapFrom(src => src.Enabled))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedAt, opt => opt.Ignore());
        }
    }
}
