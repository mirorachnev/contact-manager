using AutoMapper;
using ContactManager.Api.Contracts;
using ContactManager.MessageBus.Messages.DataTypes;

namespace ContactManager.Api.Utilities
{
    /// <summary>
    /// Auto mapper
    /// </summary>
    internal static class AutoMapper
    {
        private static IMapper _autoMapper;

        /// <summary>
        /// Static constructor that creates mapper instance internally.
        /// </summary>
        static AutoMapper()
        {
            _autoMapper = CreateMapper();
        }

        /// <summary>
        /// Gets mapper insatnce for type mapping.
        /// </summary>
        public static IMapper GetMapper()
        {
            return _autoMapper;
        }

        /// <summary>
        /// Creates mapper for type mapping.
        /// </summary>
        private static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;

                cfg.CreateMap<Contact, ContactData>().ReverseMap();
            });

            _autoMapper = config.CreateMapper();
            return _autoMapper;
        }
    }
}
