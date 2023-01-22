using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MTM_Holidays.Models;

namespace MTM_Holidays.Pages
{

    /// <summary>
    /// After the customer confirms the order details, it is redirected to the payment page.
    ///
    /// Payment is always accepted if the validation is correct. This does not implement any real payment transactions going on.
    /// 
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>20th Jan 2023</version>

    [Authorize]
    public class PaymentModel : PageModel
    {
        private readonly MTM_Holidays.Data.ApplicationDbContext _context;

        public PaymentModel(MTM_Holidays.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; }

        [BindProperty]
        public CardPayment CardPayment { get; set; } = default!;

        [BindProperty]
        public string DiscountCode { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }
            if (User.Identity.Name != Order.Customer.EmailAddress)
            {
                return RedirectToPage("Unauthorized");
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }
            else if (order.IsPaid)
            {
                return RedirectToPage("Index");
            }

            Order = GetOrderAsync(order.ID).Result;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            Order = GetOrderAsync((int)id).Result;
            var expiryDate = CardPayment.ExpiryDate.Date;
            var today = DateTime.Today;

            if (!ModelState.IsValid || _context.Holidays == null || Order == null
            || CardPayment == null)
            {
                //return page with this order ID
                return await OnGetAsync(Order.ID);
            }
            else if (expiryDate <= today && !(expiryDate.Month == today.Month && expiryDate.Year == today.Year))
            {
                ModelState.AddModelError("CardPayment.ExpiryDate", "Expired Date invalid! Please input a greater value.");
                return await OnGetAsync(Order.ID);
            }

            var cardNumber = CardPayment.CardNumber;
            var securityCode = CardPayment.SecurityCode;

            // Check that only numbers are input by the user for card number and security code
            foreach (char c in cardNumber)
            {
                if (!Char.IsDigit(c))
                {
                    ModelState.AddModelError("CardPayment.CardNumber", "Please input a valid number.");
                    return await OnGetAsync(Order.ID);
                }
            }
            foreach (char c in securityCode)
            {
                if(!Char.IsDigit(c))
                {
                    ModelState.AddModelError("CardPayment.SecurityCode", "Please input a valid number.");
                    return await OnGetAsync(Order.ID);
                }
            }

            Order.CardPayment = CardPayment;
            Order.IsPaid = true;

            _context.Attach(Order).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(Order.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Index");
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

        private bool OrderExists(int id)
        {
            return _context.Holidays.Any(e => e.ID == id);
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
