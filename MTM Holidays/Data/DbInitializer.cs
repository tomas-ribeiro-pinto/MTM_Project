using MTM_Holidays.Models;

namespace MTM_Holidays.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Look for any DiscountCodes.
            if (context.DiscountCodes.Any())
            {
                return;   // DB has been seeded with discount codes
            }

            var discountCodes = new DiscountCode[]
            {
                new DiscountCode{Code="MTM", Discount=5.00},
                new DiscountCode{Code="CO550", Discount=10.50},
                new DiscountCode{Code="BNU2023", Discount=100.00}
            };

            context.DiscountCodes.AddRange(discountCodes);
            context.SaveChanges();
        }
    }
}