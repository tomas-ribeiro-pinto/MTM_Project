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
    /// This class is a model to the order process while editing.
    ///
    /// When a customer clicks to edit a holiday at checkout, it's redirected to a form that is modelled by this class.
    /// A customer selects how many days, persons wants to buy a holiday for
    /// and fills the Address and Customer details, which are modified in the database.
    /// 
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>20th Jan 2023</version>

    [Authorize]
    public class OrderEditModel : PageModel
    {
        private readonly MTM_Holidays.Data.ApplicationDbContext _context;

        public OrderEditModel(MTM_Holidays.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        [BindProperty]
        public Holiday Holiday { get; set; } = default!;

        [BindProperty]
        public Order_Holiday Order_Holiday { get; set; }

        [BindProperty]
        public DateTime StartDate { get; set; }

        [BindProperty]
        public int Night { get; set; }

        [BindProperty]
        public int Person { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Order_Holidays == null)
            {
                return RedirectToPage("NotFound");
            }

            Order_Holiday = await _context.Order_Holidays.FirstOrDefaultAsync(m => m.ID == id);
            var order = GetOrderAsync(Order_Holiday.OrderID).Result;
            var holiday = await _context.Holidays.FirstOrDefaultAsync(m => m.ID == Order_Holiday.HolidayID);

            if (order == null || holiday == null)
            {
                return RedirectToPage("NotFound");
            }
            else if (User.Identity.Name != Order.Customer.EmailAddress)
            {
                return RedirectToPage("Unauthorized");
            }

            holiday.DestinationAddress = await _context.Addresses.FirstOrDefaultAsync(m => m.ID == holiday.DestinationAddressID);
            holiday.OriginAddress = await _context.Addresses.FirstOrDefaultAsync(m => m.ID == holiday.OriginAddressID);
            holiday.Pictures = await _context.Pictures.Where(m => m.HolidayID == holiday.ID).ToListAsync();
            Holiday = holiday;

            Order = order;

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Order_Holidays == null || _context.Orders == null
                || Order_Holiday.Night < 2 || Order_Holiday.Quantity < 1)
            {
                return await OnGetAsync(Order_Holiday.ID);
            }

            Order.Customer.EmailAddress = User.Identity?.Name;
            Order_Holiday.Order = Order;
            Order_Holiday.Holiday = Holiday;
            _context.Attach(Order).State = EntityState.Modified;
            _context.Attach(Order_Holiday).State = EntityState.Modified;

            // Saves the data to the database
            await _context.SaveChangesAsync();

            return RedirectToPage("Checkout", new { id = Order.ID });
        }

        // Get Order through ID for editing purposes
        private async Task<Order> GetOrderAsync(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return null;
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.ID == order.CustomerID);
            customer.Address = await _context.Addresses.FirstOrDefaultAsync(m => m.ID == customer.AddressID);
            order.Order_Holidays = await _context.Order_Holidays.Where(m => m.OrderID == order.ID)
                .Include(a => a.Holiday)
                .ThenInclude(a => a.DestinationAddress)
                .Include(a => a.Holiday.OriginAddress)
                .ToListAsync();

            order.Customer = customer;
            var discount = await _context.DiscountCodes.FirstOrDefaultAsync(m => m.ID == order.DiscountCodeID);
            if (discount != null)
                order.DiscountCode = discount;

            return order;

        }
    }
}
