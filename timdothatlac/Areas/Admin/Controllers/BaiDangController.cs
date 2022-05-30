using ModalEF.DAO;
using ModalEF.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using timdothatlac.Common;

namespace timdothatlac.Areas.Admin.Controllers
{
    public class BaiDangController : BaseController
    {
        private ContextDB db = new ContextDB();

        //Phân trang list, default = 10
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new BaiDangDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;

            return View(model);
        }

        //Thêm
        public ActionResult Create()
        {
            ViewBag.MaAnhDinhKem = new SelectList(db.AnhDinhKems, "MaAnhDinhKem", "AnhBia");
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "MaSinhVien");
            ViewBag.MaTrangThaiBaiDang = new SelectList(db.TrangThaiBaiDangs, "MaTrangThaiBaiDang", "TenTrangThai");
            return View();
        }

        [HttpPost]
        public ActionResult Create(BaiDang baiDang)
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

        //Xoá
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new BaiDangDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new BaiDangDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        //Đăng xuất
        public ActionResult Logout()
        {
            Session[Constant.USER_SESSION] = null;
            return RedirectToAction("Index", "Login", routeValues: new { Area = "" });
        }
    }
}