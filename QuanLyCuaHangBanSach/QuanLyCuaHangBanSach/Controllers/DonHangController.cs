using QuanLyCuaHangBanSach.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCuaHangBanSach.Controllers
{
    public class DonHangController : Controller
    {
        private QuanLyBanSachEntities db = new QuanLyBanSachEntities();


        // Hiển thị đơn hàng
        public ActionResult DonHang()
        {
            db.Configuration.LazyLoadingEnabled = false;

            // Kiểm tra người dùng đăng nhập
            if (Session["UserID"] == null)
            {
                return RedirectToAction("DangNhapDangKy", "DangNhapDangKy");
            }

            int userID = (int)Session["UserID"];

            // Lấy thông tin người dùng
            var khachHang = db.KHACHHANGs.FirstOrDefault(k => k.IDKhachHang == userID);
            if (khachHang == null)
            {
                return HttpNotFound("Không tìm thấy thông tin người dùng.");
            }

            // Lấy giỏ hàng của người dùng
            var gioHang = db.GIOHANGs
                .Where(g => g.IDKhachHang == userID)
                .Include("SACH")  // Bao gồm thông tin sách
                .ToList();

            // Tính tổng tiền
            decimal tongTien = gioHang.Sum(g => Convert.ToDecimal(g.SACH.GiaBan * g.SoLuongDat));

            // Truyền dữ liệu vào View
            ViewBag.KhachHang = khachHang;
            ViewBag.TongTien = tongTien;
            ViewBag.NgayHienTai = DateTime.Now.ToString("dd/MM/yyyy"); // Lấy ngày hiện tại

            // Truyền giỏ hàng vào ViewBag (mã hóa thành JSON)
            ViewBag.GioHangList = gioHang.Select(g => new {
                g.IDGioHang,
                g.SACH.TenSach,
                g.SACH.GiaBan,
                g.SoLuongDat,
                g.SACH.AnhSach
            }).ToList();  // Chọn các thuộc tính cần thiết

            return View(gioHang);
        }


        // Xử lý khi đặt hàng
        //[HttpPost]
        //public ActionResult DatHang(List<GIOHANG> gioHang)
        //{
        //    if (gioHang == null || !gioHang.Any())
        //    {
        //        return Json(new { success = false, message = "Giỏ hàng trống!" });
        //    }

        //    try
        //    {
        //        // Kiểm tra người dùng đăng nhập
        //        if (Session["UserID"] == null)
        //        {
        //            return RedirectToAction("DangNhapDangKy", "DangNhapDangKy");
        //        }

        //        int userID = (int)Session["UserID"];
        //        var khachHang = db.KHACHHANGs.FirstOrDefault(k => k.IDKhachHang == userID);
        //        if (khachHang == null)
        //        {
        //            return Json(new { success = false, message = "Không tìm thấy thông tin người dùng." });
        //        }

        //        // Tạo đơn hàng
        //        var donHang = new DONHANG
        //        {
        //            IDKhachHang = userID,
        //            NgayDatHang = DateTime.Now,
        //            TongTien = gioHang.Sum(g => g.SACH.GiaBan * g.SoLuongDat),
        //            TrangThai = "Đang chờ" // Hoặc trạng thái khác nếu cần
        //        };

        //        db.DONHANGs.Add(donHang);
        //        db.SaveChanges();  // Lưu đơn hàng vào cơ sở dữ liệu

        //        // Lưu chi tiết đơn hàng
        //        foreach (var item in gioHang)
        //        {
        //            var chiTietDonHang = new CHITIETDONHANG
        //            {
        //                IDDonHang = donHang.IDDonHang,
        //                IDSach = item.IDSach,
        //                SoLuong = item.SoLuongDat,
        //                DonGia = item.SACH.GiaBan*item.SoLuongDat
        //            };

        //            db.CHITIETDONHANGs.Add(chiTietDonHang);
        //        }
        //        db.SaveChanges();  // Lưu chi tiết đơn hàng vào cơ sở dữ liệu

        //        // Xóa giỏ hàng sau khi đặt hàng
        //        var gioHangList = db.GIOHANGs.Where(g => g.IDKhachHang == userID).ToList();
        //        db.GIOHANGs.RemoveRange(gioHangList);
        //        db.SaveChanges();  // Lưu thay đổi

        //        return Json(new { success = true, message = "Đặt hàng thành công!" });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log lỗi và trả về thông báo lỗi
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}
        [HttpPost]
        public ActionResult DatHang()
        {
            var userId = (int)Session["UserID"];
            var db = new QuanLyBanSachEntities();

            // Kiểm tra giỏ hàng của người dùng
            var gioHang = db.GIOHANGs.Where(g => g.IDKhachHang == userId).ToList();
            if (gioHang.Count == 0)
            {
                return Json(new { success = false, message = "Giỏ hàng trống, vui lòng thêm sản phẩm." });
            }

            // Tạo mới đơn hàng
            var donHang = new DONHANG
            {
                IDKhachHang = userId,
                NgayDatHang = DateTime.Now,
                TongTien = gioHang.Sum(g => g.SACH.GiaBan * g.SoLuongDat),
                TrangThai= "Đang xử lý"
                // Các thuộc tính khác của đơn hàng có thể cần thêm như trạng thái, phương thức thanh toán, etc.
            };

            db.DONHANGs.Add(donHang);
            db.SaveChanges();

            // Lưu chi tiết đơn hàng vào bảng DONHANG_DETAIL
            foreach (var item in gioHang)
            {
                var chiTietDonHang = new CHITIETDONHANG
                {
                    IDDonHang = donHang.IDDonHang,
                    IDSach = item.IDSach,
                    SoLuong = item.SoLuongDat,
                    //DonGia = item.SACH.GiaBan,
                    DonGia = item.SACH.GiaBan * item.SoLuongDat
                };
                db.CHITIETDONHANGs.Add(chiTietDonHang);
            }

            db.SaveChanges();

            // Xóa giỏ hàng sau khi đặt hàng thành công
            db.GIOHANGs.RemoveRange(gioHang);
            db.SaveChanges();

            // Trả về kết quả thành công cho người dùng
            return Json(new { success = true });
        }

    }
}
