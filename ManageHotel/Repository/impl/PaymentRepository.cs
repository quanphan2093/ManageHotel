using AutoMapper;
using ManageHotel.DAO;
using ManageHotel.DTOs.PaymentTypes;

namespace ManageHotel.Repository.impl
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDAO _dao;
        private readonly IMapper _mapper;
        public PaymentRepository(PaymentDAO dao, IMapper mapper)
        {
            _dao = dao;
            _mapper = mapper;
        }
        public List<GetPaymentTypeDTO> GetAllPayment()
        {
            return _mapper.Map<List<GetPaymentTypeDTO>>(_dao.GetPaymentTypes());
        }
    }
}
