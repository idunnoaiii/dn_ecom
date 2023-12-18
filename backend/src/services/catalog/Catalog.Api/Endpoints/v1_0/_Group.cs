namespace Catalog.Api.Endpoints.V1_0;

public static class Group
{

    public static RouteGroupBuilder GroupVersion1(this RouteGroupBuilder group)
    {
        group.AddBrandRoutes();
        group.AddProductRoutes();
        group.AddTypeRoutes();
        return group;
    }
    // public static RouteGroupBuilder GroupVersion2(this RouteGroupBuilder group)
    // {
    //     group.MapGet("/version", () => $"Hello version 2");
    //     group.MapGet("/version2only", () => $"Hello version 2 only");
    //     return group;
    // }
}
