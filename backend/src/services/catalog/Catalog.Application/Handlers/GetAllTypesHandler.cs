using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetAllTypesHandler : IRequestHandler<GetAllTypesQuery, IList<TypeResponse>>
{
    private readonly ITypeRepository _typeRepository;

    public GetAllTypesHandler(ITypeRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }

    public async Task<IList<TypeResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var types = await _typeRepository.GetAllTypes();
        var typeResponse = LazyMapper.Mapper.Map<IList<ProductType>, IList<TypeResponse>>(types.ToList());
        return typeResponse;
    }
}
