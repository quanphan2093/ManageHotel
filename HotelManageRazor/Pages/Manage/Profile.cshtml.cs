using ManageHotel.DTOs.Accounts;
using ManageHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using static QRCoder.PayloadGenerator;
using System.Text.Json;
using System.Text;

namespace HotelManageRazor.Pages.Manage
{
    public class ProfileModel : PageModel
    {
        private readonly HttpClient client;
        private readonly string AccountApiUrl;
        private readonly string RoleApiUrl;
        public ProfileModel(IHttpContextAccessor httpContextAccessor)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            AccountApiUrl = "https://localhost:7036/api/Account";
            RoleApiUrl = "https://localhost:7036/api/Role";
        }
        public void OnGet(int id)
        {

        }

        public async Task<IActionResult> OnPost(int id, string name, string phone)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            UpdateAccountDTO addAccountDTO = new UpdateAccountDTO();
            addAccountDTO.Name= name;
            addAccountDTO.IsDeleted = false;
            addAccountDTO.PhoneNumber=phone;
            addAccountDTO.UpdateAt = DateTime.Now;
            var createAccount = AccountApiUrl + "/"+id;
            var jsonContent = new StringContent(JsonSerializer.Serialize(addAccountDTO, options), Encoding.UTF8, "application/json");
            var response = await client.PutAsync(createAccount, jsonContent);
            return LocalRedirect("/Dashboard");
        }
    }
}
