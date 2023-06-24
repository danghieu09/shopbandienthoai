using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;
// xu lý cac yeu cau 
namespace Ictshop.Areas.Admin.Controllers
{
    public class DonhangsController : Controller
    {
        private Qlbanhang db = new Qlbanhang();

        // GET: Admin/Donhangs
        public ActionResult Index()
        {
            var donhangs = db.Donhangs.Include(d => d.Nguoidung);  //truy vấn cơ sở dữ liệu và lấy danh sách các đơn hàng
            return View(donhangs.ToList());
        }

        // GET: Admin/Donhangs/Details/5  tìm kiếm một đơn hàng với id tương ứng trong cơ sở dữ liệu và trả về một view hiển thị chi tiết của đơn hàng.
        public ActionResult Details(int? id)
        { //xử lý yêu cầu chi tiết đơn hàng
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donhang donhang = db.Donhangs.Find(id);
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return View(donhang);
        }

        // GET: Admin/Donhangs/Create
        public ActionResult Create()
        {
            ViewBag.MaNguoidung = new SelectList(db.Nguoidungs, "MaNguoiDung", "Anhdaidien");
            return View();
        }

        public ActionResult Xacnhan(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donhang donhang = db.Donhangs.Find(id);
            donhang.Tinhtrang = 1;       //xác nhận đơn hàng
            db.SaveChanges();
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }

        // POST: Admin/Donhangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //tạo đơn hàng mới, thêm đơn hàng
        public ActionResult Create([Bind(Include = "Madon,Ngaydat,Tinhtrang,Thanhtoan,MaNguoidung,Diachinhanhang")] Donhang donhang)
        {    //xử lý việc tạo đơn hàng mới
            if (ModelState.IsValid)
            {
                db.Donhangs.Add(donhang);   //thêm đơn hàng
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNguoidung = new SelectList(db.Nguoidungs, "MaNguoiDung", "Anhdaidien", donhang.MaNguoidung);  //hiển thị các tùy chọn trong form của View
            return View(donhang);
        }

        // GET: Admin/Donhangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donhang donhang = db.Donhangs.Find(id);
            if (donhang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNguoidung = new SelectList(db.Nguoidungs, "MaNguoiDung", "Anhdaidien", donhang.MaNguoidung);
            return View(donhang);
        }

        // POST: Admin/Donhangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Madon,Ngaydat,Tinhtrang,Thanhtoan,MaNguoidung,Diachinhanhang")] Donhang donhang)
        {  //xử lý việc chỉnh sửa thông tin đơn hàng,cập nhật đơn hàng 
            if (ModelState.IsValid)
            {
                db.Entry(donhang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNguoidung = new SelectList(db.Nguoidungs, "MaNguoiDung", "Anhdaidien", donhang.MaNguoidung);
            return View(donhang);
        }

        // GET: Admin/Donhangs/Delete/5
        public ActionResult Delete(int? id)
        {    // phương thức Delete trả về một View hiển thị thông tin về đơn hàng cần xóa
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donhang donhang = db.Donhangs.Find(id);
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return View(donhang);
        }

        // POST: Admin/Donhangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {   //xác nhận và xóa đơn hàng
            Donhang donhang = db.Donhangs.Find(id);
            db.Donhangs.Remove(donhang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {   //giải phóng tài nguyên liên quan khi không cần sử dụng đối tượng Controller
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
