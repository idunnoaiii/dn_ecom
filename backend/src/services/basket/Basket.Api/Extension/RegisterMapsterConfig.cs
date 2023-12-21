using System.Reflection;
using Mapster;
using MapsterMapper;

namespace Basket.Api.Extension;

public static class AddRegisterMapsterConfigEx
{
    public static IServiceCollection AddMapping(this WebApplicationBuilder app)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        app.Services.AddSingleton(config);
        app.Services.AddScoped<IMapper, ServiceMapper>();
        return app.Services;
    }
}