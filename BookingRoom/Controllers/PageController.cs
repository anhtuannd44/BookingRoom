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
    public class PageController : Controller
    {
        private HouseModel db = new HouseModel();

        // GET: Page/Details/5
        public ActionResult Index(int? Page)
        {
            if (Page == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = db.Page.Find(Page);
            if (page == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categories = db.BlogCategory.ToList();
            ViewBag.CategoryTitle = "Tất cả chuyên mục";
            return View(page);
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
