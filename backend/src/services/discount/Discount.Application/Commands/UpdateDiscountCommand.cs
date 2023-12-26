using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands;

public record UpdateDiscountCommand(int Id, string ProductName, string Description, decimal Amount) : IRequest<CouponModel>;
