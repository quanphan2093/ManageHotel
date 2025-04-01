using ManageHotel.DTOs.Accounts;
using ManageHotel.DTOs.Roles;
using ManageHotel.DTOs.Rooms;
using ManageHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using ManageHotel.DAO;

namespace HotelManageRazor.Pages.Manage
{
    public class ManageAccountModel : PageModel
    {   
        private readonly HttpClient client;
        private readonly string AccountApiUrl;
        private readonly string RoleApiUrl;
        public ManageAccountModel(IHttpContextAccessor httpContextAccessor)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            AccountApiUrl = "https://localhost:7036/api/Account";
            RoleApiUrl = "https://localhost:7036/api/Role";
        }
        public List<AccountRequestDTO> GetAccounts { get; set; } 
        public AccountRequestDTO GetRole { get; set; } 
        public List<GetRoleDTO> GetRoles { get; set; } 
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Common/403");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var accountresponse= await client.GetAsync(AccountApiUrl);
            var roleresponse= await client.GetStringAsync(RoleApiUrl);
            if (!accountresponse.IsSuccessStatusCode)
            {
                return RedirectToPage("/Common/403");
            }
            var accountJson = await accountresponse.Content.ReadAsStringAsync();
            GetAccounts = JsonSerializer.Deserialize<List<AccountRequestDTO>>(accountJson, options);
            GetAccounts = GetAccounts.OrderByDescending(x => x.AccountId).ToList();
            GetRoles = JsonSerializer.Deserialize<List<GetRoleDTO>>(roleresponse, options);
            return Page();
        }

        public async Task<IActionResult> OnPostCreate(string email, int role)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            AddAccountDTO addAccountDTO = new AddAccountDTO();
            addAccountDTO.Email = email;
            addAccountDTO.IsDeleted = true;
            addAccountDTO.RoleId = role;
            addAccountDTO.Password = GenerateRandomString(8);
            addAccountDTO.CreateAt= DateTime.Now;
            var createAccount = AccountApiUrl + "/Register";
            var jsonContent = new StringContent(JsonSerializer.Serialize(addAccountDTO, options), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(createAccount, jsonContent);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostActive(int id)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var deleteAccount = AccountApiUrl + "/" + id;
            var response = await client.DeleteAsync(deleteAccount);
            GetRole= JsonSerializer.Deserialize<AccountRequestDTO>(deleteAccount, options);
            Console.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Failed to activate account.");
            }
        }

        //public async Task<IActionResult> OnPostUpdate(int id, string name, string phone, int role)
        //{
        //    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        //    var updateAccount = AccountApiUrl + "/" + id;
        //    var updateData = new UpdateAccountDTO
        //    {
        //        Name = name,
        //        PhoneNumber = phone,
        //        RoleId = role,
        //        UpdateAt = DateTime.UtcNow 
        //    };
        //    Console.WriteLine("Sending JSON: " + JsonSerializer.Serialize(updateData));
        //    var jsonContent = new StringContent(JsonSerializer.Serialize(updateData), Encoding.UTF8, "application/json");
        //    var response = await client.PutAsync(updateAccount, jsonContent);
        //    Console.WriteLine($"Response Status: {response.StatusCode}");
        //    Console.WriteLine("Response Content: " + await response.Content.ReadAsStringAsync());
        //    return RedirectToPage();
        //}

        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }
    }
}
