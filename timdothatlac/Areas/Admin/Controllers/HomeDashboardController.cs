using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace timdothatlac.Areas.Admin.Controllers
{
    public class HomeDashboardController : Controller
    {
        // GET: Admin/HomeDashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}