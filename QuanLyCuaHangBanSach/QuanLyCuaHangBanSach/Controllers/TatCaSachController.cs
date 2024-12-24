using QuanLyCuaHangBanSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCuaHangBanSach.Controllers
{
    public class TatCaSachController : Controller
    {

        private QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: TatCaSach
        //public ActionResult TatCaSach()
        //{

        //    var viewModel = db.SACHes
        //   .ToList();

        //    return View(viewModel);
        //}

        public ActionResult TatCaSach(string search)
        {
            var viewModel = string.IsNullOrEmpty(search)
                ? db.SACHes.ToList()
                : db.SACHes.Where(s => s.TenSach.Contains(search) || s.TacGia.Contains(search)).ToList();

            ViewBag.Search = search;
            return View(viewModel);
        }
    }
}