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

namespace MTM_Holidays.Pages.Pictures
{
    public class EditModel : PageModel
    {
        private readonly MTM_Holidays.Data.ApplicationDbContext _context;

        public EditModel(MTM_Holidays.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Picture Picture { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Pictures == null)
            {
                return NotFound();
            }

            var picture =  await _context.Pictures.FirstOrDefaultAsync(m => m.PictureID == id);
            if (picture == null)
            {
                return NotFound();
            }
            Picture = picture;
           ViewData["HolidayID"] = new SelectList(_context.Holidays, "ID", "Region");
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

            _context.Attach(Picture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PictureExists(Picture.PictureID))
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

        private bool PictureExists(int id)
        {
          return (_context.Pictures?.Any(e => e.PictureID == id)).GetValueOrDefault();
        }
    }
}
