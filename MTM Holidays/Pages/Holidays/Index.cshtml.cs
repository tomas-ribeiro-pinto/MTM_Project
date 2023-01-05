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
    public class IndexModel : PageModel
    {
        private readonly MTM_Holidays.Data.ApplicationDbContext _context;

        public IndexModel(MTM_Holidays.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Holiday> Holiday { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Holidays != null)
            {
                Holiday = await _context.Holidays
                .Include(h => h.DestinationAddress)
                .Include(h => h.OriginAddress).ToListAsync();
            }
        }
    }
}
