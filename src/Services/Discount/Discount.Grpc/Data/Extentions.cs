using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;


    public static class Extensions
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder builder)
        {
            using var scope = builder.ApplicationServices.CreateAsyncScope();
        
            using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
        
            dbContext.Database.MigrateAsync();
        
            return builder;
        }
    }
