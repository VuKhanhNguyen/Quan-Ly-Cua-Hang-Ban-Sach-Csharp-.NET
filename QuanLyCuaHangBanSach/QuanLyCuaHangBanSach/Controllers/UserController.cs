using QuanLyCuaHangBanSach.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCuaHangBanSach.Controllers
{
    public class UserController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();

        // GET: ThongTinNguoiDung
        public ActionResult User()
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
                // Truyền thông tin người dùng vào view
                ViewBag.UserName = user.HoTen;
                ViewBag.Email = user.Email;
                ViewBag.Phone = user.SoDienThoai;
                ViewBag.Address = user.DiaChi;
            }

            return View(user);
        }

        // Action để đăng xuất
        public ActionResult DangXuat()
        {
            // Xóa tất cả session để đăng xuất người dùng
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}