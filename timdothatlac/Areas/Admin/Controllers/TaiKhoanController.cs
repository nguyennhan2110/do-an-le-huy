﻿using ModalEF.DAO;
using ModalEF.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using timdothatlac.Common;

namespace timdothatlac.Areas.Admin.Controllers
{
    public class TaiKhoanController : BaseController
    {
        private ContextDB db = new ContextDB();

        //Phân trang list, default = 10
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
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen");
            return View();
        }

        [HttpPost]
        public ActionResult Create(TaiKhoan taiKhoan, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var dao = new TaiKhoanDao();

                //file 
                string path = Server.MapPath("~/FileUpload");
                string fileName = Path.GetFileName(file.FileName);

                string pathFull = Path.Combine(path, fileName);
                file.SaveAs(pathFull);

                var encryptedMd5Pass = Encryptor.MD5Hash(taiKhoan.MatKhau);
                taiKhoan.MatKhau = encryptedMd5Pass;
                taiKhoan.NgayTao = DateTime.Now;
                taiKhoan.AnhDaiDien = file.FileName;

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
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", taiKhoan.MaQuyen);
            return View("Index");
        }

        //Sửa
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", taiKhoan.MaQuyen);
            return View(taiKhoan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", taiKhoan.MaQuyen);
            return View(taiKhoan);
        }

        //Xoá
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new TaiKhoanDao().Delete(id);
            return RedirectToAction("Index");
        }

        //Xem chi tiết
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
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

        //Đăng xuất
        public ActionResult Logout()
        {
            Session[Constant.USER_SESSION] = null;
            return RedirectToAction("Index", "Login", routeValues: new { Area = "" });
        }
    }
}