using BookingRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Data.Entity;

namespace BookingRoom.Controllers
{
    public class HomeController : Controller
    {
        readonly HouseModel db = new HouseModel();
        readonly ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.Slider = db.Slider.Where(a => a.Status == true).ToList();
            return View();
        }

        public ActionResult Houses(string location)
        {
            List<House> House = new List<House>();
            if (!String.IsNullOrEmpty(location))
            {
                House = db.House.Where(a => a.Province == location).ToList();
            }
            else
            {
                House = db.House.ToList();
            }

            if (House == null)
            {
                return HttpNotFound();
            }
            List<RelaHotelService> service = new List<RelaHotelService>();
            List<MinPriceViewModel> roomprice = new List<MinPriceViewModel>();
            List<PictureHouse> picturehouse = new List<PictureHouse>();

            foreach (var item in House.ToList())
            {
                var RoomInHouse = db.Room.Where(a => a.HouseID == item.HouseID);
                if (RoomInHouse.Count() != 0)
                {
                    var minprice = RoomInHouse.OrderBy(a => a.Price).FirstOrDefault().Price;
                    roomprice.Add(new MinPriceViewModel { HouseID = item.HouseID, MinPrice = minprice });

                    var ServiceHouse = db.RelaHotelService.Where(a => a.HouseID == item.HouseID).Include(a => a.Service).ToList();
                    service.AddRange(ServiceHouse);

                    var picture = db.PictureHouse.Where(a => a.HouseID == item.HouseID).ToList();
                    picturehouse.AddRange(picture);
                }
            }

            //var Result = db.House;
            //ViewBag.ResultCount = Result.Count();
            //ResultFinal = Result.ToList();
            //List<RelaHotelService> service = new List<RelaHotelService>();
            //List<MinPriceViewModel> RoomPrice = new List<MinPriceViewModel>();
            //List<PictureHouse> picturehouse = new List<PictureHouse>();
            //foreach (var item in ResultFinal.ToList())
            //{
            //    var RoomInHouse = db.Room.Where(a => a.HouseID == item.HouseID);
            //    if (RoomInHouse.Count() != 0)
            //    {
            //        var minprice = RoomInHouse.OrderBy(a => a.Price).FirstOrDefault().Price;
            //        RoomPrice.Add(new MinPriceViewModel { HouseID = item.HouseID, MinPrice = minprice });

            //        var ServiceHouse = db.RelaHotelService.Where(a => a.HouseID == item.HouseID).Include(a => a.Service).ToList();
            //        service.AddRange(ServiceHouse);

            //        var picture = db.PictureHouse.Where(a => a.HouseID == item.HouseID).ToList();
            //        picturehouse.AddRange(picture);
            //    }
            //    else
            //    {
            //        var HouseRemove = db.House.Find(item.HouseID);
            //        ResultFinal.Remove(HouseRemove);
            //    }
            //}
            ViewBag.ServiceCheck = true;
            ViewBag.HousePictureCheck = true;
            ViewBag.MinPriceCheck = true;
            if (service.Count() == 0)
            {
                ViewBag.ServiceCheck = false;
            }
            if (picturehouse.Count() == 0)
            {
                ViewBag.HousePictureCheck = false;
            }
            if (roomprice.Count() == 0)
            {
                ViewBag.HousePictureCheck = false;
            }

            ViewBag.Service = service;
            ViewBag.MinPrice = roomprice.OrderBy(a => a.MinPrice);
            ViewBag.PictureHouse = picturehouse;
            ViewBag.Categories = db.BlogCategory.ToList();
            ViewBag.ResultCount = House.Count();
            return View(House);
        }

        public ActionResult HouseInfo(HouseInfoViewModel houseinfo)
        {
            var house = db.Room.Where(a => a.HouseID == houseinfo.HouseID);
            var HouseDetail = db.House.Find(houseinfo.HouseID);
            if (HouseDetail == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<PictureRoom> RoomPicture = new List<PictureRoom>();
            foreach (var item in house)
            {
                RoomPicture.AddRange(db.PictureRoom.Where(a => a.RoomID == item.RoomID));
            }
            if (RoomPicture.Count() == 0)
            {
                ViewBag.RoomPicture = null;
            }
            else
            {
                ViewBag.RoomPicture = RoomPicture.ToList();
            }
            ViewBag.HousePicture = db.PictureHouse.Where(a => a.HouseID == houseinfo.HouseID).ToList();
            ViewBag.HouseInfo = HouseDetail;
            ViewBag.Service = db.RelaHotelService.Where(a => a.HouseID == houseinfo.HouseID).Include(a => a.Service).Take(4).ToList();
            ViewBag.MinPrice = house.OrderBy(a => a.Price).FirstOrDefault().RoomID;
            ViewBag.HouseInfoViewModel = houseinfo;
            return View(house.OrderBy(a => a.Price).ToList());
        }

        //public ActionResult SearchingResult()
        //{
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public ActionResult SearchingResult(SearchingViewModel SVM, string err)
        //{
        //    if (err == "Lỗi")
        //    {
        //        ViewData["Lỗi"] = "Lỗi";
        //        return View();
        //    }
        //    else
        //    {
        //        ViewData["Lỗi"] = "";
        //        DateTime checkin = DateTime.Parse("12/12/2019");
        //        DateTime checkout = DateTime.Parse("12/12/2019");
        //        if (checkin > checkout)
        //        {
        //            return RedirectToAction("SearchingResult", "Home", new { err = "Lỗi" });
        //        }
        //        if (checkin == checkout)
        //            checkout = checkout.AddDays(1);


        //        var Result = db.House.Where(a => a.Province.Contains(SVM.SearchKey) || a.District.Contains(SVM.SearchKey));

        //        ViewBag.ResultCount = Result.Count();
        //        var ResultFinal = Result.ToList();
        //        List<RelaHotelService> service = new List<RelaHotelService>();
        //        List<MinPriceViewModel> RoomPrice = new List<MinPriceViewModel>();
        //        List<PictureHouse> picturehouse = new List<PictureHouse>();
        //        foreach (var item in ResultFinal.ToList())
        //        {
        //            var RoomInHouse = db.Room.Where(a => a.HouseID == item.HouseID);
        //            if (RoomInHouse.Count() != 0)
        //            {
        //                var minprice = RoomInHouse.OrderBy(a => a.Price).FirstOrDefault().Price;
        //                RoomPrice.Add(new MinPriceViewModel { HouseID = item.HouseID, MinPrice = minprice });

        //                var ServiceHouse = db.RelaHotelService.Where(a => a.HouseID == item.HouseID).Include(a => a.Service).ToList();
        //                service.AddRange(ServiceHouse);

        //                var picture = db.PictureHouse.Where(a => a.HouseID == item.HouseID).ToList();
        //                picturehouse.AddRange(picture);
        //            }
        //            else
        //            {
        //                var HouseRemove = db.House.Find(item.HouseID);
        //                ResultFinal.Remove(HouseRemove);
        //            }
        //        }
        //        ViewBag.House = ResultFinal;
        //        ViewBag.Service = service;
        //        ViewBag.MinPrice = RoomPrice.OrderBy(a => a.MinPrice);
        //        ViewBag.PictureHouse = picturehouse;
        //        return View(SVM);
        //    }
        //}

        //public ActionResult HouseInfo(HouseInfoViewModel houseinfo)
        //{
        //    var house = db.Room.Where(a => a.HouseID == houseinfo.HouseID);
        //    var HouseDetail = db.House.Find(houseinfo.HouseID);
        //    if (HouseDetail == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    List<PictureRoom> RoomPicture = new List<PictureRoom>();
        //    foreach (var item in house)
        //    {
        //        RoomPicture.AddRange(db.PictureRoom.Where(a => a.RoomID == item.RoomID));
        //    }
        //    if (RoomPicture.Count() == 0)
        //    {
        //        ViewBag.RoomPicture = null;
        //    }
        //    else
        //    {
        //        ViewBag.RoomPicture = RoomPicture.ToList();
        //    }
        //    ViewBag.HousePicture = db.PictureHouse.Where(a => a.HouseID == houseinfo.HouseID).ToList();
        //    ViewBag.HouseInfo = HouseDetail;
        //    ViewBag.Service = db.RelaHotelService.Where(a => a.HouseID == houseinfo.HouseID).Include(a => a.Service).Take(4).ToList();
        //    ViewBag.MinPrice = house.OrderBy(a => a.Price).FirstOrDefault().RoomID;
        //    ViewBag.HouseInfoViewModel = houseinfo;
        //    return View(house.OrderBy(a => a.Price).ToList());
        //}
        //public PartialViewResult SearchAjax(SearchingViewModel svm, string sort)
        //{
        //    DateTime checkin = DateTime.Parse(svm.CheckIn);
        //    DateTime checkout = DateTime.Parse(svm.CheckOut);
        //    List<Room> Result = new List<Room>();
        //    System.Threading.Thread.Sleep(500);
        //    if (checkin > checkout)
        //    {
        //        ViewBag.DateErr = "Lỗi: Ngày nhận phòng phải trước ngày trả phòng";
        //        Result = null;
        //    }
        //    else
        //    {
        //        ViewBag.DateErr = null;
        //        if (checkin == checkout)
        //            svm.CheckOut = checkout.AddDays(1).ToString("dd/MM/yyyy");

        //        ViewBag.SearchKey = svm.SearchKey;
        //        ViewBag.RoomCount = svm.RoomCount;
        //        ViewBag.CheckIn = svm.CheckIn.ToString();
        //        ViewBag.CheckOut = svm.CheckOut.ToString();


        //        //Danh sách phòng phù hợp với SearchKey
        //        Result = db.Room.Include(a => a.House).Where(a => a.House.Ward.Contains(svm.SearchKey)).ToList();
        //        string[] HotelInfo = new string[Result.Count() + 1];
        //        int i = 1;
        //        HotelInfo[0] = "";
        //        //Danh sách phòng phù hợp SearchKey + DateTime + Số Lượng
        //        foreach (var item in Result.OrderBy(a => a.Hotel_ID).ToList())
        //        {
        //            int roomcount = db.Room.Find(item.RelaHotelRoom_ID).Room_Count;

        //            //Danh sách không hợp lệ + Thêm mảng danh sách Hotel
        //            var IsRoomBooked = db.Order.Where(a => a.RelaHotelRoom_ID == item.RelaHotelRoom_ID
        //            &&
        //            ((a.Order_CheckIn <= checkin && a.Order_CheckOut > checkin)
        //            ||
        //            (a.Order_CheckIn > checkin && a.Order_CheckIn < checkout)
        //            ));
        //            foreach (var checkroom in IsRoomBooked.ToList())
        //            {
        //                roomcount -= checkroom.Order_RoomCount;
        //            }

        //            if ((roomcount <= 0) || (roomcount < svm.RoomCount))
        //            {
        //                var delrela = db.Room.Find(item.RelaHotelRoom_ID);
        //                Result.Remove(delrela);
        //            }
        //            else
        //            {
        //                item.Room_Count = roomcount;
        //                if (HotelInfo[i - 1] != item.Hotel_ID)
        //                {
        //                    HotelInfo[i] = item.House.HouseID;
        //                    i++;
        //                }
        //            }

        //        }

        //        //Giữ lại phần tử có giá Khuyến Mãi nhỏ nhất
        //        //if (i > 1 )
        //        {
        //            for (int j = 1; j < i; j++)
        //            {
        //                int MinPriceRoomID = Result.Where(a => a.HouseID == HotelInfo[j]).OrderBy(a => a.Room_PriceSale).FirstOrDefault().RelaHotelRoom_ID;
        //                var NotMinPriceRoom = Result.Where(a => a.RoomID != MinPriceRoomID && a.HouseID == HotelInfo[j]);
        //                if (NotMinPriceRoom != null)
        //                {
        //                    foreach (var item in NotMinPriceRoom.ToList())
        //                    {
        //                        Result.Remove(item);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return PartialView(Result);

        //}

        //public ActionResult HotelInfo(SearchingViewModel svm, string HotelID)
        //{
        //    DateTime checkin = Convert.ToDateTime(svm.CheckIn);
        //    DateTime checkout = Convert.ToDateTime(svm.CheckOut);

        //    var HotelInfo = db.Room.Include(a => a.Rooms).Where(a => a.Hotel_ID == HotelID).Include(a => a.House).ToList();

        //    foreach (var item in HotelInfo.ToList())
        //    {
        //        int roomcount = db.Room.Find(item.RelaHotelRoom_ID).Room_Count;

        //        //Danh sách không hợp lệ + Thêm mảng danh sách Hotel
        //        var IsRoomBooked = db.Order.Where(a => a.RelaHotelRoom_ID == item.RelaHotelRoom_ID
        //        &&
        //        ((a.Order_CheckIn <= checkin && a.Order_CheckOut > checkin)
        //        ||
        //        (a.Order_CheckIn > checkin && a.Order_CheckIn < checkout)
        //        ));
        //        foreach (var checkroom in IsRoomBooked.ToList())
        //        {
        //            roomcount -= checkroom.Order_RoomCount;
        //        }
        //        if (roomcount <= 0)
        //        {
        //            var delrela = db.Room.Find(item.RelaHotelRoom_ID);
        //            HotelInfo.Remove(delrela);
        //        }
        //        else
        //        {
        //            item.Room_Count = roomcount;
        //        }
        //    }

        //    ViewBag.CheckIn = svm.CheckIn.ToString();
        //    ViewBag.CheckOut = svm.CheckOut.ToString();
        //    ViewBag.ServiceList = db.RelaHotelService.Where(a => a.Hotel_ID == HotelID).Include(a => a.Service).Take(5).OrderBy(p => Guid.NewGuid());
        //    ViewBag.ImageList = db.PictureHouse.Where(a => a.Hotel_ID == HotelID);
        //    return View(HotelInfo.OrderBy(a => a.Room_PriceSale));
        //}


        //[HttpGet]
        //public ActionResult LoginForBooking(BookingViewModel bvm)
        //{
        //    Session["ReturnUrl"] = "/OrderDetail?HotelID=" + bvm.HotelID + "&&CheckIn=" + bvm.CheckIn + "&&CheckOut=" + bvm.CheckOut + "&&RoomCount=" + bvm.RoomCount + "&&RoomID=" + bvm.RoomID;


        //    Session["OrderSession"] = new List<PreOrderViewModel>();
        //    List<PreOrderViewModel> ordses = Session["OrderSession"] as List<PreOrderViewModel>;

        //    PreOrderViewModel prenew = new PreOrderViewModel
        //    {
        //        PreOrd_ID = 1,
        //        PreOrd_CheckIn = bvm.CheckIn,
        //        PreOrd_CheckOut = bvm.CheckOut,
        //        PreOrd_HotelID = bvm.HotelID,
        //        PreOrd_RoomCount = bvm.RoomCount,
        //        PreOrd_RoomID = bvm.RoomID,
        //    };
        //    ordses.Add(prenew);


        //    if (!Request.IsAuthenticated)
        //    {
        //        ViewBag.ReturnUrl = Session["ReturnUrl"];
        //        return View();
        //    }
        //    else
        //        return RedirectToAction("Index", "OrderDetail");
        //}




        private string ConvertToUnSign(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }
    }
}