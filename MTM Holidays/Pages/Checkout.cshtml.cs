using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MTM_Holidays.Models;

namespace MTM_Holidays.Pages
{
	public class CheckoutModel : PageModel
    {
        private readonly MTM_Holidays.Data.ApplicationDbContext _context;

        public CheckoutModel(MTM_Holidays.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; }

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
            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.ID == order.CustomerID);
            if (order == null)
            {
                return NotFound();
            }
            order.Order_Holidays = await _context.Order_Holidays.Where(m => m.OrderID == order.ID)
                .Include(a => a.Holiday)
                .ThenInclude(a => a.DestinationAddress)
                .Include(a => a.Holiday.OriginAddress)
                .ToListAsync();
            
            order.Customer = customer;
            Order = order;

            return Page();
        }
    }
}
