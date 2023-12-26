using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MapsterMapper;

namespace Discount.Application.Handlers.Commands;

public class UpdateDiscoutCommandHandler
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public UpdateDiscoutCommandHandler(IMapper mapper, IDiscountRepository discountRepository)
    {
        _mapper = mapper;
        _discountRepository = discountRepository;
    }

    public async Task<CouponModel> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Coupon>(request);

        await _discountRepository.UpdateDiscount(entity);

        var couponModel = _mapper.Map<CouponModel>(entity);

        return couponModel;

    }
}
