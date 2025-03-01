using ManageHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageHotel.DAO
{
    public class HotelDAO
    {
        private readonly HotelManageContext _context;
        public HotelDAO(HotelManageContext context)
        {
            _context = context;
        }

        public List<Hotel> GetAllHotel()
        {   
            var hotel = new List<Hotel>();
            try
            {
                hotel = _context.Hotels.Include(x => x.Account).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return hotel;
        }

        public Hotel GetHotelById(int id)
        {   
            Hotel hotel= new Hotel();
            try
            {
                hotel = _context.Hotels.Include(x => x.Account).FirstOrDefault(x => x.HotelId == id);   
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return hotel;
        }

        public void CreateHotel(Hotel hotel)
        {
            try
            {
                _context.Hotels.Add(hotel);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateHotel(int id, Hotel hotel)
        {
            try
            {
                var h = _context.Hotels.Find(id);
                if(h != null)
                {
                    h.HotelName = hotel.HotelName;
                    h.Address = hotel.Address;
                    h.Image=hotel.Image;
                    h.Description = hotel.Description;
                    h.CreateAt = hotel.CreateAt;
                    h.UpdateAt = DateTime.Now;
                    h.IsDeleted = hotel.IsDeleted;
                    h.AccountId = hotel.AccountId;
                    _context.Hotels.Update(h);
                    _context.SaveChanges();
                }
            }
            catch(Exception ex )
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteHotel(int id)
        {
            try
            {
                var h = _context.Hotels.Find(id);
                if( h != null )
                {
                    h.IsDeleted = true;
                    _context.Hotels.Update(h);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
