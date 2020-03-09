namespace BookingRoom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    public class House
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int HouseID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên Nhà Của Bạn")]
        public string Name { get; set; }

        [StringLength(500)]
        [Display(Name = "Giới Thiệu Ngắn")]
        public string Intro { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Số Điện Thoại")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Số Nhà và Tên Đường")]
        public string Street { get; set; }

        [Display(Name = "Xã/Phường/Thị Trấn")]
        public int Ward { get; set; }

        public virtual ICollection<PictureHouse> PictureHouse { get; set; }
        public virtual ICollection<RelaHotelService> RelaHotelService { get; set; }
        public virtual Wards Wards { get; set; }
        public virtual ICollection<Room> Room { get; set; }
    }


    public class PictureHouse
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Tên Hình")]
        public string Name { get; set; }

        [Required]
        public int HouseID { get; set; }

        [Required]
        public bool IsMainPicture { get; set; }

        public virtual House House { get; set; }
    }


    public class RelaHotelService
    {
        [Key]
        [Column(Order = 1)]
        public int HouseID { get; set; }

        [Key]
        [Column(Order = 2)]
        public long ServiceID { get; set; }

        public virtual House House { get; set; }

        public virtual Service Service { get; set; }
    }


    public class Service
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long ServiceID { get; set; }

        [StringLength(100)]
        [Display(Name = "Tên Dịch Vụ/Tiện Nghi")]
        public string Name { get; set; }

        public virtual ICollection<RelaHotelService> RelaHotelService { get; set; }
    }


    public class Room
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RoomID { get; set; }

        [Required]
        public int TypeOfRoomID { get; set; }

        [Required]
        [Display(Name = "Giá Niêm Yết")]
        public float Price { get; set; }

        [Required]
        [Display(Name = "Giảm Giá")]
        public float SalePrice { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Trạng Thái Phòng")]
        public string Status { get; set; }

        [Required]
        public int HouseID { get; set; }

        [Required]
        [Display(Name = "Diện Tích")]
        public int Area { get; set; }

        public virtual House House { get; set; }
        public virtual ICollection<PictureRoom> PictureRoom { get; set; }
        public virtual TypeOfRoom TypeOfRoom { get; set; }
    }

    public class TypeOfRoom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TypeOfRoomID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên Loại Phòng")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Số Giường Đơn")]
        public int SingleBed { get; set; }

        [Required]
        [Display(Name = "Số Giường Đôi")]
        public int TwinBed { get; set; }

        [Required]
        [Display(Name = "Số Giường DOM")]
        public int DOMBed { get; set; }

        [Required]
        [Display(Name = "Trẻ Em Tối Đa")]
        public int ChildrenMax { get; set; }

        public virtual ICollection<Room> Room { get; set; }
    }

    public class PictureRoom
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PictureRoomID { get; set; }

        [Required]
        [Display(Name = "Tên Hình")]
        public string Name { get; set; }

        [Required]
        public int RoomID { get; set; }

        [Required]
        public bool IsMainPicture { get; set; }

        public virtual Room Room { get; set; }
    }

}
