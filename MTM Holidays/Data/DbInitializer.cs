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
                new Holiday{Title="Trip to Ibiza - All inclusive", Description="One of the world’s most popular party hotspots, the Ibiza Star is more than just a hotel - it’s a lifestyle. With awesome weekly events, showcasing a mix of exclusive pool parties and one-off events from the world’s biggest and freshest stars. If You looking for a Party holiday with the highest level of Hotel standards - 4 Stars all-inclusive, this place waiting for you. ",
                    OriginAddressID=1, DestinationAddressID=2, AccommodationType="Hotel", Rating=4, Region="Europe", Price=145},
                new Holiday{Title="Trip to Bondi Beach in Australia", Description="The suites Bondi offer spectacular views of Sydney and its breathtaking harbour. World-famous Bondi Beach, home of Australia’s oldest Life Saving Club is only a short bus ride away. Private rooms are fully refurbished and provide all needed facilities for the guests. The whole trip is also provided with an all-inclusive service and a lot of attractions. \r\n",
                    OriginAddressID=1, DestinationAddressID=3, AccommodationType="Private", Rating=3, Region="Asia Pacific", Price=99},
                new Holiday{Title="Travel to Navagio Beach in Greece!!", Description="Get your camera at the ready for Navagio Beach – also known as Smuggler’s Cove or Shipwreck Beach. This stretch of sand is home to an actual shipwreck that ran aground in the 1980s, so it’s no surprise it’s one of Greece’s most iconic sights. Only accessible by boat, set sail to this unique beauty spot and mix sightseeing with soaking up the sun and a spot of swimming. Our Atlas Hotel is one of the closest hotel situated near Navagio Beach. ",
                    OriginAddressID=1, DestinationAddressID=4, AccommodationType="Hotel", Rating=5, Region="Europe", Price=220},
                new Holiday{Title="Trip to Antalaya - All inclusive", Description="Antony and Cleopatra used to love Side. Set on Turkey’s south coast – about an hour from Antalya – the place hasn’t changed much since the Romans were here. There’s an untouched old town and crumbling ruins flanking the sandy beaches. Modern luxuries get a good look-in, too, with a chic harbour, waterfront restaurants and lively bars. We are prepared for You modern, 5-star Antalya Sunrise Resort with places to eat, drink and swim. And the beach on its doorstep is another highlight. ",
                    OriginAddressID=1, DestinationAddressID=5, AccommodationType="Resort", Rating=4, Region="Europe", Price=180},
                new Holiday{Title="Phuket - Thailand's best beach", Description="On your Phuket holiday, discover why this island is one of Thailand's most popular holiday destinations. With beaches stretching as far as the eye can see and a vibrant diverse culture (not to mention the buzzing nightlife), it's easy to fall in love with this exciting destination on a trip to Phuket. We are offering You 5 Star all-inclusive 2022 launched Kari Exclusive Hotel.\r\n",
                    OriginAddressID=1, DestinationAddressID=6, AccommodationType="Hotel", Rating=5, Region="Asia Pacific", Price=250},
                new Holiday{Title="Trip to Santorini Beach", Description="Chic and modern, 5 Stars Diamond Resort & Spa certainly lives up to its grand name. You'll have plenty of opportunity to relax here, whether you simply laze away the days by the pool or indulge yourself with treatments in the spa. We have got 5 delicious restaurants inside our resort and also full all-inclusive is provided for the guests. Located right on the black sands of Kamari, it’s the perfect place for couples to unwind.",
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