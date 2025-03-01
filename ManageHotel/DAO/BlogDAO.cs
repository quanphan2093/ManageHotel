using ManageHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageHotel.DAO
{
    public class BlogDAO
    {
        private readonly HotelManageContext _context;
        public BlogDAO(HotelManageContext context)
        {
            _context = context;            
        }

        public List<Blog> GetAllBlog()
        {
            return _context.Blogs.Include(x => x.Hotel).ToList();
        }

        public Blog GetBlogById(int id)
        {
            return _context.Blogs.Include(x => x.Hotel).FirstOrDefault(x => x.BlogId == id);
        }

        public void CreateBlog(Blog blog)
        {
            _context.Blogs.Add(blog);
            _context.SaveChanges();
        }

        public void UpdateBlog(int id,Blog blog)
        {
            var b = _context.Blogs.Find(id);
            if(b != null)
            {
                b.Title = blog.Title;
                b.Description = blog.Description;
                b.CreateAt = blog.CreateAt;
                b.BriefInfo=blog.BriefInfo;
                b.IsDeleted = blog.IsDeleted;
                b.HotelId = blog.HotelId;
                _context.Blogs.Update(b);
                _context.SaveChanges();
            }
        }

        public void DelteBlog(int id)
        {
            var b = _context.Blogs.Find(id);
            if(b != null)
            {
                b.IsDeleted=true;
                _context.Blogs.Update(b);
                _context.SaveChanges();
            }
        }
    }
}
