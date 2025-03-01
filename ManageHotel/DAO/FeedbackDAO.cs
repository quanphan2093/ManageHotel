using ManageHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageHotel.DAO
{
    public class FeedbackDAO
    {
        private readonly HotelManageContext _context;
        public FeedbackDAO(HotelManageContext context)
        {
            _context = context;
        }

        public List<Feedback> GetAllHotel()
        {
            return _context.Feedbacks.Include(x => x.Blog).Include(x => x.Hotel).ToList();
        }

        public Feedback GetFeedbackById(int id)
        {
            return _context.Feedbacks.Include(x => x.Blog).Include(x => x.Hotel).FirstOrDefault(x => x.FeedbackId == id);
        }

        public void CreateFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
        }

        public void UpdateFeedback(int id, Feedback feedback)
        {
            var f = _context.Feedbacks.Find(id);
            if (f != null)
            {
                f.Content= feedback.Content;
                f.Image= feedback.Image;
                f.CreateAt= feedback.CreateAt;
                f.PhoneNumber= feedback.PhoneNumber;
                f.BlogId= feedback.BlogId;
                f.HotelId= feedback.HotelId;
                f.IsDeleted= feedback.IsDeleted;
                _context.Feedbacks.Update(f);
                _context.SaveChanges();
            }
        }

        public void DeleteFeedback(int id)
        {
            var f = _context.Feedbacks.Find(id);
            if(f != null)
            {
                f.IsDeleted = true;
                _context.Feedbacks.Update(f);
                _context.SaveChanges();
            }
        }

        public List<Feedback> GetFeedbackByHotelId(int hotelId)
        {
            return _context.Feedbacks.Include(x => x.Hotel).Where(x => x.HotelId == hotelId).ToList();
        }

        public List<Feedback> GetFeedbackByBlogId(int blogId)
        {
            return _context.Feedbacks.Include(x => x.Blog).Where(x => x.BlogId == blogId).ToList();
        }
    }
}
