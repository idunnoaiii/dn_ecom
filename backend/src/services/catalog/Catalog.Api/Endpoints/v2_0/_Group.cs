namespace Catalog.Api.Endpoints.V2_0;

public static class Group
{

    public static RouteGroupBuilder GroupVersion2(this RouteGroupBuilder group)
    {
        group.AddBrandRoutes();
        return group;
    }
    // public static RouteGroupBuilder GroupVersion2(this RouteGroupBuilder group)
    // {
    //     group.MapGet("/version", () => $"Hello version 2");
    //     group.MapGet("/version2only", () => $"Hello version 2 only");
    //     return group;
    // }
}
