using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MTM_Holidays.Data;
using MTM_Holidays.Models;

namespace MTM_Holidays.Pages
{
    /// <author>Tomás Pinto</author>
    /// <version>26th Jan 2023</version>
    public class HolidayModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration Configuration;

        public string PriceSort { get; set; }
        public string SearchString { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        [BindProperty]
        public List<Holiday> Holidays { get; set; }

        public HolidayModel(ILogger<IndexModel> logger, ApplicationDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            Configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync(string sortOrder,
            string currentFilter, string searchString)
        {
            var holidays = await _context.Holidays.ToListAsync();
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

            CurrentFilter = currentFilter;
            SearchString = searchString;

            CurrentSort = sortOrder;
            PriceSort = String.IsNullOrEmpty(sortOrder) ? "price" : "";
            PriceSort = sortOrder == "price" ? "price_desc" : "price";

            if (!String.IsNullOrEmpty(searchString))
            {
                Holidays = Holidays.Where(s => s.Title.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "price_desc":
                    Holidays = Holidays.OrderByDescending(s => s.Price).ToList();
                    break;
                case "price":
                    Holidays = Holidays.OrderBy(s => s.Price).ToList();
                    break;
                default:
                    Holidays = Holidays.OrderBy(s => s.Title).ToList();
                    break;
            }

            switch (currentFilter)
            {
                case "hotel":
                    Holidays = Holidays.Where(s => s.AccommodationType == "Hotel").ToList();
                    break;
                case "resort":
                    Holidays = Holidays.Where(s => s.AccommodationType == "Resort").ToList();
                    break;
                case "private":
                    Holidays = Holidays.Where(s => s.AccommodationType == "Private").ToList();
                    break;
                default:
                    break;
            }

            return Page();
        }
    }
}
