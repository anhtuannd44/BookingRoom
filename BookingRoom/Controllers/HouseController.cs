using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BookingRoom.Models;
using Microsoft.AspNet.Identity;

namespace BookingRoom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HouseController : Controller
    {
        private readonly HouseModel db = new HouseModel();

        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Houses()
        {
            var UserID = User.Identity.GetUserId();
            var model = db.House.Where(a => a.UserID == UserID);
            if (model.Count() == 0)
            {
                ViewBag.Noti = "Bạn chưa có nhà nào, thêm nhà của bạn ngay nào!";
            }
            List<PictureHouse> data = new List<PictureHouse>();
            ViewBag.Picture = "";
            foreach (var item in model)
            {
                if (db.PictureHouse.Where(a => a.HouseID == item.HouseID && a.IsMainPicture == true).Count() != 0)
                {
                    data.Add(db.PictureHouse.Where(a => a.HouseID == item.HouseID && a.IsMainPicture == true).FirstOrDefault());
                }
            }
            if (data.Count() != 0)
                ViewBag.Picture = data;
            return View(model.ToList());
        }
        public ActionResult CreateHouse()
        {
            ViewBag.UserID = User.Identity.GetUserId();
            ViewBag.Province = new SelectList(db.Provinces, "Name", "Name");
            return View();
        }

        // POST: Houses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateHouse([Bind(Include = "HouseID,Name,Intro,PhoneNumber,Street,Province,District,Ward,UserID,Payment")] House house)
        {
            if (ModelState.IsValid)
            {
                var UserID = User.Identity.GetUserId();
                house.UserID = UserID;
                house.Payment = "Cash";
                db.House.Add(house);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Province = new SelectList(db.Provinces, "Name", "Name");
            return View();
        }

        public JsonResult GetProvince()
        {
            var listProvince = db.Provinces.ToList().ToArray().Select(s => new { name = s.Name });
            return Json(listProvince, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDistrict(string province)
        {
            var provinceid = db.Provinces.Where(a => a.Name == province).FirstOrDefault().Id;
            var District = db.Districts.Where(a => a.ProvinceId == provinceid).ToList().ToArray();
            var listDistrict = District.Select(s => new { name = s.Name });
            return Json(listDistrict, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetWard(string district)
        {
            var districtid = db.Districts.Where(a => a.Name == district).FirstOrDefault().Id;
            var Ward = db.Wards.Where(a => a.DistrictID == districtid).ToList().ToArray();
            var listWard = Ward.Select(s => new { name = s.Name });
            return Json(listWard, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HouseDetail(int? HouseID)
        {
            if (HouseID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                House house = db.House.Find(HouseID);

                var Picture = db.PictureHouse.Where(a => a.HouseID == HouseID);
                if (Picture.Count() >= 1)
                {
                    ViewBag.Picture = Picture.ToList();
                    ViewBag.MainPicture = Picture.Where(a => a.IsMainPicture == true).FirstOrDefault().Name;
                }
                else
                {
                    ViewBag.MainPicture = "default.jpg";
                }
                if (house == null)
                {
                    return HttpNotFound();
                }
                return View(house);
            }
        }

        public ActionResult HouseEdit(int? HouseID)
        {
            if (HouseID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            House house = db.House.Find(HouseID);
            if (house == null)
            {
                return HttpNotFound();
            }
            return View(house);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HouseEdit([Bind(Include = "HouseID,Name,Intro,PhoneNumber,Street,Ward,District,Province,UserID")] House house)
        {
            var UserID = User.Identity.GetUserId();
            house.UserID = UserID;
            if (ModelState.IsValid)
            {
                db.Entry(house).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HouseDetail", "House", new { HouseID = house.HouseID });
            }
            return View(house);
        }
        public ActionResult HouseService(int? HouseID)
        {
            var House = db.House.Find(HouseID);
            if (House == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.HouseID = HouseID;
            var Service = db.Service.ToList();
            ViewBag.RelaHouseService = db.RelaHotelService.ToList().Where(a => a.HouseID == HouseID);

            List<ServiceHouseViewModel> ServiceHouse = new List<ServiceHouseViewModel>();

            foreach (var item in Service)
            {
                if (db.RelaHotelService.Where(a => a.ServiceID == item.ServiceID && a.HouseID == HouseID).Count() != 0)
                {
                    ServiceHouse.Add(new ServiceHouseViewModel { ServiceID = item.ServiceID, Name = item.Name, IsHouseService = true });
                }
                else
                {
                    ServiceHouse.Add(new ServiceHouseViewModel { ServiceID = item.ServiceID, Name = item.Name, IsHouseService = false });
                }
            }

            return View(ServiceHouse);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateHouseService(int HouseID)
        {
            foreach (var item in db.Service)
            {
                var data = Request.Form[item.ServiceID.ToString()];
                if (data == "true")
                {
                    if (db.RelaHotelService.Where(a => a.HouseID == HouseID && a.ServiceID == item.ServiceID).Count() != 1)
                    {
                        RelaHotelService AddService = new RelaHotelService
                        {
                            HouseID = HouseID,
                            ServiceID = item.ServiceID
                        };
                        db.RelaHotelService.Add(AddService);

                    }
                }
                else
                {
                    var RemoveData = db.RelaHotelService.Where(a => a.HouseID == HouseID && a.ServiceID == item.ServiceID);
                    if (RemoveData.Count() == 1)
                    {
                        db.RelaHotelService.Remove(RemoveData.FirstOrDefault());
                    }
                }
            }
            db.SaveChanges();
            return RedirectToAction("HouseService", "House", new { HouseID = HouseID });
        }
        public ActionResult HousePicture(int? HouseID, string Noti)
        {
            if (HouseID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            House house = db.House.Find(HouseID);
            if (house == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseID = HouseID;
            var HousePicture = db.PictureHouse.Where(a => a.HouseID == HouseID);
            if (HousePicture.Count() == 0)
            {
                ViewBag.MainPicture = "default.jpg";
            }
            else
            {
                ViewBag.MainPicture = HousePicture.Where(a => a.IsMainPicture == true).FirstOrDefault().Name;
            }
            ViewBag.Noti = Noti;
            ViewBag.MinPictureUpload = 6 - HousePicture.Count();
            ViewBag.HouseID = HouseID;
            return View(HousePicture.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImgHouse(HttpPostedFileBase imageUpload, int HouseID)
        {
            string Noti = "Đăng Hình Thành công!";
            if (imageUpload != null)
            {
                string front = RandomString();
                string end = System.IO.Path.GetExtension(imageUpload.FileName);
                var name = front + end;
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/Hotel/"), name);
                imageUpload.SaveAs(path);

                var PictureDb = db.PictureHouse.Where(a => a.HouseID == HouseID);
                if (PictureDb.Count() < 6)
                {
                    PictureHouse housepicture = new PictureHouse();
                    housepicture.HouseID = HouseID;
                    housepicture.Name = name;
                    db.PictureHouse.Add(housepicture);

                    var CheckMainPicture = PictureDb.FirstOrDefault();
                    if (CheckMainPicture == null)
                    {
                        housepicture.IsMainPicture = true;
                    }
                    db.SaveChanges();
                }
                else
                {
                    Noti = "Đã quá số hình cho phép!";
                }
            }
            else
                Noti = "Bạn chưa chọn hình để đăng!";
            return RedirectToAction("HousePicture", "House", new { houseid = HouseID, Noti = Noti });
        }

        public ActionResult SetMainPicture(int? HouseID, int? PictureID)
        {
            if ((HouseID == null) || (PictureID == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var PictureHouse = db.PictureHouse.Where(a => a.HouseID == HouseID && a.IsMainPicture == true).ToList();
            foreach (var item in PictureHouse)
            {
                db.PictureHouse.Find(item.ID).IsMainPicture = false;
            }
            db.PictureHouse.Find(PictureID).IsMainPicture = true;
            db.SaveChanges();
            return RedirectToAction("HousePicture", "House", new { houseid = HouseID });
        }
        public ActionResult DeletePictureHouse(int? HouseID, int? PictureID)
        {
            if ((HouseID == null) || (PictureID == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var PictureHouse = db.PictureHouse;
            var DelPicture = PictureHouse.Find(PictureID);

            string fullPath = Request.MapPath("~/Content/Images/Hotel/" + DelPicture.Name);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            if (DelPicture.IsMainPicture == false)
            {
                db.PictureHouse.Remove(DelPicture);
            }
            else if (PictureHouse.Where(a => a.HouseID == HouseID).Count() == 1)
            {
                db.PictureHouse.Remove(DelPicture);
            }
            else
            {
                var SetNewMainPicture = db.PictureHouse.Where(a => a.HouseID == HouseID && a.IsMainPicture == false).FirstOrDefault();
                SetNewMainPicture.IsMainPicture = true;
                db.Entry(SetNewMainPicture).State = EntityState.Modified;
                db.PictureHouse.Remove(DelPicture);
            }
            db.SaveChanges();
            return RedirectToAction("HousePicture", "House", new { houseid = HouseID });
        }

        public ActionResult Rooms(int? HouseID)
        {
            if (HouseID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.Room.Where(a => a.HouseID == HouseID);
            if (model.Count() == 0)
            {
                ViewBag.Noti = "Bạn chưa tạo phòng nào, thêm phòng ngay thôi nào!";
            }
            List<PictureRoom> data = new List<PictureRoom>();
            foreach (var item in model)
            {
                if (db.PictureRoom.Where(a => a.RoomID == item.RoomID && a.IsMainPicture == true) != null)
                {
                    data.Add(db.PictureRoom.Where(a => a.RoomID == item.RoomID && a.IsMainPicture == true).FirstOrDefault());
                }
            }
            ViewBag.Picture = data;
            ViewBag.Picture = db.PictureRoom;
            ViewBag.HouseID = HouseID;
            return View(model.ToList());
        }
        public ActionResult RoomDetail(int? RoomID)
        {
            if (RoomID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Room.Find(RoomID);

            var Picture = db.PictureRoom.Where(a => a.RoomID == RoomID);
            if (Picture.Count() >= 1)
            {
                ViewBag.Picture = Picture.ToList();
                ViewBag.MainPicture = Picture.Where(a => a.IsMainPicture == true).FirstOrDefault().Name;
            }
            else
            {
                ViewBag.MainPicture = "default.jpg";
            }
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: House/CreateRoom
        public ActionResult CreateRoom(int? HouseID)
        {
            if (HouseID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.HouseID = HouseID;
            ViewBag.TypeOfRoomID = new SelectList(db.TypeOfRoom, "TypeOfRoomID", "Name");
            ViewBag.HouseName = db.House.Find(HouseID).Name;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRoom([Bind(Include = "RoomID,Name,TypeOfRoomID,Price,SalePrice,Status,HouseID,Area,MaxChildren,RentType")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Room.Add(room);
                db.SaveChanges();
                return RedirectToAction("Rooms", "House", new { room.HouseID });
            }

            ViewBag.HouseID = new SelectList(db.House, "HouseID", "Name", room.HouseID);
            ViewBag.TypeOfRoomID = new SelectList(db.TypeOfRoom, "TypeOfRoomID", "Name", room.TypeOfRoomID);
            return View(room);
        }
        public ActionResult RoomEdit(int? RoomID)
        {
            if (RoomID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Room.Find(RoomID);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeOfRoomID = new SelectList(db.TypeOfRoom, "TypeOfRoomID", "Name", room.TypeOfRoomID);
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoomEdit([Bind(Include = "RoomID,Name,TypeOfRoomID,Price,SalePrice,Status,HouseID,Area,MaxChildren")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("RoomDetail", "House", new { RoomID = room.RoomID });
            }
            ViewBag.TypeOfRoomID = new SelectList(db.TypeOfRoom, "TypeOfRoomID", "Name", room.TypeOfRoomID);
            return View(room);
        }

        public ActionResult RoomPicture(int? RoomID, string Noti)
        {
            if (RoomID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Room.Find(RoomID);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomID = RoomID;
            var RoomPicture = db.PictureRoom.Where(a => a.RoomID == RoomID);
            if (RoomPicture.Count() == 0)
            {
                ViewBag.MainPicture = "default.jpg";
                ViewBag.IsPictureRooom = "0";
            }
            else
            {
                ViewBag.MainPicture = RoomPicture.Where(a => a.RoomID==RoomID && a.IsMainPicture == true).FirstOrDefault().Name;
                ViewBag.IsPictureRooom = "1";
            }
            ViewBag.Noti = Noti;
            ViewBag.MinPictureUpload = 6 - RoomPicture.Count();
            ViewBag.RoomID = RoomID;
            return View(RoomPicture.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImgRoom(HttpPostedFileBase imageUpload, int RoomID)
        {
            string Noti = "Đăng Hình Thành công!";
            if (imageUpload != null)
            {
                string front = RandomString();
                string end = System.IO.Path.GetExtension(imageUpload.FileName);
                var name = front + end;
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/Room/"), name);
                imageUpload.SaveAs(path);

                var PictureDb = db.PictureRoom.Where(a => a.RoomID == RoomID);
                if (PictureDb.Count() < 6)
                {
                    PictureRoom roompicture = new PictureRoom();
                    roompicture.RoomID = RoomID;
                    roompicture.Name = name;
                    db.PictureRoom.Add(roompicture);

                    var CheckMainPicture = PictureDb.FirstOrDefault();
                    if (CheckMainPicture == null)
                    {
                        roompicture.IsMainPicture = true;
                    }
                    db.SaveChanges();

                }
                else
                {
                    Noti = "Đã quá số hình cho phép!";
                }

            }
            else
                Noti = "Bạn chưa chọn hình để đăng";
            return RedirectToAction("RoomPicture", "House", new { RoomID = RoomID, Noti = Noti });
        }

        public ActionResult SetMainPictureRoom(int? RoomID, int? PictureID)
        {
            if ((RoomID == null) || (PictureID == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var PictureHouse = db.PictureRoom.Where(a => a.RoomID == RoomID && a.IsMainPicture == true).ToList();
            foreach (var item in PictureHouse)
            {
                db.PictureRoom.Find(item.PictureRoomID).IsMainPicture = false;
            }
            db.PictureRoom.Find(PictureID).IsMainPicture = true;
            db.SaveChanges();
            return RedirectToAction("RoomPicture", "House", new { RoomID = RoomID });
        }
        public ActionResult DeletePictureRoom(int? RoomID, int? PictureID)
        {
            if ((RoomID == null) || (PictureID == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var RoomPicture = db.PictureRoom;
            var DelPicture = RoomPicture.Find(PictureID);

            string fullPath = Request.MapPath("~/Content/Images/Room/" + DelPicture.Name);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            if ((DelPicture.IsMainPicture == false) || (RoomPicture.Where(a => a.RoomID == RoomID).Count() == 1))
            {
                db.PictureRoom.Remove(DelPicture);
            }
            else
            {
                db.PictureRoom.Where(a => a.RoomID == RoomID && a.IsMainPicture != true).FirstOrDefault().IsMainPicture = true;
                db.PictureRoom.Remove(DelPicture);

            }
            db.SaveChanges();
            return RedirectToAction("RoomPicture", "House", new { RoomID = RoomID });
        }


        private string RandomString()
        {
            var size = 15;
            StringBuilder sb = new StringBuilder();
            char c;
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                c = Convert.ToChar(Convert.ToInt32(rand.Next(65, 90)));
                sb.Append(c);
            }
            return sb.ToString();
        }

    }
}
