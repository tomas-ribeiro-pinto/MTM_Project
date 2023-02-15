using Microsoft.EntityFrameworkCore;
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

            /// Discounts

            var d1 = new DiscountCode { Code = "MTM", Discount = 5.00 };
            var d2 = new DiscountCode { Code = "CO550", Discount = 10.50 };
            var d3 = new DiscountCode { Code = "BNU2023", Discount = 100.00 };

            var discountCodes = new DiscountCode[]
            {
                d1,
                d2,
                d3
            };

            /// Addresses

            var addressLondon = new Address { Town = "London", Country = "England", Region = "Europe" };
            context.Addresses.Add(addressLondon);

            /// Customers

            var cust1 = new Customer
            {
                FirstName = "John",
                LastName = "Apple",
                EmailAddress = "johnapple@gov.co.uk",
                DateOfBirth = DateTime.Parse("18/02/1998"),
                PhoneNumber = "07723429485",
                Address = new Address { Street = "House of Commons", Town = "Westminster", County = "London", Country = "England", PostCode = "SW1A0RS" }
            };

            var cust2 = new Customer
            {
                FirstName = "Alice",
                LastName = "Buttercup",
                EmailAddress = "buttercup@outllok.com",
                DateOfBirth = DateTime.Parse("08/11/1968"),
                PhoneNumber = "07727845073",
                Address = new Address { Street = "Peacock Road", Town = "Marston", County = "Oxfordshire", Country = "England", PostCode = "OX4A0LS" }
            };

            var cust3 = new Customer
            {
                FirstName = "Justin",
                LastName = "Time",
                EmailAddress = "timelydone@yahoo.net",
                DateOfBirth = DateTime.Parse("26/12/1970"),
                PhoneNumber = "07721845267",
                Address = new Address { Street = "Bridge Steet", Town = "High Wycombe", County = "Buckinghamshire", Country = "England", PostCode = "HP112ET" }
            };

            var customers = new Customer[]
            {
                cust1,
                cust2,
                cust3       
            };

            /// Holidays

            var h1 = new Holiday
            {
                Title = "Trip to Ibiza - All inclusive",
                Description = "One of the world’s most popular party hotspots, Ibiza is more than just a destination - it’s a lifestyle. With awesome weekly events, showcasing a mix of exclusive pool parties and one-off events from the world’s biggest and freshest stars!",
                OriginAddress = addressLondon,
                DestinationAddress = new Address { Town = "Ibiza", Country = "Spain", Region = "Europe" },
                AccommodationType = "Hotel",
                Rating = 4,
                Region = "Europe",
                Price = 145
            };

            var h2 = new Holiday
            {
                Title = "Trip to Bondi Beach in Australia",
                Description = "The suites Bondi offer spectacular views of Sydney and its breathtaking harbour. World-famous Bondi Beach, home of Australia’s oldest Life Saving Club is a short bus ride away. Private rooms are refurbished with all needed facilities for the guests.",
                OriginAddress = addressLondon,
                DestinationAddress = new Address { Town = "Bondi Beach", Country = "Australia", Region = "Asia Pacific" },
                AccommodationType = "Private",
                Rating = 3,
                Region = "Asia Pacific",
                Price = 99
            };

            var h3 = new Holiday
            {
                Title = "Travel to Navagio Beach in Greece!!",
                Description = "Get your camera at the ready for Navagio Beach, also known as Shipwreck Beach. This stretch of sand is only accessible by boat and it’s home to an actual shipwreck that ran aground in the 1980s, so it’s no surprise it’s one of the most iconic sights!",
                OriginAddress = addressLondon,
                DestinationAddress = new Address { Town = "Navagio Beach", Country = "Greece", Region = "Europe" },
                AccommodationType = "Hotel",
                Rating = 5,
                Region = "Europe",
                Price = 220
            };

            var h4 = new Holiday
            {
                Title = "Trip to Antalaya - All inclusive",
                Description = "Antony and Cleopatra used to love Side. Set on Turkey’s south coast, about an hour from Antalya, the place hasn’t changed much since the Romans were here. There’s an untouched old town and crumbling ruins flanking the sandy beaches.",
                OriginAddress = addressLondon,
                DestinationAddress = new Address { Town = "Antalaya Beach", Country = "Turkey", Region = "Middle East" },
                AccommodationType = "Resort",
                Rating = 4,
                Region = "Europe",
                Price = 180
            };

            var h5 = new Holiday
            {
                Title = "Phuket - Thailand's best beach",
                Description = "On your Phuket holiday, discover why this island is one of Thailand's most popular holiday destinations. With beaches stretching as far as the eye can see and a vibrant diverse culture, and not to mention the buzzing nightlife!",
                OriginAddress = addressLondon,
                DestinationAddress = new Address { Town = "Phuket", Country = "Thailand", Region = "Asia Pacific" },
                AccommodationType = "Hotel",
                Rating = 5,
                Region = "Asia Pacific",
                Price = 250
            };

            var h6 = new Holiday
            {
                Title = "Trip to Santorini Beach",
                Description = "Chic and modern, 5 Stars Diamond Resort & Spa certainly lives up to its grand name. You'll have plenty of opportunity to relax here, whether you simply laze away the days by the pool or indulge yourself with treatments in the spa.",
                OriginAddress = addressLondon,
                DestinationAddress = new Address { Town = "Santorini Beach", Country = "Greece", Region = "Europe" },
                AccommodationType = "Resort",
                Rating = 4,
                Region = "Europe",
                Price = 154
            };


            var holidays = new Holiday[]
            {
                h1,
                h2,
                h3,
                h4,
                h5,
                h6
            };

            /// Orders

            var o1 = new Order
            {
                Customer = cust1,
                DiscountCode = d1,
                OrderDate = DateTime.Parse("18/12/2022"),
                IsPaid = true,
                CardPayment = new CardPayment { CardNumber = "0190456712348907", ExpiryDate = DateTime.Parse("01/2025"), SecurityCode = "012" }
            };

            var o2 = new Order
            {
                Customer = cust3,
                OrderDate = DateTime.Parse("25/01/2023"),
                IsPaid = true,
                CardPayment = new CardPayment { CardNumber = "0190456712348607", ExpiryDate = DateTime.Parse("07/2024"), SecurityCode = "021" }
            };

            var orders = new Order[]
            {
                o1,
                o2
            };

            var order_holidays = new Order_Holiday[]
            {
                new Order_Holiday{Order=o1,Holiday=h2, Night=3, Quantity=2, StartDate= DateTime.Parse("11/02/2023"), EndDate= DateTime.Parse("14/02/2023")},
                new Order_Holiday{Order=o2,Holiday=h4, Night=5, Quantity=1, StartDate= DateTime.Parse("10/06/2023"), EndDate= DateTime.Parse("15/06/2023")},
                new Order_Holiday{Order=o2,Holiday=h5, Night=3, Quantity=4, StartDate= DateTime.Parse("10/09/2024"), EndDate= DateTime.Parse("14/09/2024")}
            };

            /// Pictures

            var p1 = new Picture { URL = "~/assets/img/ibiza.jpg", HolidayID = 1 };
            var p2 = new Picture { URL = "~/assets/img/bondi.jpg", HolidayID = 2 };
            var p3 = new Picture { URL = "~/assets/img/navagio.jpg", HolidayID = 3 };
            var p4 = new Picture { URL = "~/assets/img/antalaya.jpg", HolidayID = 4 };
            var p5 = new Picture { URL = "~/assets/img/tailand.jpg", HolidayID = 5 };
            var p6 = new Picture { URL = "~/assets/img/santorini.jpg", HolidayID = 6 };

            var pictures = new Picture[]
            {
                    p1,
                    p2,
                    p3,
                    p4,
                    p5,
                    p6
            };

            context.DiscountCodes.AddRange(discountCodes);
            context.Holidays.AddRange(holidays);
            context.Customers.AddRange(customers);
            context.Orders.AddRange(orders);
            context.Order_Holidays.AddRange(order_holidays);
            context.Pictures.AddRange(pictures);
            context.SaveChanges();
        }
    }
}