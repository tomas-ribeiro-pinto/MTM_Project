using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MTM_Holidays.Models;

namespace MTM_Holidays.Pages
{
    /// <summary>
    /// Confirmation Page for a order placed by a user.
    /// 
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>22nd Jan 2023</version>

    [Authorize]
    public class ConfirmationModel : PageModel
    {

        private readonly MTM_Holidays.Data.ApplicationDbContext _context;

        public ConfirmationModel(MTM_Holidays.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return RedirectToPage("NotFound");
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return RedirectToPage("NotFound");
            }
            else if (!order.IsPaid)
            {
                return RedirectToPage("Checkout", new {id = order.ID});
            }

            Order = GetOrderAsync(order.ID).Result;

            if (User.Identity.Name != Order.Customer.EmailAddress)
            {
                return RedirectToPage("Unauthorized");
            }


            return Page();
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (var order_holiday in Order.Order_Holidays)
            {
                total += CalculateTotal(order_holiday);
            }
            if (Order.DiscountCode != null)
                total -= Order.DiscountCode.Discount;
            return total;
        }

        public double CalculateTotal(Order_Holiday order_Holiday)
        {
            double total;
            var holiday = order_Holiday.Holiday;
            total = holiday.Price * order_Holiday.Night * order_Holiday.Quantity;
            return total;
        }

        private async Task<Order> GetOrderAsync(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return null;
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.ID == order.CustomerID);
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
