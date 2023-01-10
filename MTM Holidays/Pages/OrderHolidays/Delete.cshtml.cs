using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MTM_Holidays.Data;
using MTM_Holidays.Models;

namespace MTM_Holidays.Pages.OrderHolidays
{
    public class DeleteModel : PageModel
    {
        private readonly MTM_Holidays.Data.ApplicationDbContext _context;

        public DeleteModel(MTM_Holidays.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Order_Holiday Order_Holiday { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Order_Holidays == null)
            {
                return NotFound();
            }

            var order_holiday = await _context.Order_Holidays.FirstOrDefaultAsync(m => m.ID == id);

            if (order_holiday == null)
            {
                return NotFound();
            }
            else 
            {
                Order_Holiday = order_holiday;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Order_Holidays == null)
            {
                return NotFound();
            }
            var order_holiday = await _context.Order_Holidays.FindAsync(id);

            if (order_holiday != null)
            {
                Order_Holiday = order_holiday;
                _context.Order_Holidays.Remove(Order_Holiday);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
