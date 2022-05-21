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
    public class TaiKhoanController : BaseController
    {
        //Phân trang list, default = 5
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new TaiKhoanDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;

            return View(model);
        }

        //Thêm
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                var dao = new TaiKhoanDao();

                var encryptedMd5Pass = Encryptor.MD5Hash(taiKhoan.MatKhau);
                taiKhoan.MatKhau = encryptedMd5Pass;
                taiKhoan.MaQuyen = 2;
                taiKhoan.NgayTao = DateTime.Now;

                long id = dao.Insert(taiKhoan);
                if (id > 0)
                {
                    SetAlert("Thêm tài khoản thành công!", "success");
                    return RedirectToAction("Index", "TaiKhoan");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm tài khoản thất bại!");
                }
            }
            return View("Index");
        }

        //Sửa
        public ActionResult Edit(int id)
        {
            var taiKhoan = new TaiKhoanDao().ViewDetail(id);
            return View(taiKhoan);
        }

        [HttpPost]
        public ActionResult Edit(TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                var dao = new TaiKhoanDao();
                if (!string.IsNullOrEmpty(taiKhoan.MatKhau))
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(taiKhoan.MatKhau);
                    taiKhoan.MatKhau = encryptedMd5Pas;
                }

                var result = dao.Update(taiKhoan);
                if (result)
                {
                    SetAlert("Cập nhật thành công!", "success");
                    return RedirectToAction("Index", "TaiKhoan");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại!");
                }
            }
            return View("Index");
        }

        //Xoá
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new TaiKhoanDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new TaiKhoanDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}