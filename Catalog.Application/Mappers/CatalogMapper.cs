

using AutoMapper;

namespace Catalog.Application.Mappers
{
    public static class CatalogMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<CatalogMappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => lazy.Value;
    }
}
