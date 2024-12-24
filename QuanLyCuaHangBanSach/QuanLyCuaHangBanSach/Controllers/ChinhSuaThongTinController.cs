using QuanLyCuaHangBanSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCuaHangBanSach.Controllers
{
    public class ChinhSuaThongTinController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: ChinhSuaThongTin
        public ActionResult ChinhSuaThongTin()
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa
            var userId = Session["UserID"];
            if (userId == null)
            {
                // Nếu chưa đăng nhập, chuyển hướng đến trang đăng nhập
                return RedirectToAction("DangNhapDangKy", "DangNhapDangKy");
            }

            // Nếu đã đăng nhập, lấy thông tin người dùng
            var user = db.KHACHHANGs.FirstOrDefault(u => u.IDKhachHang == (int)userId);
            if (user != null)
            {
                return View(user);
            }

            return RedirectToAction("ThongTinNguoiDung", "ThongTinNguoiDung");
        }

        // Action xử lý lưu thông tin chỉnh sửa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LuuThongTin(KHACHHANG user)
        {
            var userId = Session["UserID"];
            var existingUser = db.KHACHHANGs.FirstOrDefault(u => u.IDKhachHang == (int)userId);

            if (existingUser != null)
            {
                // Cập nhật thông tin người dùng
                existingUser.HoTen = user.HoTen;
                existingUser.Email = user.Email;
                existingUser.SoDienThoai = user.SoDienThoai;
                existingUser.DiaChi = user.DiaChi;

                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }
    }
}