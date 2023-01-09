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
                new DiscountCode{ID=0, Code="0", Discount=0.00},
                new DiscountCode{Code="MTM", Discount=5.00},
                new DiscountCode{Code="CO550", Discount=10.50},
                new DiscountCode{Code="BNU2023", Discount=100.00}
            };

            var customers = new Customer[]
            {
                new Customer{ FirstName="John", LastName="Apple", EmailAddress="johnapple@gmail.com", DateOfBirth = DateTime.Parse("18/02/1998"), PhoneNumber="07723429485",
                    Address= new Address{Street="House of Commons", Town="Westminster", County="London", Country="England", PostCode="SW1A 0RS"}, CardPaymentID=1 }
            };

            var cardPayments = new CardPayment[]
            {
                new CardPayment{ID=1, CardNumber="0190456712348907", ExpiryDate=DateTime.Parse("01/2025"), SecurityCode="012"}
            };

            context.DiscountCodes.AddRange(discountCodes);
            context.Customers.AddRange(customers);
            context.CardPayments.AddRange(cardPayments);
            context.SaveChanges();
        }
    }
}