using ManageHotel.DTOs.Feedbacks;

namespace ManageHotel.Repository
{
    public interface IFeedbackRepository
    {
        List<GetFeedbackDTO> GetAllFeedback();
        GetFeedbackDTO GetFeedbackById(int id);
        void CreateFeedback(AddFeedbackDTO dto);
        void UpdateFeedback(int id, UpdateFeedbackDTO dto);
        void DeleteFeedback(int id);
        List<GetFeedbackDTO> GetFeedbackByHotelId(int hotelId);
        List<GetFeedbackDTO> GetFeedbackByBlogId(int blogId);
    }
}
