using ManageHotel.DTOs.Blogs;

namespace ManageHotel.Repository
{
    public interface IBlogRepository
    {
        List<GetBlogDTO> GetAllBlog();
        GetBlogDTO GetBlogById(int id);
        void CreateBlog(AddBlogDTO blog);
        void UpdateBlog(int id,UpdateBlogDTO blog);
        void DeleteBlog(int id);    
    }
}
