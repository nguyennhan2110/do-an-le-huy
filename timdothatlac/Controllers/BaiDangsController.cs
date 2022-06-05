using ModalEF.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace timdothatlac.Controllers
{
    public class BaiDangsController : Controller
    {
        private ContextDB db = new ContextDB();

        //public ActionResult Index()
        //{
        //    var baiDangs = db.BaiDangs.Include(b => b.AnhDinhKem).Include(b => b.DanhMuc).Include(b => b.TaiKhoan).Include(b => b.TrangThaiBaiDang);
        //    return View(baiDangs.ToList());
        //}
    }
}