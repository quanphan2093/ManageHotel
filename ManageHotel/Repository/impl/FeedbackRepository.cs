using AutoMapper;
using ManageHotel.DAO;
using ManageHotel.DTOs.Blogs;
using ManageHotel.DTOs.Feedbacks;
using ManageHotel.DTOs.Hotels;
using ManageHotel.Models;

namespace ManageHotel.Repository.impl
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IMapper _mapper;
        private readonly FeedbackDAO _dao;
        public FeedbackRepository(IMapper mapper, FeedbackDAO dao)
        {
            _mapper = mapper;
            _dao = dao;
        }
        public void CreateFeedback(AddFeedbackDTO dto)
        {
            var f = _mapper.Map<Feedback>(dto);
            _dao.CreateFeedback(f);
        }

        public void DeleteFeedback(int id)
        {
            _dao.DeleteFeedback(id);
        }

        public List<GetFeedbackDTO> GetAllFeedback()
        {
            var f = _dao.GetAllHotel();
            var feedback = _mapper.Map<List<GetFeedbackDTO>>(f);
            for(int i= 0;i <feedback.Count;i++)
            {
                feedback[i].Blog = _mapper.Map<GetBlogDTO>(f[i].Blog);
                feedback[i].Hotel= _mapper.Map<GetHotelDTO>(f[i].Hotel);
            }
            return feedback;
        }

        public List<GetFeedbackDTO> GetFeedbackByBlogId(int blogId)
        {
            var f = _dao.GetFeedbackByBlogId(blogId);
            var feedback = _mapper.Map<List<GetFeedbackDTO>>(f);
            for(int i=0; i<feedback.Count;i++)
            {
                feedback[i].Blog = _mapper.Map<GetBlogDTO>(f[i].Blog);
            }
            return feedback;
        }

        public List<GetFeedbackDTO> GetFeedbackByHotelId(int hotelId)
        {
            var f = _dao.GetFeedbackByHotelId(hotelId);
            var feedback = _mapper.Map<List<GetFeedbackDTO>>(f);
            for (int i = 0; i < feedback.Count; i++)
            {
                feedback[i].Hotel = _mapper.Map<GetHotelDTO>(f[i].Hotel);
            }
            return feedback;
        }

        public GetFeedbackDTO GetFeedbackById(int id)
        {
            var feedback = _dao.GetFeedbackById(id);
            return _mapper.Map<GetFeedbackDTO>(feedback);
        }

        public void UpdateFeedback(int id, UpdateFeedbackDTO dto)
        {
            var f = _mapper.Map<Feedback>(dto);
            _dao.UpdateFeedback(id, f);
        }
    }
}
