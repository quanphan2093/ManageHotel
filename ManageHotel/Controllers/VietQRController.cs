﻿using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace ManageHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VietQRController : ControllerBase
    {
        [HttpGet("generate")]
        public IActionResult GenerateQR(string account, string bank, int amount, string description)
        {
            string vietQrUrl = $"https://img.vietqr.io/image/{bank}-{account}-compact2.jpg?amount={amount}&addInfo={Uri.EscapeDataString(description)}";
            return Redirect(vietQrUrl);
        }
    }
}
