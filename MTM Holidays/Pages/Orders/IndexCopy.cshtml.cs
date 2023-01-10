using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MTM_Holidays.Data;
using MTM_Holidays.Models;

namespace MTM_Holidays.Pages.Orders
{
    public class IndexCopyModel : PageModel
    {
        private readonly MTM_Holidays.Data.ApplicationDbContext _context;

        public IndexCopyModel(MTM_Holidays.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Orders != null)
            {
                Order = await _context.Orders
                .Include(o => o.Customer).ToListAsync();
            }
        }
    }
}
