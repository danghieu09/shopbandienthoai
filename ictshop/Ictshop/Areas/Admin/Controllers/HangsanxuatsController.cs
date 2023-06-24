using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;

namespace Ictshop.Areas.Admin.Controllers
{
    public class HangsanxuatsController : Controller
    {    // hiển thị danh sách các đối tượng "Hangsanxuat" lấy từ cơ sở dữ liệu
        private Qlbanhang db = new Qlbanhang();

        // GET: Admin/Hangsanxuats
        public ActionResult Index()
        {
            return View(db.Hangsanxuats.ToList());
        }

        // GET: Admin/Hangsanxuats/Details/5
        public ActionResult Details(int? id)
        {
         //  hiển thị thông tin chi tiết về đối tượng "Hangsanxuat"
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hangsanxuat hangsanxuat = db.Hangsanxuats.Find(id);
            if (hangsanxuat == null)
            {
                return HttpNotFound();
            }
            return View(hangsanxuat);
        }

        // GET: Admin/Hangsanxuats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Hangsanxuats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Mahang,Tenhang")] Hangsanxuat hangsanxuat)
        {    //kiểm tra và tạo mới một đối tượng "Hangsanxuat", lưu nó vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                db.Hangsanxuats.Add(hangsanxuat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hangsanxuat);
        }

        // GET: Admin/Hangsanxuats/Edit/5
        public ActionResult Edit(int? id)
        {   //View hiển thị giao diện chỉnh sửa thông tin của đối tượng "Hangsanxuat" dựa trên ID được cung cấp
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hangsanxuat hangsanxuat = db.Hangsanxuats.Find(id);
            if (hangsanxuat == null)
            {
                return HttpNotFound();
            }
            return View(hangsanxuat);
        }

        // POST: Admin/Hangsanxuats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Mahang,Tenhang")] Hangsanxuat hangsanxuat)
        {      //kiểm tra và cập nhật 
            if (ModelState.IsValid)
            {
                db.Entry(hangsanxuat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hangsanxuat);
        }

        // GET: Admin/Hangsanxuats/Delete/5
        public ActionResult Delete(int? id)
        {   //hiển thị giao diện xác nhận việc xóa đối tượng
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hangsanxuat hangsanxuat = db.Hangsanxuats.Find(id);
            if (hangsanxuat == null)
            {
                return HttpNotFound();
            }
            return View(hangsanxuat);
        }

        // POST: Admin/Hangsanxuats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hangsanxuat hangsanxuat = db.Hangsanxuats.Find(id);
            db.Hangsanxuats.Remove(hangsanxuat);  // loại bỏ đối tượng 
            db.SaveChanges();   //lưu lại các thay đổi đã được thực hiện trong cơ sở dữ liệu.
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
