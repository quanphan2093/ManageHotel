using System;
using System.Collections.Generic;

namespace ManageHotel.Models
{
    public partial class RoomImage
    {
        public int RoomImagesId { get; set; }
        public string ImageName { get; set; } = null!;
        public int RoomId { get; set; }

        public virtual Room Room { get; set; } = null!;
    }
}
