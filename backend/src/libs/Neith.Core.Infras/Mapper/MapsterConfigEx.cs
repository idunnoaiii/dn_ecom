using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Neith.Core.Infras.Mapper;

public static class MapsterConfigEx
{
    public static WebApplicationBuilder AddMapster(this WebApplicationBuilder builder)

    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        builder.Services.AddSingleton(config);
        builder.Services.AddScoped<IMapper, ServiceMapper>();
        return builder;
    }
}
