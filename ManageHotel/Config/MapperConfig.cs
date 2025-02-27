using AutoMapper;
using ManageHotel.DTOs.Accounts;
using ManageHotel.DTOs.Blogs;
using ManageHotel.DTOs.BookingDetails;
using ManageHotel.DTOs.Bookings;
using ManageHotel.DTOs.Feedbacks;
using ManageHotel.DTOs.Hotels;
using ManageHotel.DTOs.PaymentTypes;
using ManageHotel.DTOs.RoomImages;
using ManageHotel.DTOs.Rooms;
using ManageHotel.DTOs.TypeRooms;
using ManageHotel.Models;

namespace ManageHotel.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Account, AccountRequestDTO>().ReverseMap();
            CreateMap<Account, AddAccountDTO>().ReverseMap();
            CreateMap<Account, UpdateAccountDTO>().ReverseMap();

            CreateMap<Blog, GetBlogDTO>().ReverseMap();
            CreateMap<Blog, AddBlogDTO>().ReverseMap();
            CreateMap<Blog, UpdateBlogDTO>().ReverseMap();

            CreateMap<BookingDetail, GetBookingDetailDTO>().ReverseMap();
            CreateMap<BookingDetail, AddBookingDetailDTO>().ReverseMap();

            CreateMap<Booking, GetBookingDTO>().ReverseMap();
            CreateMap<Booking, AddBookingDTO>().ReverseMap();
            CreateMap<Booking, UpdateBookingDTO>().ReverseMap();

            CreateMap<Feedback, GetFeedbackDTO>().ReverseMap();
            CreateMap<Feedback, UpdateFeedbackDTO>().ReverseMap();
            CreateMap<Feedback, AddFeedbackDTO>().ReverseMap();

            CreateMap<Hotel, GetHotelDTO>().ReverseMap();
            CreateMap<Hotel, AddHotelDTO>().ReverseMap();
            CreateMap<Hotel, UpdateHotelDTO>().ReverseMap();
            
            CreateMap<PaymentType, GetPaymentTypeDTO>().ReverseMap();
            CreateMap<PaymentType, UpdatePaymentTypeDTO>().ReverseMap();
            CreateMap<PaymentType, AddPaymentType>().ReverseMap();
            
            CreateMap<RoomImage, GetRoomImageDTO>().ReverseMap();
            CreateMap<RoomImage, AddRoomImageDTO>().ReverseMap();
            CreateMap<RoomImage, UpdateRoomImageDTO>().ReverseMap();

            CreateMap<Room, GetRoomDTO>().ReverseMap();
            CreateMap<Room, AddRoomDTO>().ReverseMap();
            CreateMap<Room, UpdateRoomDTO>().ReverseMap();

            CreateMap<TypeRoom, GetTypeRoomDTO>().ReverseMap();
            CreateMap<TypeRoom, AddTypeRoomDTO>().ReverseMap();
            CreateMap<TypeRoom, UpdateTypeRoomDTO>().ReverseMap();
        }
    }
}
