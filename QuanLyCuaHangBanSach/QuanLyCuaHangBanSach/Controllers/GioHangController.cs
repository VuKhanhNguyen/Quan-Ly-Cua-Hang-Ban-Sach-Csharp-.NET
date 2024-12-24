using QuanLyCuaHangBanSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCuaHangBanSach.Controllers
{
    public class GioHangController : Controller
    {   
        private QuanLyBanSachEntities db = new QuanLyBanSachEntities();
       
        // Hiển thị giỏ hàng
        public ActionResult GioHang()
        {
            int userID = (Session["UserID"] != null) ? (int)Session["UserID"] : 0;

            if (userID == 0)
            {
                return RedirectToAction("DangNhapDangKy", "DangNhapDangKy");
            }

            var gioHang = db.GIOHANGs.Where(g => g.IDKhachHang == userID).ToList();
            return View(gioHang);
        }
        [HttpPost]
        public ActionResult ThemVaoGioHang(string IDSach, int SoLuong)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            int userID = (int)Session["UserID"];

            // Kiểm tra sách có tồn tại không
            var sach = db.SACHes.FirstOrDefault(s => s.IDSach == IDSach);
            if (sach == null)
            {
                return HttpNotFound("Không tìm thấy sách.");
            }

            // Kiểm tra xem sách đã có trong giỏ hàng chưa
            var gioHang = db.GIOHANGs.FirstOrDefault(g => g.IDSach == IDSach && g.IDKhachHang == userID);

            if (gioHang != null)
            {
                gioHang.SoLuongDat += SoLuong;
            }
            else
            {
                // Thêm sách mới vào giỏ hàng
                gioHang = new GIOHANG
                {
                    IDKhachHang = userID,
                    IDSach = IDSach,
                    SoLuongDat = SoLuong
                };
                db.GIOHANGs.Add(gioHang);
            }

            db.SaveChanges();
            return RedirectToAction("GioHang");
        }

        // Xóa sách khỏi giỏ hàng
        public ActionResult XoaKhoiGioHang(string IDSach)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            int userID = (int)Session["UserID"];
            var gioHang = db.GIOHANGs.FirstOrDefault(g => g.IDSach == IDSach && g.IDKhachHang == userID);

            if (gioHang != null)
            {
                db.GIOHANGs.Remove(gioHang);
                db.SaveChanges();
            }

            return RedirectToAction("GioHang");
        }
    }
}