using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ManageHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public VNPayController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("create-payment")]
        public IActionResult CreatePayment(int amount, string orderInfo)
        {
            try
            {
                var tmnCode = _configuration["VNPay:TmnCode"];
                var hashSecret = _configuration["VNPay:HashSecret"];
                var returnUrl = _configuration["VNPay:ReturnUrl"];
                var baseUrl = _configuration["VNPay:BaseUrl"];
                var vnp_TxnRef = DateTime.UtcNow.Ticks.ToString();
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                if (ipAddress == "::1") ipAddress = "127.0.0.1";

                // Xóa ký tự đặc biệt khỏi orderInfo
                var sanitizedOrderInfo = Regex.Replace(orderInfo, "[^a-zA-Z0-9 ]", "");

                var vnp_Params = new SortedDictionary<string, string>
                {
                    { "vnp_Version", "2.1.0" },
                    { "vnp_Command", "pay" },
                    { "vnp_TmnCode", tmnCode },
                    { "vnp_Amount", (amount * 100).ToString() },
                    { "vnp_CurrCode", "VND" },
                    { "vnp_TxnRef", vnp_TxnRef },
                    { "vnp_OrderInfo", sanitizedOrderInfo },
                    { "vnp_OrderType", "other" },
                    { "vnp_Locale", "vn" },
                    { "vnp_ReturnUrl", returnUrl },
                    { "vnp_IpAddr", ipAddress },
                    { "vnp_CreateDate", DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss") }
                };
                Console.WriteLine($"HashSecret: '{hashSecret}'"); // Xem có khoảng trắng không

                if (vnp_Params.ContainsKey("vnp_SecureHash"))
                {
                    vnp_Params.Remove("vnp_SecureHash");
                }

                var sortedVnpParams = vnp_Params.OrderBy(kv => kv.Key).ToDictionary(kv => kv.Key, kv => kv.Value);
                var rawData = string.Join("&", vnp_Params.OrderBy(kv => kv.Key)
                                         .Select(kv => $"{kv.Key}={kv.Value}"));


                Console.WriteLine($"Raw Data: {rawData}");

                var secureHash = HmacSha512(hashSecret, rawData);
                Console.WriteLine($"Generated SecureHash: {secureHash}");

                vnp_Params.Add("vnp_SecureHash", secureHash);

                var vnpUrl = $"{baseUrl}?" + string.Join("&",
                            vnp_Params.Select(kv => $"{kv.Key}={HttpUtility.UrlEncode(kv.Value)}"));


                Console.WriteLine($"Payment URL: {vnpUrl}");

                return Ok(new { paymentUrl = vnpUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi tạo thanh toán", error = ex.Message });
            }
        }
        private string HmacSha512(string key, string inputData)
        {
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(inputData));
                return BitConverter.ToString(hashValue).Replace("-", "").ToLower();
            }
        }

        [HttpGet("payment-return")]
        public IActionResult PaymentReturn([FromQuery] Dictionary<string, string> queryParams)
        {
            try
            {
                var hashSecret = _configuration["VNPay:HashSecret"];
                if (!queryParams.ContainsKey("vnp_SecureHash"))
                    return BadRequest(new { success = false, message = "Thiếu chữ ký bảo mật" });

                var secureHash = queryParams["vnp_SecureHash"];
                queryParams.Remove("vnp_SecureHash"); // Loại bỏ để tính hash đúng

                // Sắp xếp tham số theo thứ tự từ điển
                var sortedParams = queryParams.OrderBy(kv => kv.Key)
                                              .ToDictionary(kv => kv.Key, kv => kv.Value);

                // Tạo rawData
                var rawData = string.Join("&", sortedParams.Select(kv => $"{kv.Key}={kv.Value}"));

                // Tạo SecureHash để đối chiếu
                var checkHash = HmacSha512(hashSecret, rawData);

                if (checkHash != secureHash)
                    return BadRequest(new { success = false, message = "Sai chữ ký bảo mật" });

                var transactionStatus = queryParams["vnp_TransactionStatus"];
                if (transactionStatus == "00")
                {
                    return Ok(new { success = true, message = "Thanh toán thành công" });
                }
                return BadRequest(new { success = false, message = "Thanh toán thất bại" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }


    }
}
