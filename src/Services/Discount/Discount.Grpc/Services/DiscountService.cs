using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public  override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
			.Coupons
			.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (coupon is null)
        {
            coupon =  new Coupon() {Amount = 0, Description =  "No Discount", Id = 0, ProductName =  "No Discount"};
        }

        logger.LogInformation("GetDiscount: {coupon}", coupon);        
        
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon is null"));
        dbContext.Add(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("CreateDiscount: {coupon}", coupon);
        
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon is null"));
        dbContext.Update(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("CreateDiscount: {coupon}", coupon);
        
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.Coupon.ProductName);
        
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found:"));
        dbContext.Remove(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("DeleteDiscount: {coupon}", coupon);
        
        return new DeleteDiscountResponse {Success = true};       
        
    }
}