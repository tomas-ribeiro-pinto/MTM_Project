using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MTM_Holidays.Models;


namespace MTM_Holidays.Pages
{
    /// <author>Milena Michalska</author>
    /// <version>20th Jan 2023</version>
    public class DetailsModel : PageModel
    {
        private readonly MTM_Holidays.Data.ApplicationDbContext _context;

        public DetailsModel(MTM_Holidays.Data.ApplicationDbContext context)
        {
            _context = context;
        }


        [BindProperty]
        public Holiday Holiday { get; set; } = default!;

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

            holiday.OriginAddress = await _context.Addresses.FirstOrDefaultAsync(m => m.ID == holiday.OriginAddressID);
            holiday.DestinationAddress = await _context.Addresses.FirstOrDefaultAsync(m => m.ID == holiday.DestinationAddressID);
            holiday.Pictures = await _context.Pictures.Where(m => m.HolidayID == holiday.ID).ToListAsync(); ;
            Holiday = holiday;

            return Page();
        }
    }
}
