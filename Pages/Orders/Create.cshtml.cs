using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MTM_Holidays.Data;
using MTM_Holidays.Models;

namespace MTM_Holidays.Pages.Orders
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
        ViewData["CardPaymentID"] = new SelectList(_context.CardPayments, "ID", "CardNumber");
        ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "EmailAddress");
        ViewData["DiscountCodeID"] = new SelectList(_context.DiscountCodes, "ID", "Code");
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
