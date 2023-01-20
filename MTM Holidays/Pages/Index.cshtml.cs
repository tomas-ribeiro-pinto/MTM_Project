using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MTM_Holidays.Data;
using MTM_Holidays.Models;

namespace MTM_Holidays.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ApplicationDbContext _context;

    [BindProperty]
    public List<Holiday> Holidays { get; set; }

    public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var holidays = await _context.Holidays.Where(p => p.ID <= 6).ToListAsync();
        if (holidays == null)
        {
            return NotFound();
        }

        foreach (var holiday in holidays)
        {
            holiday.Pictures = await _context.Pictures.Where(m => m.HolidayID == holiday.ID)
                .ToListAsync();
        }

        Holidays = holidays;

        return Page();
    }
}

