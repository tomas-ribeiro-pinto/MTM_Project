using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MTM_Holidays.Data;
using MTM_Holidays.Models;

namespace MTM_Holidays.Pages.Holidays
{
    public class DetailsModel : PageModel
    {
        private readonly MTM_Holidays.Data.ApplicationDbContext _context;

        public DetailsModel(MTM_Holidays.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
            else 
            {
                Holiday = holiday;
            }
            return Page();
        }
    }
}
