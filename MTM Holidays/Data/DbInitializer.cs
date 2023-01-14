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
                new DiscountCode{ID=1, Code="MTM", Discount=5.00},
                new DiscountCode{ID=2, Code="CO550", Discount=10.50},
                new DiscountCode{ID=3, Code="BNU2023", Discount=100.00}
            };

            var addresses = new Address[]
            {
                new Address{ID=1, Town = "London", Country="England", Region="Europe"},
                new Address{ID=2, Town = "Ibiza", Country="Spain", Region="Europe"},
                new Address{ID=3, Town = "Bondi Beach", Country="Australia", Region="Asia Pacific"},
                new Address{ID=4, Town = "Navagio Beach", Country="Greece", Region="Europe"},
                new Address{ID=5, Town = "Antalaya Beach", Country="Turkey", Region="Middle East"},
                new Address{ID=6, Town = "Phuket", Country="Thailand", Region="Asia Pacific"},
                new Address{ID=7, Town = "Santorini Beach", Country="Greece", Region="Europe"}
            };

            var customers = new Customer[]
            {
                new Customer{ID=1, FirstName="John", LastName="Apple", EmailAddress="johnapple@gov.co.uk", DateOfBirth = DateTime.Parse("18/02/1998"), PhoneNumber="07723429485",
                    Address= new Address{ID=8, Street="House of Commons", Town="Westminster", County="London", Country="England", PostCode="SW1A0RS"}},
                new Customer{ID=2, FirstName="Alice", LastName="Buttercup", EmailAddress="buttercup@outllok.com", DateOfBirth = DateTime.Parse("08/11/1968"), PhoneNumber="07727845073",
                    Address= new Address{ID=9,Street="Peacock Road", Town="Marston", County="Oxfordshire", Country="England", PostCode="OX4A0LS"}},
                new Customer{ID=3, FirstName="Justin", LastName="Time", EmailAddress="timelydone@yahoo.net", DateOfBirth = DateTime.Parse("26/12/1970"), PhoneNumber="07721845267",
                    Address= new Address{ID=10,Street="Bridge Steet", Town="High Wycombe", County="Buckinghamshire", Country="England", PostCode="HP112ET"}}
            };

            var holidays = new Holiday[]
            {
                new Holiday{Title="Trip to Ibiza - All inclusive", Description="Ibiza description",
                    OriginAddressID=1, DestinationAddressID=2, AccommodationType="Hotel", Rating=4, Region="Europe", Price=145},
                new Holiday{Title="Trip to Bondi Beach in Australia", Description="Australia description",
                    OriginAddressID=1, DestinationAddressID=3, AccommodationType="Private", Rating=3, Region="Asia Pacific", Price=99},
                new Holiday{Title="Travel to Navagio Beach in Greece!!", Description="Navagio description",
                    OriginAddressID=1, DestinationAddressID=4, AccommodationType="Hotel", Rating=5, Region="Europe", Price=220},
                new Holiday{Title="Trip to Antalaya - All inclusive", Description="Antalaya description",
                    OriginAddressID=1, DestinationAddressID=5, AccommodationType="Resort", Rating=4, Region="Europe", Price=180},
                new Holiday{Title="Phuket - Thailand's best beach", Description="Phuket description",
                    OriginAddressID=1, DestinationAddressID=6, AccommodationType="Hotel", Rating=5, Region="Asia Pacific", Price=250},
                new Holiday{Title="Trip to Santorini Beach", Description="Santorini description",
                    OriginAddressID=1, DestinationAddressID=7, AccommodationType="Resort", Rating=4, Region="Europe", Price=154}
            };

            var orders = new Order[]
            {
                new Order{ID=1, CustomerID=1, DiscountCodeID=1, OrderDate=DateTime.Parse("18/12/2022"),IsPaid=true,
                    CardPayment= new CardPayment{ID=1, CardNumber="0190456712348907", ExpiryDate=DateTime.Parse("01/2025"), SecurityCode="012"}},
                new Order{ID=2, CustomerID=3, OrderDate=DateTime.Parse("25/01/2023"),IsPaid=true, CardPaymentID=1}
            };

            var order_holidays = new Order_Holiday[]
            {
                new Order_Holiday{OrderID=1,HolidayID=2, Night=3, Quantity=2, StartDate= DateTime.Parse("11/02/2023"), EndDate= DateTime.Parse("14/02/2023")},
                new Order_Holiday{OrderID=2,HolidayID=4, Night=5, Quantity=1, StartDate= DateTime.Parse("10/06/2023"), EndDate= DateTime.Parse("15/06/2023")}
            };

            context.DiscountCodes.AddRange(discountCodes);
            context.Addresses.AddRange(addresses);
            context.Holidays.AddRange(holidays);
            context.Customers.AddRange(customers);
            context.Orders.AddRange(orders);
            context.Order_Holidays.AddRange(order_holidays);
            context.SaveChanges();
        }
    }
}