using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MTM_Holidays.Pages;

public class FAQModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public FAQModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}

