﻿using ManageHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageHotel.DAO
{
    public class RoomDAO
    {
        private readonly HotelManageContext _context;
        public RoomDAO(HotelManageContext context)
        {
            _context = context;            
        }

        public List<Room> GetAllRooms()
        {
            return _context.Rooms.Include(x => x.Hotel).Include(x => x.TypeRoom).ToList();
        }

        public void CreateRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public void UpdateRoom(int id, Room room)
        {
            var r = _context.Rooms.Find(id);
            if(r != null)
            {
                r.RoomFloor = room.RoomFloor;
                r.RoomNumber = room.RoomNumber;
                r.Price = room.Price;
                r.Quantity= room.Quantity;
                r.Description = room.Description;
                r.IsDeleted = room.IsDeleted;
                r.TypeRoomId = room.TypeRoomId;
                _context.Rooms.Update(r);
                _context.SaveChanges();
            }
        }

        public void DeleteRoom(int id)
        {
            var r = _context.Rooms.Find(id);
            if(r != null)
            {
                r.IsDeleted = true;
                _context.Rooms.Update(r);
                _context.SaveChanges();
            }
        }
    }
}
