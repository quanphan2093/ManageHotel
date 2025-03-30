using ManageHotel.DTOs.PaymentTypes;

namespace ManageHotel.Repository
{
    public interface IPaymentRepository
    {
        List<GetPaymentTypeDTO> GetAllPayment();
    }
}
