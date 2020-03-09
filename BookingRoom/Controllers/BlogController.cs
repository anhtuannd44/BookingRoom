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
    public class BlogController : Controller
    {
        private HouseModel db = new HouseModel();

        // GET: Blog
        public ActionResult Index(int? Category)
        {
            if (Category == null)
            {
                var blog = db.Blog.Include(b => b.BlogCategory).OrderBy(a => a.TimeModified);
                ViewBag.Categories = db.BlogCategory.ToList();
                ViewBag.CategoryTitle = "Tất cả chuyên mục";
                return View(blog.ToList());
            }
            else
            {
                var blog = db.Blog.Where(a => a.BlogCategoryID == Category);
                var category = db.BlogCategory.Find(Category);
                if (blog.Count() == 0)
                {
                    if (category == null)
                    { 
                        return RedirectToAction("CantFindCategory");
                    }
                    ViewBag.Categories = db.BlogCategory.ToList();
                    ViewBag.CategoryTitle = "Chuyên mục: " + category.Name +" chưa có bài viết!";
                    return View(blog.OrderBy(a => a.TimeModified).ToList()); ;
                }
                ViewBag.CategoryTitle = "Chuyên mục: " + blog.FirstOrDefault().BlogCategory.Name;
                ViewBag.Categories = db.BlogCategory.ToList();
                return View(blog.OrderBy(a => a.TimeModified).ToList());
            }
        }

        // GET: Blog/Details/5
        public ActionResult BlogDetails(int? BlogID)
        {
            if (BlogID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(BlogID);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categories = db.BlogCategory.ToList();
            return View(blog);
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
