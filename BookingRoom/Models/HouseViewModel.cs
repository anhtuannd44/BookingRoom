namespace BookingRoom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;


    //public class SearchingViewModel
    //{
    //    public string SearchKey { get; set; }
    //    public string DateCheckIn { get; set; }
    //    public string DateCheckOut { get; set; }
    //    public int RoomCount { get; set; }
    //}
    public class SearchingViewModel
    {
        public string SearchKey { get; set; }
        public string DateCheckIn { get; set; }
        public string DateCheckOut { get; set; }
        public int RoomCount { get; set; }
    }
    public class MinPriceViewModel
    {
        public int HouseID { get; set; }
        public float MinPrice { get; set; }
    }
    public class HouseInfoViewModel
    {
        public int HouseID { get; set; }
        public string DateCheckIn { get; set; }
        public string DateCheckOut { get; set; }
        public int RoomCount { get; set; }
    }
    public class RoomInfoViewModel
    {
        public int RoomID { get; set; }
        public int RoomCount { get; set; }
    }
    public class ServiceHouseViewModel
    {
        public long ServiceID { get; set; }
        public string Name { get; set; }
        public bool IsHouseService { get; set; }
    }
    public class PreOrderViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập Họ và tên")]
        [Display(Name = "Họ và Tên (Như trong CMND, Hộ chiếu)")]
        public string PeopleName { get; set; }

        [Required(ErrorMessage = "Số điện thoại không đúng hoặc không hợp lệ")]
        [Display(Name = "Số Điện Thoại")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Email")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        public string DateCheckIn { get; set; }
        public string DateCheckOut { get; set; }
        public int RoomCount { get; set; }
        public int RoomID { get; set; }
    }
    public class OrderViewModel
    {

        public DateTime DateCheckIn { get; set; }

        public DateTime DateCheckOut { get; set; }

        [Required]
        public string PeopleName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }

        public int RoomCount { get; set; }

        public DateTime TimeBooking { get; set; }

        public float Price { get; set; }

        public float PriceSale { get; set; }

        public float PriceTotal { get; set; }

        public int RelaHotelRoom_ID { get; set; }

        public bool FreeCancel { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
