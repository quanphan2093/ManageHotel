using ManageHotel.DTOs.Bookings;
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
            return _context.Bookings.Include(x => x.PaymentType).Include(x => x.BookingDetails).ThenInclude(x => x.Room).ToList();
        }
        public List<Booking> GetBookingByDay(DateTime from, DateTime to)
        {   
            if(from > to)
            {
                return null;
            }
            return _context.Bookings.Include(x => x.PaymentType).Include(x => x.BookingDetails).ThenInclude(x => x.Room).Where(x => x.StartDate <= to || x.EndDate >= from).ToList();
        }
        //
        public List<Booking> GetBookingByPhoneNumber(string phoneNumber)
        {
            return _context.Bookings.Include(x => x.PaymentType).Include(x => x.BookingDetails).ThenInclude(x => x.Room).Where(x => x.PhoneNumber.Equals(phoneNumber)).ToList();
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

        public void UpdateStatusBooking(int id, Booking booking)
        {
            var b = _context.Bookings.Find(id);
            if (b != null)
            {
                b.Status = booking.Status;
                _context.Bookings.Update(b);
                _context.SaveChanges();
            }
        }
    }
}
