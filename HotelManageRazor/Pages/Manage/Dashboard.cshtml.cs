using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace HotelManageRazor.Pages.Manage
{
    public class DashboardModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DashboardModel()
        {
            _httpClient = new HttpClient();
        }
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Common/403");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return Page();
        }
    }
}
