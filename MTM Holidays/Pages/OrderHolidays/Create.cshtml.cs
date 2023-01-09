using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MTM_Holidays.Data;
using MTM_Holidays.Models;

namespace MTM_Holidays.Pages.OrderHolidays
{
    public class CreateModel : PageModel
    {
        private readonly MTM_Holidays.Data.ApplicationDbContext _context;

        public CreateModel(MTM_Holidays.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["DiscountCodeID"] = new SelectList(_context.DiscountCodes, "ID", "Code");
        ViewData["HolidayID"] = new SelectList(_context.Holidays, "ID", "Region");
        ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public Order_Holiday Order_Holiday { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Order_Holidays.Add(Order_Holiday);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
