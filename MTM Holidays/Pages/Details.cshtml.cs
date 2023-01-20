using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MTM_Holidays.Models;


namespace MTM_Holidays.Pages.Shared
{
    public class HolidayModel : PageModel
    {
        private readonly MTM_Holidays.Data.ApplicationDbContext _context;

        public HolidayModel(MTM_Holidays.Data.ApplicationDbContext context)
        {
            _context = context;
        }
    

        [BindProperty]
        public Holiday Holiday { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null || _context.Holidays == null)
            {
                return NotFound();
            }

            var holiday = await _context.Holidays.FirstOrDefaultAsync(m => m.ID == id);
            if (holiday == null)
            {
                return NotFound();
            }
            Holiday = holiday;

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Holidays == null || Holiday == null)
                       
            {
                return await OnGetAsync(Holiday.ID);
            }

     

            // Saves the data to the database
            await _context.SaveChangesAsync();

            return RedirectToPage("Details", new { id = Holiday.ID });
        }
    }
}
