using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHangBanSach.Models;

namespace QuanLyCuaHangBanSach.Controllers
{
    public class HomeController : Controller
    {
        private QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult IndexAfterDangNhap()
        {
            
            return View();

        }

        public ActionResult Index()
        {
            var viewModel = db.THELOAIs.Include("SACHes").ToList();
            return View(viewModel);
            
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