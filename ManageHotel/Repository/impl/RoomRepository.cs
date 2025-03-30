using AutoMapper;
using ManageHotel.DAO;
using ManageHotel.DTOs.Bookings;
using ManageHotel.DTOs.Hotels;
using ManageHotel.DTOs.RoomImages;
using ManageHotel.DTOs.Rooms;
using ManageHotel.DTOs.TypeRooms;
using ManageHotel.Models;

namespace ManageHotel.Repository.impl
{
    public class RoomRepository : IRoomRepository
    {
        private readonly RoomDAO _dao;
        private readonly BookingDAO _bookingdao;
        private readonly IMapper _mapper;
        public RoomRepository(RoomDAO dao, IMapper mapper, BookingDAO bookingdao)
        {
            _dao = dao;
            _mapper = mapper;
            _bookingdao = bookingdao;
        }

        public void CreateRoom(AddRoomDTO room)
        {
            var r = _mapper.Map<Room>(room);
            _dao.CreateRoom(r);
        }

        public void DeleteRoom(int roomId)
        {
            _dao.DeleteRoom(roomId);
        }

        public List<GetRoomDTO> GetAllRoom()
        {
            var r = _dao.GetAllRooms();
            var room = _mapper.Map<List<GetRoomDTO>>(r);
            for(int i=0; i<r.Count; i++)
            {
                room[i].Hotel= _mapper.Map<GetHotelDTO>(r[i].Hotel);
                room[i].TypeRoom = _mapper.Map<GetTypeRoomDTO>(r[i].TypeRoom);
            }
            return room;
        }

        public List<GetRoomDTO> GetAllRoomUnBooking(DateTime from, DateTime to)
        {
            List<GetRoomDTO> room = GetAllRoom();

            var booking = _bookingdao.GetBookingByDay(from, to);
            var b = _mapper.Map<List<GetBookingDTO>>(booking);
            foreach(var i in b)
            {
                foreach(var j in i.BookingDetails)
                {
                    room.Remove(j.Room);
                }   
            }
            return room;
        }

        public void UpdateRoom(int id, UpdateRoomDTO room)
        {
            var r = _mapper.Map<Room>(room);
            _dao.UpdateRoom(id, r);
        }
    }
}
