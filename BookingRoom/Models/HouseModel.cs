namespace BookingRoom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;

    public class HouseModel : DbContext
    {
        // Your context has been configured to use a 'HouseModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'BookingRoom.Models.HouseModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'HouseModel' 
        // connection string in the application configuration file.
        public HouseModel()
            : base("name=HouseModel")
        {
        }

        public virtual DbSet<Districts> Districts { get; set; }
        public virtual DbSet<House> House { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<PictureHouse> PictureHouse { get; set; }
        public virtual DbSet<PictureRoom> PictureRoom { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Provinces> Provinces { get; set; }
        public virtual DbSet<RelaHotelService> RelaHotelService { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<TypeOfRoom> TypeOfRoom { get; set; }
        public virtual DbSet<Wards> Wards { get; set; }
        public virtual DbSet<Homepage> Homepage { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
        public virtual DbSet<Page> Page { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<BlogCategory> BlogCategory { get; set; }

        public System.Data.Entity.DbSet<BookingRoom.Models.BookingContact> BookingContacts { get; set; }





        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }


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

        [Required]
        [Display(Name = "Xã/Phường/Thị Trấn")]
        public string Ward { get; set; }

        [Required]
        [Display(Name = "Quận/Huyện")]
        public string District { get; set; }

        [Required]
        [Display(Name = "Tỉnh/Thành Phố")]
        public string Province { get; set; }

        [Required]
        [Display(Name = "Hình Thức Thanh Toán")]
        public string Payment { get; set; }

        [Required]
        public string UserID { get; set; }

        public virtual ICollection<PictureHouse> PictureHouse { get; set; }
        public virtual ICollection<RelaHotelService> RelaHotelService { get; set; }
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
        [Display(Name = "Tên  Phòng")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Loại Phòng")]
        public int TypeOfRoomID { get; set; }

        [Required]
        [Display(Name = "Giá Niêm Yết")]
        public float Price { get; set; }

        [Required]
        [Display(Name = "Giảm Giá")]
        public float SalePrice { get; set; }

        [Required]
        [Display(Name = "Trạng Thái Phòng")]
        public bool Status { get; set; }

        [Required]
        [Display(Name = "Nhà Của Bạn")]
        public int HouseID { get; set; }

        [Required]
        [Display(Name = "Diện Tích (m2)")]
        public int Area { get; set; }

        [Required]
        [Display(Name = "Trẻ Em Tối Đa")]
        public int MaxChildren { get; set; }

        [Required]
        [Display(Name = "Hình Thức Cho Thuê")]
        public int RentType { get; set; }

        public virtual House House { get; set; }
        public virtual ICollection<PictureRoom> PictureRoom { get; set; }
        public virtual ICollection<BookingContact> BookingContacts { get; set; }
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


    public class Homepage
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int HomepageID { get; set; }
        public string Hotline { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public string FacebookLink { get; set; }

        public string YoutubeLink { get; set; }

        public string GoogleLink { get; set; }
    }
    public class Slider
    {
        [Key]
        public string SliderID { get; set; }
        public string Title { get; set; }
        [Display(Name = "Nội dung")]
        public string Content { get; set; }
        [Display(Name = "Nội dung Button 1")]
        public string ButtonContent1 { get; set; }
        [Display(Name = "Link Button 1")]
        public string ButtonLink1 { get; set; }
        [Display(Name = "Nội dung Button 2")]
        public string ButtonContent2 { get; set; }
        [Display(Name = "Link Button 2")]
        public string ButtonLink2 { get; set; }
        [Display(Name = "Trạng Thái")]
        public bool Status { get; set; }
    }
    public class Page
    {
        [Key]
        public int PageID { get; set; }
        [Display(Name = "Tiêu Đề")]
        public string Title { get; set; }

        [Display(Name = "Nội Dung")]
        [AllowHtml]
        public string Content { get; set; }
    }
    public class BlogCategory
    {
        [Key]
        public int BlogCategoryID { get; set; }
        [Display(Name = "Tên Chuyên Mục")]
        public string Name { get; set; }
        public virtual ICollection<BlogCategory> BlogCategories { get; set; }
    }
    public class Blog
    {
        [Key]
        public int BlogID { get; set; }
        [Display(Name = "Ảnh Đại Diện")]
        public string Cover { get; set; }
        [Display(Name = "Tiêu Đề")]
        public string Title { get; set; }
        [Display(Name = "Tóm tắt ngắn (2-3 câu)")]
        public string ShortDescription { get; set; }
        [AllowHtml]
        [Display(Name = "Nội Dung")]
        public string Content { get; set; }
        [Required]
        public DateTime TimeModified { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Status { get; set; }
        public int BlogCategoryID { get; set; }
        public virtual BlogCategory BlogCategory { get; set; }
    }
    public class BookingContact
    {
        [Key]
        public int BookingContactID { get; set; }
        [Required]
        [Display(Name = "Họ và Tên như trên CMND/Passport/CCCD")]
        public string PersonName { get; set; }
        [Required]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Điện thoại")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        public int RoomID { get; set; }
        [Required]
        [Display(Name = "Số phòng")]
        public int RoomCount { get; set; }
        [Display(Name = "Ghi chú thêm")]
        public string Messenger { get; set; }
        public bool Status { get; set; }

        public virtual Room Room { get; set; }
    }
}