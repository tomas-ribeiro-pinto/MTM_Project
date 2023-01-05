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

namespace MTM_Holidays.Pages.Discounts
{
    public class EditModel : PageModel
    {
        private readonly MTM_Holidays.Data.ApplicationDbContext _context;

        public EditModel(MTM_Holidays.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DiscountCode DiscountCode { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DiscountCodes == null)
            {
                return NotFound();
            }

            var discountcode =  await _context.DiscountCodes.FirstOrDefaultAsync(m => m.ID == id);
            if (discountcode == null)
            {
                return NotFound();
            }
            DiscountCode = discountcode;
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

            _context.Attach(DiscountCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscountCodeExists(DiscountCode.ID))
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

        private bool DiscountCodeExists(int id)
        {
          return (_context.DiscountCodes?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
