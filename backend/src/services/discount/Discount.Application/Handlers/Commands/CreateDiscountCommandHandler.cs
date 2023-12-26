using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MapsterMapper;
using MediatR;

namespace Discount.Application.Handlers.Commands;

public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, CouponModel>
{

    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public CreateDiscountCommandHandler(IMapper mapper, IDiscountRepository discountRepository)
    {
        _mapper = mapper;
        _discountRepository = discountRepository;
    }

    public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Coupon>(request);

        await _discountRepository.CreateDiscount(entity);

        var couponModel = _mapper.Map<CouponModel>(entity);

        return couponModel;

    }
}
