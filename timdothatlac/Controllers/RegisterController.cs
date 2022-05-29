using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModalEF.EF;
using timdothatlac.Common;

namespace timdothatlac.Controllers
{
    public class RegisterController : Controller
    {
        private ContextDB db = new ContextDB();

        public ActionResult Create()
        {
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTaiKhoan,MaQuyen,MaSinhVien,Ten,GioiTinh,Email,MatKhau,SDT,NgaySinh,AnhDaiDien,AnhTheSV,TrangThai")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                taiKhoan.MatKhau = Encryptor.MD5Hash(taiKhoan.MatKhau);
                taiKhoan.MaQuyen = 2;
                taiKhoan.TrangThai = true;
                taiKhoan.NgayTao = DateTime.Now;
                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                return RedirectToAction("Index", "Login");
            }
            return View(taiKhoan);
        }
        
    }
}
