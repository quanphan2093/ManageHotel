using ManageHotel.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _repository;
        public PaymentController(IPaymentRepository repository)
        {
            _repository= repository;
        }
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_repository.GetAllPayment());
        }
    }
}
