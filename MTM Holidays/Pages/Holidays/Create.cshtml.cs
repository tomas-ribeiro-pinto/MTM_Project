using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MTM_Holidays.Data;
using MTM_Holidays.Models;

namespace MTM_Holidays.Pages.Holidays
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
        ViewData["DestinationAddressID"] = new SelectList(_context.Address, "ID", "Country");
        ViewData["OriginAddressID"] = new SelectList(_context.Address, "ID", "Country");
            return Page();
        }

        [BindProperty]
        public Holiday Holiday { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Holidays == null || Holiday == null)
            {
                return Page();
            }

            _context.Holidays.Add(Holiday);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
