using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MTM_Holidays.Data;
using MTM_Holidays.Models;

namespace MTM_Holidays.Pages.OrderHolidays
{
    public class EditModel : PageModel
    {
        private readonly MTM_Holidays.Data.ApplicationDbContext _context;

        public EditModel(MTM_Holidays.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order_Holiday Order_Holiday { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Order_Holidays == null)
            {
                return NotFound();
            }

            var order_holiday =  await _context.Order_Holidays.FirstOrDefaultAsync(m => m.ID == id);
            if (order_holiday == null)
            {
                return NotFound();
            }
            Order_Holiday = order_holiday;
           ViewData["DiscountCodeID"] = new SelectList(_context.DiscountCodes, "ID", "Code");
           ViewData["HolidayID"] = new SelectList(_context.Holidays, "ID", "Region");
           ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "ID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Order_Holiday).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Order_HolidayExists(Order_Holiday.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool Order_HolidayExists(int id)
        {
          return _context.Order_Holidays.Any(e => e.ID == id);
        }
    }
}
