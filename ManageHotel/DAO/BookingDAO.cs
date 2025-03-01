using ManageHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageHotel.DAO
{
    public class BookingDAO
    {
        private readonly HotelManageContext _context;
        public BookingDAO(HotelManageContext context)
        {
            _context = context;
        }

        public List<Booking> GetAllBooking()
        {
            return _context.Bookings.Include(x => x.PaymentType).ToList();
        }

        public void CreateBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public void UpdateBooking(int id, Booking booking)
        {
            var b = _context.Bookings.Find(id);
            if(b!= null)
            {
                b.Status = booking.Status;
                b.FullName = booking.FullName;
                b.StartDate= booking.StartDate;
                b.EndDate= booking.EndDate;
                _context.Bookings.Update(b);
                _context.SaveChanges();
            }
        }
    }
}
