using ManageHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageHotel.DAO
{
    public class BookingDetailsDAO
    {
        private readonly HotelManageContext _context;
        public BookingDetailsDAO(HotelManageContext context)
        {
            _context = context;
        }

        public List<BookingDetail> GetBookingDetails(int bookingId)
        {
            return _context.BookingDetails.Include(x => x.Booking).Include(x => x.Room).Where(x => x.BookingId == bookingId).ToList();
        }

        public void CreateBookingDetail(int id, BookingDetail bookingDetail)
        {
            var b = _context.Bookings.Find(id);
            if (b != null)
            {
                bookingDetail.BookingId = b.BookingId;
                _context.BookingDetails.Add(bookingDetail);
                _context.SaveChanges();
            }
        }

        public void UpdateBookingDetail(int bookingid, BookingDetail bookingDetail)
        {
            var b = _context.BookingDetails.Find(bookingid);
            if(b != null)
            {
                var room = _context.Rooms.Find(bookingDetail.RoomId);
                b.RoomId = bookingDetail.RoomId;
                b.Quantity = bookingDetail.Quantity;
                b.Price = room.Price * bookingDetail.Quantity;
                _context.BookingDetails.Update(b);

                var booking = _context.Bookings.Find(b.BookingId);
                if (booking != null)
                {
                    decimal result = 0;
                    foreach (var item in booking.BookingDetails)
                    {
                        result += item.Price;
                    }
                    booking.TotalPrice = result;
                    _context.Bookings.Update(booking);
                }
                _context.SaveChanges();
            }
        }

        public void DeleteBookingDetail(int bookingid)
        {
            var b = _context.BookingDetails.Find(bookingid);
            if( b != null)
            {
                var booking = _context.Bookings.Find(b.BookingId);

            }

        }
    }
}
