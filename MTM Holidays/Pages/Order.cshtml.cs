using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MTM_Holidays.Data;
using MTM_Holidays.Models;

namespace MTM_Holidays.Pages
{
    /// <summary>
    /// This class is a model to the order process.
    ///
    /// When a customer clicks to buy a holiday, it's presented by a form that is modelled by this class.
    /// A customer selects how many days, persons wants to buy a holiday for
    /// and fills the Address and Customer details, which are added to the database.
    /// 
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>20th Jan 2023</version>

    [Authorize]
    public class OrderModel : PageModel
    {
        private readonly MTM_Holidays.Data.ApplicationDbContext _context;

        public OrderModel(MTM_Holidays.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        private Order Order = new Order();
        private Order_Holiday Order_Holiday = new Order_Holiday();
        private int orderId = 0;

        [BindProperty]
        public Holiday Holiday { get; set; } = default!;

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        [BindProperty]
        public Address Address { get; set; } = default!;

        [BindProperty]
        public DateTime StartDate { get; set; }

        [BindProperty]
        public int Night { get; set; }

        [BindProperty]
        public int Person { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Holidays == null)
            {
                return RedirectToPage("NotFound");
            }

            var holiday = await _context.Holidays.FirstOrDefaultAsync(m => m.ID == id);
            if (holiday == null)
            {
                return RedirectToPage("NotFound");
            }

            holiday.OriginAddress = await _context.Addresses.FirstOrDefaultAsync(m => m.ID == holiday.OriginAddressID);
            holiday.DestinationAddress = await _context.Addresses.FirstOrDefaultAsync(m => m.ID == holiday.DestinationAddressID);
            holiday.Pictures = await _context.Pictures.Where(m => m.HolidayID == holiday.ID).ToListAsync();
            Holiday = holiday;

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Holidays == null || Holiday == null
                        || Customer == null || Address == null || Night < 2 || Person < 1)
            {
                return await OnGetAsync(Holiday.ID);
            }

            AddCustomerInfo();

            AddOrderInfo();

            // Saves the data to the database
            await _context.SaveChangesAsync();

            return RedirectToPage("Checkout", new { id = Order.ID });
        }

        /// <summary>
        /// Adds Customer Details and Address to DbContext
        /// </summary>
        public void AddCustomerInfo()
        {
            _context.Addresses.Add(Address);
            // MAkes sure this order is placed by the current user
            Customer.EmailAddress = User.Identity?.Name;
            Customer.Address = Address;
            _context.Customers.Add(Customer);
        }

        /// <summary>
        /// Adds Order and Order_Holiday details
        /// </summary>
        public void AddOrderInfo()
        {
            Order.Customer = Customer;
            _context.Orders.Add(Order);

            Order_Holiday.Order = Order;
            Order_Holiday.HolidayID = Holiday.ID;
            Order_Holiday.Holiday = default!;

            Order_Holiday.StartDate = StartDate;
            Order_Holiday.Quantity = Person;
            Order_Holiday.Night = Night;

            DateTime endDate = StartDate.AddDays(Convert.ToDouble(Night));
            Order_Holiday.EndDate = endDate;

            _context.Order_Holidays.Add(Order_Holiday);
        }
    }
}
