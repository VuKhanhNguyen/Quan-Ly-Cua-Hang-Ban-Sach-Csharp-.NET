using QuanLyCuaHangBanSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCuaHangBanSach.Controllers
{
    public class ChiTietSachController : Controller
    {
        private QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: ChiTietSach
        public ActionResult ChiTietSach(String id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound("Sách không tồn tại.");
            }



            var sach = db.SACHes.FirstOrDefault(s => s.IDSach == id);
            if (sach == null)
            {
                return HttpNotFound("Sách không tồn tại.");
            }

            return View(sach);
        }
    }
}