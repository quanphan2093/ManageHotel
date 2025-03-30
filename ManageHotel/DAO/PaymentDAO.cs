using ManageHotel.Models;

namespace ManageHotel.DAO
{
    public class PaymentDAO
    {
        private readonly HotelManageContext _context;
        public PaymentDAO(HotelManageContext context)
        {
            _context = context;
        }

        public List<PaymentType> GetPaymentTypes()
        {
            return _context.PaymentTypes.ToList();
        }
    }
}
