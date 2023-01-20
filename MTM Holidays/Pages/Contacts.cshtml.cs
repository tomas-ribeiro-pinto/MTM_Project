using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MTM_Holidays.Pages
{
    public class ContactsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public ContactsModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
