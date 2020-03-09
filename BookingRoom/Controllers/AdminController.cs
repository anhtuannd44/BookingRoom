using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BookingRoom.Models;

namespace BookingRoom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private HouseModel db = new HouseModel();

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.House.ToList());
        }
        public ActionResult HomeEdit(string noti)
        {
            Homepage homepage = db.Homepage.Find(1);
            if (homepage == null)
            {
                return HttpNotFound();
            }
            ViewBag.Noti = noti;
            return View(homepage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HomeEdit([Bind(Include = "HomepageID,Hotline,Address,Email,FacebookLink,YoutubeLink,GoogleLink")] Homepage homepage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(homepage).State = EntityState.Modified;
                db.SaveChanges();
                var noti = "Cập Nhật Thành Công!";
                return RedirectToAction("Header", "Admin", new { Noti = noti });
            }
            return View(homepage);
        }

        public ActionResult SliderEdit()
        {
            for (int i = 1; i <= 3; i++)
            {
                string fullPath = Request.MapPath("~/Content/Images/Slider/Slider" + i + ".jpg");
                if (System.IO.File.Exists(fullPath))
                {
                    ViewData["Slider" + i] = "Slider" + i + ".jpg";
                }
                else
                {
                    ViewData["Slider" + i] = "default.jpg";
                }
            }
            return View(db.Slider.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SliderEdit([Bind(Include = "SliderID,Title,Content,ButtonContent1,ButtonLink1,ButtonContent2,ButtonLink2,Status")] Slider slider, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    string front = slider.SliderID;
                    string end = System.IO.Path.GetExtension(Picture.FileName);
                    var name = front + end;
                    string fullPath = Request.MapPath("~/Content/Images/Slider/" + name);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                        string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/Slider/"), name);
                        Picture.SaveAs(path);
                    }
                    else
                    {
                        string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/Slider/"), name);
                        Picture.SaveAs(path);
                    }
                }
                db.Entry(slider).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Noti = "Cập nhật thành công!";
            }
            for (int i = 1; i <= 3; i++)
            {
                string fullPath = Request.MapPath("~/Content/Images/Slider/Slider" + i + ".jpg");
                if (System.IO.File.Exists(fullPath))
                {
                    ViewData["Slider" + i] = "Slider" + i + ".jpg";
                }
                else
                {
                    ViewData["Slider" + i] = "default.jpg";
                }
            }
            return View(db.Slider.ToList());
        }

        public ActionResult ListPage()
        {
            var listpage = db.Page.Where(a => a.PageID > 9);
            return View(listpage.ToList());
        }
        public ActionResult CreatePage()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePage([Bind(Include = "PageID,Title,Content")] Page page)
        {
            if (ModelState.IsValid)
            {
                db.Page.Add(page);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(page);
        }

        public ActionResult PageEdit(int? page)
        {
            if (page == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            Page PageEdit = db.Page.Find(page);
            if (PageEdit == null)
            {
                return HttpNotFound();
            }
            return View(PageEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PageEdit([Bind(Include = "PageID,Title,Content")] Page page)
        {
            if (ModelState.IsValid)
            {
                db.Entry(page).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Noti = "Cập nhật thành công!";
            }
            else
            {
                ViewBag.Noti = "Có lỗi xảy ra, vui lòng kiểm tra lại!";
            }
            return View(page);
        }
        public ActionResult DeletePage(int? Page)
        {
            if (Page == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = db.Page.Find(Page);
            if (page == null || page.PageID >= 1 && page.PageID <= 9)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // POST: Page/Delete/5
        [HttpPost, ActionName("DeletePage")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePageConfirmed(int Page)
        {
            Page page = db.Page.Find(Page);
            db.Page.Remove(page);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Blog-Categories
        public ActionResult BlogCategories(string noti)
        {
            ViewBag.Noti = noti;
            return View(db.BlogCategory.ToList());
        }

        public ActionResult CreateBlogCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBlogCategory([Bind(Include = "BlogCategoryID,Name")] BlogCategory blogCategory)
        {
            if (ModelState.IsValid)
            {
                db.BlogCategory.Add(blogCategory);
                db.SaveChanges();
                return RedirectToAction("BlogCategories", "Admin");
            }

            return View(blogCategory);
        }

        public ActionResult EditBlogCategory(int? BlogCategoryID)
        {
            if (BlogCategoryID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogCategory blogCategory = db.BlogCategory.Find(BlogCategoryID);
            if (blogCategory == null)
            {
                return HttpNotFound();
            }
            return View(blogCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBlogCategory([Bind(Include = "BlogCategoryID,Name")] BlogCategory blogCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("BlogCategories", "Admin");
            }
            return View(blogCategory);
        }

        public ActionResult DeleteBlogCategory(int? BlogCategoryID)
        {
            if (BlogCategoryID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogCategory blogCategory = db.BlogCategory.Find(BlogCategoryID);
            if (blogCategory == null)
            {
                return HttpNotFound();
            }
            if (blogCategory.Name == "Chưa Phân Loại")
            {
                return RedirectToAction("BlogCategories", "Admin", new { noti = "Bạn không thể xóa Chuyên Mục này!" });
            }
            return View(blogCategory);
        }
        [HttpPost, ActionName("DeleteBlogCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int BlogCategoryID)
        {
            BlogCategory blogCategory = db.BlogCategory.Find(BlogCategoryID);
            db.BlogCategory.Remove(blogCategory);
            db.SaveChanges();
            return RedirectToAction("BlogCategories", "Admin");
        }
        public ActionResult Blogs()
        {
            var blog = db.Blog.Include(b => b.BlogCategory);
            return View(blog.ToList());
        }
        public ActionResult CreateBlog()
        {
            ViewBag.BlogCategoryID = new SelectList(db.BlogCategory, "BlogCategoryID", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBlog([Bind(Include = "BlogID,Title,ShortDescription,Content,Cover,BlogCategoryID,TimeModified,LastUpdate,Status")] Blog blog, HttpPostedFileBase CoverPicture)
        {
            string CoverAdd;
            blog.TimeModified = DateTime.Now;
            blog.Status = true;
            blog.LastUpdate = blog.TimeModified;
            if (ModelState.IsValid)
            {
                if (CoverPicture != null)
                {
                    string front = RandomString();
                    string end = System.IO.Path.GetExtension(CoverPicture.FileName);
                    var name = front + end;
                    string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/Blog/"), name);
                    CoverPicture.SaveAs(path);
                    CoverAdd = name;
                }
                else
                {
                    CoverAdd = "default.jpg";
                }
                blog.Cover = CoverAdd;
                db.Blog.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Blogs");
            }

            ViewBag.BlogCategoryID = new SelectList(db.BlogCategory, "BlogCategoryID", "Name", blog.BlogCategoryID);
            return View(blog);
        }
        public ActionResult EditBlog(int? BlogID)
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
            ViewBag.BlogCategoryID = new SelectList(db.BlogCategory, "BlogCategoryID", "Name", blog.BlogCategoryID);
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBlog([Bind(Include = "BlogID,Title,ShortDescription,Content,Cover,BlogCategoryID,TimeModified,LastUpdate,Status")] Blog blog, HttpPostedFileBase CoverPicture)
        {
            blog.Status = true;
            blog.LastUpdate = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (CoverPicture != null)
                {
                    string front = RandomString();
                    string end = System.IO.Path.GetExtension(CoverPicture.FileName);
                    var name = front + end;
                    string fullPath = Request.MapPath("~/Content/Images/Blog/" + name);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                        string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/Blog/"), name);
                        CoverPicture.SaveAs(path);
                    }
                    else
                    {
                        string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/Blog/"), name);
                        CoverPicture.SaveAs(path);
                    }
                    blog.Cover = name;
                }
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Blogs");
            }
            ViewBag.BlogCategoryID = new SelectList(db.BlogCategory, "BlogCategoryID", "Name", blog.BlogCategoryID);
            return View(blog);
        }

        public ActionResult DeleteBlog(int? BlogID)
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
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("DeleteBlog")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBlogConfirmed(int BlogID)
        {
            Blog blog = db.Blog.Find(BlogID);
            string fullPath = Request.MapPath("~/Content/Images/Blog/" + blog.Cover);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            db.Blog.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Blogs");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
