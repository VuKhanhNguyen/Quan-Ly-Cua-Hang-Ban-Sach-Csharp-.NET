using QuanLyCuaHangBanSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyCuaHangBanSach.Controllers
{
    public class DangNhapDangKyController : Controller
    {

        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: DangNhapDangKy
        public ActionResult DangNhapDangKy()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DangNhapDangKy(string username, string password)
        {
            if (ModelState.IsValid)
            {

                //var hashedPassword = ComputeHash(password);
                var user = db.KHACHHANGs.SingleOrDefault(u => u.TaiKhoan == username && u.MatKhau == password);

                // Kiểm tra tài khoản admin
                if (username == "admin" && password == "admin")
                {
                    Session["UserID"] = user.IDKhachHang;
                    Session["UserName"] = "admin";
                    return RedirectToAction("Index", "QuanLy");
                }


                // Tìm người dùng theo tài khoản và mật khẩu
              

                if (user != null)
                {
                    // Lưu IDKhachHang vào Session khi đăng nhập thành công
                    Session["UserID"] = user.IDKhachHang; // Lưu IDKhachHang vào Session

                    // Lưu tên người dùng vào Session (tuỳ chọn)
                    Session["UserName"] = user.HoTen;

                    // Redirect đến trang chủ hoặc trang nào đó
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                    
                }
            }
        
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public ActionResult DangKy(string HoTen, string SoDienThoai, string DiaChi, string Email, string TaiKhoan, string MatKhau, string XacNhanMatKhau)
        {
            if (ModelState.IsValid)
            {
                if (MatKhau != XacNhanMatKhau)
                {
                    ModelState.AddModelError("", "Mật khẩu và xác nhận mật khẩu không khớp");
                    return View("DangNhapDangKy");
                }

                // Kiểm tra tài khoản đã tồn tại
                if (db.KHACHHANGs.Any(u => u.TaiKhoan == TaiKhoan))
                {
                    ModelState.AddModelError("", "Tài khoản đã tồn tại");
                    return View("DangNhapDangKy");
                }

                // Tạo khách hàng mới với mật khẩu chưa mã hóa
                var khachHangMoi = new KHACHHANG
                {
                    HoTen = HoTen,
                    SoDienThoai = SoDienThoai,
                    DiaChi = DiaChi,
                    Email = Email,
                    TaiKhoan = TaiKhoan,
                    MatKhau = MatKhau // Không mã hóa mật khẩu
                };

                db.KHACHHANGs.Add(khachHangMoi);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Đăng ký thành công! Bạn có thể đăng nhập.";
                return RedirectToAction("DangNhapDangKy");
            }

            return View("DangNhapDangKy");
        }


        //public string ComputeHash(string password)
        //{
        //    using (SHA256 sha256Hash = SHA256.Create())
        //    {

        //        if (string.IsNullOrEmpty(password))
        //        {
        //            throw new ArgumentNullException("password", "Password cannot be null or empty");
        //        }
        //        // Chuyển mật khẩu thành mảng byte
        //        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

        //        // Chuyển đổi mảng byte thành chuỗi hex
        //        StringBuilder builder = new StringBuilder();
        //        foreach (byte t in bytes)
        //        {
        //            builder.Append(t.ToString("x2"));
        //        }
        //        return builder.ToString(); // Trả về mật khẩu đã mã hóa
        //    }
        //}
    }
}