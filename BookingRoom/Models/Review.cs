namespace BookingRoom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Review")]
    public partial class Review
    {
        public int ReviewID { get; set; }

        public int Star { get; set; }

        public string Comment { get; set; }

        public string User_ID { get; set; }

        public int RoomID { get; set; }

        public virtual Room Room { get; set; }
    }
}
