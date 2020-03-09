using BookingRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookingRoom.Controllers
{
    public class OrderController : Controller
    {
        private readonly HouseModel db = new HouseModel();

        public ActionResult RoomBookingInfo(int? RoomID)
        {
            var room = db.Room.Find(RoomID);
            ViewBag.Room = room;
            ViewBag.HotelCover = db.PictureHouse.Where(a => a.HouseID == room.HouseID && a.IsMainPicture == true).FirstOrDefault().Name;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoomBookingInfo([Bind(Include = "BookingContactID,PersonName,Address,PhoneNumber,Email,RoomID,RoomCount,Messeger,Status")] BookingContact bookingContact)
        {
            if (ModelState.IsValid)
            {
                db.BookingContacts.Add(bookingContact);
                db.SaveChanges();
                return RedirectToAction("Success", "Order", new { bookingContactID = bookingContact.BookingContactID });
            }
            var room = db.Room.Find(bookingContact.RoomID);
            ViewBag.Room = room;
            ViewBag.HotelCover = db.PictureHouse.Where(a => a.HouseID == room.HouseID && a.IsMainPicture == true).FirstOrDefault().Name;
            return View();
        }
        [HttpGet]
        public ActionResult Success(int bookingContactID)
        {
            ViewBag.Categories = db.BlogCategory.ToList();
            return View(db.BookingContacts.Find(bookingContactID));
        }
    }
}