using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MTM_Holidays.Data;
using MTM_Holidays.Models;

namespace MTM_Holidays.Pages.Discounts
{
    public class DeleteModel : PageModel
    {
        private readonly MTM_Holidays.Data.ApplicationDbContext _context;

        public DeleteModel(MTM_Holidays.Data.ApplicationDbContext context)
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

            var discountcode = await _context.DiscountCodes.FirstOrDefaultAsync(m => m.ID == id);

            if (discountcode == null)
            {
                return NotFound();
            }
            else 
            {
                DiscountCode = discountcode;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.DiscountCodes == null)
            {
                return NotFound();
            }
            var discountcode = await _context.DiscountCodes.FindAsync(id);

            if (discountcode != null)
            {
                DiscountCode = discountcode;
                _context.DiscountCodes.Remove(DiscountCode);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
