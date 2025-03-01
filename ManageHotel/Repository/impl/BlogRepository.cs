using AutoMapper;
using ManageHotel.DAO;
using ManageHotel.DTOs.Blogs;
using ManageHotel.DTOs.Hotels;
using ManageHotel.Models;

namespace ManageHotel.Repository.impl
{
    public class BlogRepository : IBlogRepository
    {
        private readonly IMapper _mapper;
        private readonly BlogDAO _dao;
        public BlogRepository(IMapper mapper, BlogDAO dao)
        {
            _mapper = mapper;
            _dao = dao;
        }
        public void CreateBlog(AddBlogDTO blog)
        {
            var b = _mapper.Map<Blog>(blog);
            _dao.CreateBlog(b);
        }

        public void DeleteBlog(int id)
        {
            _dao.DelteBlog(id);
        }

        public List<GetBlogDTO> GetAllBlog()
        {
            var b = _dao.GetAllBlog();
            var blog= _mapper.Map<List<GetBlogDTO>>(b);
            for(int i = 0 ; i < b.Count; i++)
            {
                blog[i].Hotel = _mapper.Map<GetHotelDTO>(b[i].Hotel);
            }
            return blog;
        }

        public GetBlogDTO GetBlogById(int id)
        {
            return _mapper.Map<GetBlogDTO>(_dao.GetBlogById(id));
        }

        public void UpdateBlog(int id, UpdateBlogDTO blog)
        {
            var b = _mapper.Map<Blog>(blog);
            _dao.UpdateBlog(id, b);
        }
    }
}
