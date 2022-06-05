using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModalEF.EF;

namespace timdothatlac.Controllers
{
    public class BaiDangs1Controller : Controller
    {
        private ContextDB db = new ContextDB();

        // GET: BaiDangs1
        public ActionResult Index()
        {
            var baiDangs = db.BaiDangs.Include(b => b.AnhDinhKem).Include(b => b.DanhMuc).Include(b => b.TaiKhoan).Include(b => b.TrangThaiBaiDang);
            return View(baiDangs.ToList());
        }

        // GET: BaiDangs1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDang baiDang = db.BaiDangs.Find(id);
            if (baiDang == null)
            {
                return HttpNotFound();
            }
            return View(baiDang);
        }

        // GET: BaiDangs1/Create
        public ActionResult Create()
        {
            ViewBag.MaAnhDinhKem = new SelectList(db.AnhDinhKems, "MaAnhDinhKem", "AnhBia");
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "MaSinhVien");
            ViewBag.MaTrangThaiBaiDang = new SelectList(db.TrangThaiBaiDangs, "MaTrangThaiBaiDang", "TenTrangThai");
            return View();
        }

        // POST: BaiDangs1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaBaiDang,MaDanhMuc,MaTaiKhoan,MaAnhDinhKem,MaTrangThaiBaiDang,TieuDe,NoiDung,NgayTao,NgayDuyet,TrangThaiDuyet,LuotXem")] BaiDang baiDang)
        {
            if (ModelState.IsValid)
            {
                db.BaiDangs.Add(baiDang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaAnhDinhKem = new SelectList(db.AnhDinhKems, "MaAnhDinhKem", "AnhBia", baiDang.MaAnhDinhKem);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", baiDang.MaDanhMuc);
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "MaSinhVien", baiDang.MaTaiKhoan);
            ViewBag.MaTrangThaiBaiDang = new SelectList(db.TrangThaiBaiDangs, "MaTrangThaiBaiDang", "TenTrangThai", baiDang.MaTrangThaiBaiDang);
            return View(baiDang);
        }

        // GET: BaiDangs1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDang baiDang = db.BaiDangs.Find(id);
            if (baiDang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaAnhDinhKem = new SelectList(db.AnhDinhKems, "MaAnhDinhKem", "AnhBia", baiDang.MaAnhDinhKem);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", baiDang.MaDanhMuc);
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "MaSinhVien", baiDang.MaTaiKhoan);
            ViewBag.MaTrangThaiBaiDang = new SelectList(db.TrangThaiBaiDangs, "MaTrangThaiBaiDang", "TenTrangThai", baiDang.MaTrangThaiBaiDang);
            return View(baiDang);
        }

        // POST: BaiDangs1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaBaiDang,MaDanhMuc,MaTaiKhoan,MaAnhDinhKem,MaTrangThaiBaiDang,TieuDe,NoiDung,NgayTao,NgayDuyet,TrangThaiDuyet,LuotXem")] BaiDang baiDang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baiDang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaAnhDinhKem = new SelectList(db.AnhDinhKems, "MaAnhDinhKem", "AnhBia", baiDang.MaAnhDinhKem);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", baiDang.MaDanhMuc);
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "MaSinhVien", baiDang.MaTaiKhoan);
            ViewBag.MaTrangThaiBaiDang = new SelectList(db.TrangThaiBaiDangs, "MaTrangThaiBaiDang", "TenTrangThai", baiDang.MaTrangThaiBaiDang);
            return View(baiDang);
        }

        // GET: BaiDangs1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDang baiDang = db.BaiDangs.Find(id);
            if (baiDang == null)
            {
                return HttpNotFound();
            }
            return View(baiDang);
        }

        // POST: BaiDangs1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaiDang baiDang = db.BaiDangs.Find(id);
            db.BaiDangs.Remove(baiDang);
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
