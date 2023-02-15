using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MTM_Holidays.Data;
using MTM_Holidays.Models;

namespace MTM_Holidays.Pages.CardPayments
{
    public class DetailsModel : PageModel
    {
        private readonly MTM_Holidays.Data.ApplicationDbContext _context;

        public DetailsModel(MTM_Holidays.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public CardPayment CardPayment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CardPayments == null)
            {
                return NotFound();
            }

            var cardpayment = await _context.CardPayments.FirstOrDefaultAsync(m => m.ID == id);
            if (cardpayment == null)
            {
                return NotFound();
            }
            else 
            {
                CardPayment = cardpayment;
            }
            return Page();
        }
    }
}
