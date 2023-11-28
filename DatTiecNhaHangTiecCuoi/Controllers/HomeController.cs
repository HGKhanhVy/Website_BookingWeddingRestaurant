using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DatTiecNhaHangTiecCuoi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() // trang chủ
        {
            return View();
        }
        public ActionResult Gioithieu()
        {
            return View();
        }
        public ActionResult SanhTiec()
        {
            return View();
        }
        public ActionResult Menu()
        {
            return View();
        }
        public ActionResult Trangtri()
        {
            return View();
        }
        public ActionResult DatTiec()
        {

            return View();
        }
        public ActionResult DangNhap()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}