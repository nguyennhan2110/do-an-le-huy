using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModalEF.DAO;
using ModalEF.EF;

namespace timdothatlac.Controllers
{
    public class BaiDangsController : Controller
    {
        private ContextDB db = new ContextDB();
        public AnhDinhKem adk = new AnhDinhKem();

        //public ActionResult Index()
        //{
        //    var baiDangs = db.BaiDangs.Include(b => b.AnhDinhKem).Include(b => b.DanhMuc).Include(b => b.TaiKhoan).Include(b => b.TrangThaiBaiDang);
        //    return View(baiDangs.ToList());
        //}

        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new BaiDangDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;

            return View(model);
        }

        //Thêm
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaAnhDinhKem = new SelectList(db.AnhDinhKems, "MaAnhDinhKem", "AnhBia");
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "MaSinhVien");
            ViewBag.MaTrangThaiBaiDang = new SelectList(db.TrangThaiBaiDangs, "MaTrangThaiBaiDang", "TenTrangThai");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaBaiDang,MaDanhMuc,MaTaiKhoan,MaAnhDinhKem,MaTrangThaiBaiDang,TieuDe,NoiDung,NgayTao,NgayDuyet,TrangThaiDuyet")] BaiDang baiDang, HttpPostedFileBase file)
        {
            var session = (timdothatlac.Common.LoginUserSession)Session[timdothatlac.Common.Constant.USER_SESSION];

            if (ModelState.IsValid)
            {
                //file 
                string path = Server.MapPath("~/FileUpload");
                string fileName = Path.GetFileName(file.FileName);
                string pathFull = Path.Combine(path, fileName);
                file.SaveAs(pathFull);
                adk.AnhBia = file.FileName;
                var a = db.AnhDinhKems.Add(adk);

                baiDang.MaAnhDinhKem = a.MaAnhDinhKem;
                baiDang.NgayTao = DateTime.Now;
                baiDang.MaTaiKhoan = session.MaUser;
                baiDang.LuotXem = 0;

                db.BaiDangs.Add(baiDang);
                db.SaveChanges();
                return RedirectToAction("Index", "BaiDangs", routeValues: new { Area = "" });
            }

            ViewBag.MaAnhDinhKem = new SelectList(db.AnhDinhKems, "MaAnhDinhKem", "AnhBia", baiDang.MaAnhDinhKem);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", baiDang.MaDanhMuc);
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "MaSinhVien", baiDang.MaTaiKhoan);
            ViewBag.MaTrangThaiBaiDang = new SelectList(db.TrangThaiBaiDangs, "MaTrangThaiBaiDang", "TenTrangThai", baiDang.MaTrangThaiBaiDang);
            return View(baiDang);
        }
    }
}