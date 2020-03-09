using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookingRoom.Models;

namespace BookingRoom.Controllers
{
    public class BookingContactsController : Controller
    {
        private HouseModel db = new HouseModel();

        // GET: BookingContacts
        public ActionResult Index()
        {
            var bookingContacts = db.BookingContacts.Include(b => b.Room);
            return View(bookingContacts.ToList());
        }

        // GET: BookingContacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingContact bookingContact = db.BookingContacts.Find(id);
            if (bookingContact == null)
            {
                return HttpNotFound();
            }
            return View(bookingContact);
        }

        // GET: BookingContacts/Create
        public ActionResult Create()
        {
            ViewBag.RoomID = new SelectList(db.Room, "RoomID", "Name");
            return View();
        }

        // POST: BookingContacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingContactID,PersonName,Address,PhoneNumber,Email,RoomID,RoomCount,Messeger,Status")] BookingContact bookingContact)
        {
            if (ModelState.IsValid)
            {
                db.BookingContacts.Add(bookingContact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoomID = new SelectList(db.Room, "RoomID", "Name", bookingContact.RoomID);
            return View(bookingContact);
        }

        // GET: BookingContacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingContact bookingContact = db.BookingContacts.Find(id);
            if (bookingContact == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomID = new SelectList(db.Room, "RoomID", "Name", bookingContact.RoomID);
            return View(bookingContact);
        }

        // POST: BookingContacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingContactID,PersonName,Address,PhoneNumber,Email,RoomID,RoomCount,Messeger,Status")] BookingContact bookingContact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingContact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomID = new SelectList(db.Room, "RoomID", "Name", bookingContact.RoomID);
            return View(bookingContact);
        }

        // GET: BookingContacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingContact bookingContact = db.BookingContacts.Find(id);
            if (bookingContact == null)
            {
                return HttpNotFound();
            }
            return View(bookingContact);
        }

        // POST: BookingContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookingContact bookingContact = db.BookingContacts.Find(id);
            db.BookingContacts.Remove(bookingContact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
