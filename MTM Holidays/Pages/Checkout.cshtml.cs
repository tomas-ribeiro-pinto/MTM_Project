using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MTM_Holidays.Models;

namespace MTM_Holidays.Pages
{
    /// <summary>
    /// When a customer clicks to buy a holiday, it's presented with a checkout page.
    /// A customer can modify, delete and confirm details of their order.
    /// They can also add discount codes to the order.
    /// 
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>20th Jan 2023</version>
    public class CheckoutModel : PageModel
    {
        private readonly MTM_Holidays.Data.ApplicationDbContext _context;

        public CheckoutModel(MTM_Holidays.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        [BindProperty]
        public string DiscountCode { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
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
            if (!String.IsNullOrEmpty(DiscountCode))
            {
                var order = GetOrderAsync((int)id).Result;
                var discount = await _context.DiscountCodes.FirstOrDefaultAsync(m => m.Code == DiscountCode);

                if (discount == null)
                {
                    ViewData["DiscountError"] = $"The code '{DiscountCode}' is invalid!";
                }
                else if (order.DiscountCodeID == discount.ID || order.DiscountCode != null)
                {
                    ViewData["DiscountError"] = $"The code '{order.DiscountCode.Code}' is already applied. You may only use one code per order!";
                }
                else
                {
                    order.DiscountCode = discount;
                    _context.Attach(order).State = EntityState.Modified;
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!OrderExists(order.ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            return await OnGetAsync(Order.ID);
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

        public async Task<IActionResult> OnGetRemoveDiscount(int? id)
        {
            var order = GetOrderAsync((int)id).Result;

            order.DiscountCode = null;
            order.DiscountCodeID = null;

            _context.Attach(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await OnGetAsync(order.ID);
        }
    }
}
