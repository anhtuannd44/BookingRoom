namespace BookingRoom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [Key]
        public int Order_ID { get; set; }

        public string UserID { get; set; }

        public DateTime Order_CheckIn { get; set; }

        public DateTime Order_CheckOut { get; set; }

        [Required]
        public string Order_PeopleName { get; set; }

        [Required]
        public string Order_PhoneNumber { get; set; }

        public int Order_RoomCount { get; set; }

        public DateTime Order_TimeBooking { get; set; }

        [Required]
        public string Order_Status { get; set; }

        [Required]
        public string Order_Email { get; set; }

        [Required]
        public string Order_Payment { get; set; }

        public float Order_Price { get; set; }

        public float Order_PriceSale { get; set; }

        public float Order_PriceTotal { get; set; }

        public int RelaHotelRoom_ID { get; set; }

        public bool Order_FreeCancel { get; set; }

        public DateTime Order_LastUpdate { get; set; }

        //public virtual Room Room { get; set; }
    }
}
