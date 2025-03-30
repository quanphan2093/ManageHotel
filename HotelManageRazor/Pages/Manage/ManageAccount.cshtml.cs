using ManageHotel.DTOs.Accounts;
using ManageHotel.DTOs.Roles;
using ManageHotel.DTOs.Rooms;
using ManageHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace HotelManageRazor.Pages.Manage
{
    public class ManageAccountModel : PageModel
    {   
        private readonly HttpClient client;
        private readonly string AccountApiUrl;
        private readonly string RoleApiUrl;
        public ManageAccountModel()
        {
            client = new HttpClient();
            AccountApiUrl = "https://localhost:7036/api/Account";
            RoleApiUrl = "https://localhost:7036/api/Role";
        }
        public List<AccountRequestDTO> GetAccounts { get; set; } 
        public AccountRequestDTO GetRole { get; set; } 
        public List<GetRoleDTO> GetRoles { get; set; } 
        public async Task OnGet()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var accountresponse= await client.GetStringAsync(AccountApiUrl);
            var roleresponse= await client.GetStringAsync(RoleApiUrl);
            GetAccounts = JsonSerializer.Deserialize<List<AccountRequestDTO>>(accountresponse, options);
            GetRoles = JsonSerializer.Deserialize<List<GetRoleDTO>>(roleresponse, options);
        }

        public async Task<IActionResult> OnPostActive(int id)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var deleteAccount = AccountApiUrl + "/" + id;
            var response = await client.DeleteAsync(deleteAccount);
            GetRole= JsonSerializer.Deserialize<AccountRequestDTO>(deleteAccount, options);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("ManageAccount");
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Failed to activate account.");
            }
        }

        public async Task<IActionResult> OnPostUpdate(int id, string name, string phone, int role)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var updateAccount = AccountApiUrl + "/" + id;
            var updateData = new UpdateAccountDTO
            {
                Name = name,
                PhoneNumber = phone,
                RoleId = role,
                UpdateAt = DateTime.UtcNow 
            };
            Console.WriteLine("Sending JSON: " + JsonSerializer.Serialize(updateData));
            var jsonContent = new StringContent(JsonSerializer.Serialize(updateData), Encoding.UTF8, "application/json");
            var response = await client.PutAsync(updateAccount, jsonContent);
            Console.WriteLine($"Response Status: {response.StatusCode}");
            Console.WriteLine("Response Content: " + await response.Content.ReadAsStringAsync());
            return RedirectToPage("ManageAccount");
        }
    }
}
