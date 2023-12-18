using System.Reflection;

namespace Catalog.Api.Model.GetAllProduct;


public class GetAllProductRequest
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string? BrandId { get; set; }
    public string? TypeId { get; set; }
    public string? Sort { get; set; }
    public string? Search { get; set; }

    public static ValueTask<GetAllProductRequest?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        var brandId = context.Request.Query[nameof(BrandId)];
        var typeId = context.Request.Query[nameof(TypeId)];
        var sort = context.Request.Query[nameof(Sort)];
        var search = context.Request.Query[nameof(Search)];

        _ = int.TryParse(context.Request.Query[nameof(PageIndex)], out var pageIndex);
        _ = int.TryParse(context.Request.Query[nameof(PageSize)], out var pageSize);

        var result = new GetAllProductRequest
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            BrandId = brandId,
            TypeId = typeId,
            Sort = sort,
            Search = search
        };
        return ValueTask.FromResult(result)!;

    }
}
