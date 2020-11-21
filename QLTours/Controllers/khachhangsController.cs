using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLTours.Models;

namespace QLTours.Controllers
{
    public class khachhangsController : Controller
    {
        private QLToursModels db = new QLToursModels();

        // GET: khachhangs
        public ActionResult Index()
        {
            return View(db.khachhangs.ToList());
        }

        // GET: khachhangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khachhang khachhang = db.khachhangs.Find(id);
            if (khachhang == null)
            {
                return HttpNotFound();
            }
            return View(khachhang);
        }

        // GET: khachhangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: khachhangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ten,SDT,NgaySinh,Email,CMND")] khachhang khachhang)
        {
            if (ModelState.IsValid)
            {
                db.khachhangs.Add(khachhang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khachhang);
        }

        // GET: khachhangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khachhang khachhang = db.khachhangs.Find(id);
            if (khachhang == null)
            {
                return HttpNotFound();
            }
            return View(khachhang);
        }

        // POST: khachhangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ten,SDT,NgaySinh,Email,CMND")] khachhang khachhang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachhang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khachhang);
        }

        // GET: khachhangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khachhang khachhang = db.khachhangs.Find(id);
            if (khachhang == null)
            {
                return HttpNotFound();
            }
            return View(khachhang);
        }

        // POST: khachhangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            khachhang khachhang = db.khachhangs.Find(id);
            db.khachhangs.Remove(khachhang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: khachhangs

        public ActionResult tracuuthongtin(string searchString)
        {
            //1. Tạo danh sách danh mục để hiển thị ở giao diện View thông qua DropDownList
            var khachhang = db.khachhangs.Select(s => new {s.Id, s.Ten, s.SDT, s.NgaySinh, s.Email, s.CMND });
            ViewBag.categoryID = new SelectList(khachhang, "Id"); // danh sách Category

            //2. Tạo câu truy vấn kết 2 bảng Link, Category bằng mệnh đề join


            //3. Tìm kiếm chuỗi truy vấn

              var  khachhangs = khachhang.Where(s => s.Ten.Contains(searchString));

            //4. Tìm kiếm theo CategoryID

            //5. Chuyển đổi kết quả từ var sang danh sách List<Link>
            List<khachhang> listkh = new List<khachhang>();
            foreach (var item in khachhangs)
            {
                khachhang temp = new khachhang();
                temp.Ten = item.Ten;
                temp.SDT = item.SDT;
                temp.NgaySinh = item.NgaySinh;
                temp.Email = item.Email;
                temp.CMND = item.CMND;
                listkh.Add(temp);
            }

            return View(listkh);
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
