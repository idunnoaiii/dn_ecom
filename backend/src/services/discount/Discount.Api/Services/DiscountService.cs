using Discount.Application.Commands;
using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Api.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DiscountService> _logger;

    public DiscountService(IMediator mediator, ILogger<DiscountService> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var query = new GetDiscountQuery(request.ProductName);

        var result = await _mediator.Send(query);
        _logger.LogInformation("Discount is retrive for the product name: {ProductName}, and Amount : {Amount}", result.ProductName, result.Amount);
        return result;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var cmd = new CreateDiscountCommand
        (
            ProductName: request.Coupon.ProductName,
            Amount: (decimal)request.Coupon.Amount,
            Description: request.Coupon.Description
        );
        
        var result = await _mediator.Send(cmd);
        _logger.LogInformation("Discount is successfully created for the product: {ProductName}", result.ProductName);

        return result;
    }


    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {

        var cmd = new UpdateDiscountCommand
        (
            Id: request.Coupon.Id,
            ProductName: request.Coupon.ProductName,
            Amount: (decimal)request.Coupon.Amount,
            Description: request.Coupon.Description
        );
        
        var result = await _mediator.Send(cmd);
        _logger.LogInformation("Discount is successfully updated for the product: {ProductName}", result.ProductName);

        return result;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var cmd = new DeleteDiscountCommand(request.ProductName);
        
        var deleted = await _mediator.Send(cmd);
        _logger.LogInformation("Discount is successfully deleted for the product: {ProductName}", request.ProductName);

        return new DeleteDiscountResponse {
            Success = deleted
        };
    }

}
