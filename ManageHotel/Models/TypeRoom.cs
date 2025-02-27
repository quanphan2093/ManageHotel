using System;
using System.Collections.Generic;

namespace ManageHotel.Models
{
    public partial class TypeRoom
    {
        public TypeRoom()
        {
            Rooms = new HashSet<Room>();
        }

        public int TypeRoomId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
