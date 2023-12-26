using Discount.Core.Entities;
using Discount.Grpc.Protos;
using Mapster;

namespace Discount.Application.Mappers;

public class DiscountMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Coupon, CouponModel>().TwoWays();
    }
}
