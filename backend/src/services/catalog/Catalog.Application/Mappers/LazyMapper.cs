using AutoMapper;

namespace Catalog.Application.Mappers;

public static class LazyMapper
{
    private static Lazy<IMapper> lazyMapper = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = p => p.GetMethod!.IsPublic is true || p.GetMethod.IsAssembly;
            cfg.AddProfile<ProductMapperProfile>();
        });

        var mapper = config.CreateMapper();
        return mapper;
    });
    

    public static IMapper Mapper => lazyMapper.Value;
}
