using QuanLyCuaHangBanSach.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCuaHangBanSach.Controllers
{
    public class QuanLyController : Controller
    {
        private QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: QuanLy
        public ActionResult Index()
        {
            var khachHangs = db.KHACHHANGs.ToList();
            var chitietdonHangs = db.vw_DonHangChiTiet.ToList();
            var sachs = db.SACHes.ToList();
            ViewBag.Sachs = sachs;
            ViewBag.KhachHangs = khachHangs;
            ViewBag.ChiTietDonHangs = chitietdonHangs; // Truyền danh sách chi tiết đơn hàng vào Vie

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveKH(KHACHHANG model)
        {
            if (ModelState.IsValid)
            {
                // Check if the account (TaiKhoan) already exists in the database (excluding the current customer's ID)
                var existingAccount = db.KHACHHANGs.FirstOrDefault(k => k.TaiKhoan == model.TaiKhoan && k.IDKhachHang != model.IDKhachHang);

                if (existingAccount != null)
                {
                    // If account already exists, add an error to the model state
                    ModelState.AddModelError("TaiKhoan", "Tài khoản đã tồn tại. Vui lòng chọn tên khác.");
                    return View("_AddKH", model); // Return to the form with the error message
                }

                if (model.IDKhachHang == 0) // Add new customer
                {
                    db.KHACHHANGs.Add(model); // Add the new customer to the database
                }
                else // Edit existing customer
                {
                    var existingCustomer = db.KHACHHANGs.FirstOrDefault(k => k.IDKhachHang == model.IDKhachHang);
                    if (existingCustomer != null)
                    {
                        // Update the customer details
                        existingCustomer.HoTen = model.HoTen;
                        existingCustomer.Email = model.Email;
                        existingCustomer.SoDienThoai = model.SoDienThoai;
                        existingCustomer.DiaChi = model.DiaChi;
                        existingCustomer.TaiKhoan = model.TaiKhoan;
                        existingCustomer.MatKhau = model.MatKhau;
                    }
                }

                db.SaveChanges(); // Save changes to the database
                return RedirectToAction("Index"); // Redirect to the customer list page
            }

            return View("_AddKH", model); // If there are validation errors, return to the form
        }

        // Hiển thị form thêm khách hàng mới
        public ActionResult AddKH()
        {
            return PartialView("_AddKH", new KHACHHANG());
        }

        public ActionResult EditKH(int id)
        {
            var customer = db.KHACHHANGs.FirstOrDefault(k => k.IDKhachHang == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditKH", customer);
        }

        // Xóa khách hàng
        [HttpPost]
        public ActionResult DeleteKH(int id)
        {
            var customer = db.KHACHHANGs.FirstOrDefault(k => k.IDKhachHang == id);
            if (customer != null)
            {
                db.KHACHHANGs.Remove(customer);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult AddSach()
        {

            // Lấy danh sách thể loại từ cơ sở dữ liệu
            ViewBag.TheLoaiList = db.THELOAIs.Select(tl => new
            {
                IDTheLoai = tl.IDTheLoai,
                TenTheLoai = tl.TenTheLoai
            }).ToList();

            // Trả về PartialView với đối tượng SACH rỗng
            return PartialView("_AddSach", new SACH());
        }



        public ActionResult EditSach(string id)
        {
            // Tìm sách theo ID
            var book = db.SACHes.FirstOrDefault(s => s.IDSach == id);
            if (book == null)
            {
                return HttpNotFound();
            }

            // Kiểm tra danh sách thể loại và truyền vào ViewBag
            var theLoaiList = db.THELOAIs.ToList(); // Lấy tất cả thể loại
            if (theLoaiList == null || !theLoaiList.Any())
            {
                // Nếu danh sách thể loại trống, có thể thông báo lỗi hoặc tạo danh sách mặc định
                ViewBag.TheLoaiList = new List<THELOAI>(); // Tạo danh sách trống nếu không có thể loại
            }
            else
            {
                ViewBag.TheLoaiList = theLoaiList;
            }

            return PartialView("_EditSach", book);
        }




        // Xóa sách
        [HttpPost]
        public ActionResult DeleteSach(string id)
        {
            var book = db.SACHes.FirstOrDefault(s => s.IDSach == id);
            if (book != null)
            {
                db.SACHes.Remove(book);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        // Lưu sách mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveNewSach(SACH model, HttpPostedFileBase AnhSach)
        {
            // Danh sách thể loại để hiển thị lại khi có lỗi
            ViewBag.TheLoaiList = db.THELOAIs.Select(tl => new
            {
                IDTheLoai = tl.IDTheLoai,
                TenTheLoai = tl.TenTheLoai
            }).ToList();

            if (ModelState.IsValid)
            {
                // Kiểm tra trùng lặp ID sách
                if (db.SACHes.Any(s => s.IDSach == model.IDSach))
                {
                    ModelState.AddModelError("IDSach", "Mã sách này đã tồn tại trong cơ sở dữ liệu.");
                    return View("_AddSach", model); // Trả về partial view form thêm sách
                }

                // Kiểm tra và xử lý file ảnh
                if (AnhSach != null && AnhSach.ContentLength > 0)
                {
                    string extension = Path.GetExtension(AnhSach.FileName).ToLower();
                    if (extension != ".jpg" && extension != ".png" && extension != ".jpeg")
                    {
                        ModelState.AddModelError("AnhSach", "Chỉ chấp nhận file ảnh định dạng JPG, PNG, JPEG.");
                        return View("_AddSach", model); // Trả về partial view form thêm sách
                    }

                    // Lưu file ảnh vào thư mục
                    string fileName = Guid.NewGuid() + extension;
                    string path = Path.Combine(Server.MapPath("~/images/"), fileName);
                    AnhSach.SaveAs(path);
                    model.AnhSach = "~/images/" + fileName; // Đường dẫn lưu trong cơ sở dữ liệu
                }

                // Thêm sách vào cơ sở dữ liệu
                db.SACHes.Add(model);
                db.SaveChanges();

                // Chuyển hướng về Index với mục "Quản lý Sách"
                return RedirectToAction("Index", new { section = "qlSach" });
            }

            // Nếu có lỗi trong model, trả về form với dữ liệu lỗi
            return View("_AddSach", model);
        }

        // Lưu chỉnh sửa sách
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEditedSach(SACH model, HttpPostedFileBase AnhSach)
        {
            var existingBook = db.SACHes.FirstOrDefault(s => s.IDSach == model.IDSach);
            if (existingBook == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                existingBook.TenSach = model.TenSach;
                existingBook.TacGia = model.TacGia;
                existingBook.IDTheLoai = model.IDTheLoai;
                existingBook.NhaXuatBan = model.NhaXuatBan;
                existingBook.NamXuatBan = model.NamXuatBan;
                existingBook.GiaNhap = model.GiaNhap;
                existingBook.GiaBan = model.GiaBan;
                existingBook.SoLuongTon = model.SoLuongTon;
                existingBook.MoTa = model.MoTa;

                if (AnhSach != null && AnhSach.ContentLength > 0)
                {
                    string extension = Path.GetExtension(AnhSach.FileName).ToLower();
                    if (extension != ".jpg" && extension != ".png" && extension != ".jpeg")
                    {
                        ModelState.AddModelError("AnhSach", "Chỉ chấp nhận file ảnh định dạng JPG, PNG, JPEG.");
                        return View("_EditSach", model);
                    }

                    string fileName = Guid.NewGuid() + extension;
                    string path = Path.Combine(Server.MapPath("~/images/"), fileName);
                    AnhSach.SaveAs(path);

                    existingBook.AnhSach = "~/images/" + fileName;
                }

                db.SaveChanges();
                return RedirectToAction("Index", new { section = "qlSach" });
            }

            ViewBag.TheLoaiList = db.THELOAIs.Select(tl => new
            {
                IDTheLoai = tl.IDTheLoai,
                TenTheLoai = tl.TenTheLoai
            }).ToList();

            return View("_EditSach", model);
        }


        public JsonResult CheckUsernameExists(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return Json(new { exists = false }); // If username is empty, return false
            }

            bool exists = db.KHACHHANGs.Any(kh => kh.TaiKhoan == username);  // Check if the username exists in the dbbase
            return Json(new { exists = exists });
        }
        public ActionResult AddDH()
        {
            // Lấy danh sách khách hàng
            var khachHangs = db.KHACHHANGs.ToList();
            ViewBag.KhachHangs = new SelectList(khachHangs, "IDKhachHang", "HoTen");

            // Lấy danh sách sách kèm giá bán
            var sachs = db.SACHes.Select(s => new
            {
                IDSach = s.IDSach,
                TenSach = s.TenSach,
                GiaBan = s.GiaBan
            }).ToList();
            ViewBag.Sachs = sachs;

            // Trả về PartialView với đối tượng `vw_DonHangChiTiet` rỗng
            return PartialView("_AddDH", new vw_DonHangChiTiet());
        }

        public ActionResult EditDH(int id)
        {
            // Lấy đơn hàng chi tiết theo ID
            var donHangChiTiet = db.vw_DonHangChiTiet.FirstOrDefault(dh => dh.IDDonHang == id);
            if (donHangChiTiet == null)
            {
                return HttpNotFound("Không tìm thấy đơn hàng.");
            }

            // Lấy danh sách khách hàng
            var khachHangs = db.KHACHHANGs.ToList();
            ViewBag.KhachHangs = new SelectList(khachHangs, "IDKhachHang", "HoTen", donHangChiTiet.IDKhachHang);

            // Lấy danh sách sách kèm giá bán
            var sachs = db.SACHes.Select(s => new
            {
                IDSach = s.IDSach,
                TenSach = s.TenSach,
                GiaBan = s.GiaBan
            }).ToList();
            ViewBag.Sachs = new SelectList(sachs, "IDSach", "TenSach", donHangChiTiet.IDSach);

            // Trả về PartialView với model là `vw_DonHangChiTiet`
            return PartialView("_EditDH", donHangChiTiet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDH(vw_DonHangChiTiet model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var sach = db.SACHes.FirstOrDefault(s => s.IDSach == model.IDSach);
                    if (sach == null)
                    {
                        ModelState.AddModelError("", "Sách không tồn tại.");
                        // Trả về view chỉnh sửa với thông tin lỗi
                        return View("_EditDH", model);
                    }

                    if (model.IDDonHang == 0)
                    {
                        // Thêm mới đơn hàng
                        var donHang = new DONHANG
                        {
                            IDKhachHang = model.IDKhachHang,
                            NgayDatHang = DateTime.Now,
                            TrangThai = model.TrangThai,
                            TongTien = sach.GiaBan * model.SoLuong
                        };

                        db.DONHANGs.Add(donHang);
                        db.SaveChanges();

                        var donHangChiTiet = new CHITIETDONHANG
                        {
                            IDDonHang = donHang.IDDonHang,
                            IDSach = model.IDSach,
                            SoLuong = model.SoLuong,
                            DonGia = sach.GiaBan * model.SoLuong
                        };

                        db.CHITIETDONHANGs.Add(donHangChiTiet);
                    }
                    else
                    {
                        // Sửa đơn hàng
                        var donHang = db.DONHANGs.FirstOrDefault(dh => dh.IDDonHang == model.IDDonHang);
                        var donHangChiTiet = db.CHITIETDONHANGs.FirstOrDefault(ct => ct.IDDonHang == model.IDDonHang);

                        if (donHang == null || donHangChiTiet == null)
                        {
                            ModelState.AddModelError("", "Đơn hàng hoặc chi tiết đơn hàng không tồn tại.");
                            return View("_EditDH", model);
                        }

                        donHang.IDKhachHang = model.IDKhachHang;
                        donHang.TrangThai = model.TrangThai;
                        donHang.TongTien = sach.GiaBan * model.SoLuong;

                        donHangChiTiet.IDSach = model.IDSach;
                        donHangChiTiet.SoLuong = model.SoLuong;
                        donHangChiTiet.DonGia = sach.GiaBan * model.SoLuong;
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index"); // Chuyển hướng về danh sách
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi: " + ex.Message);
                }
            }

            // Nếu model không hợp lệ, trả về view chỉnh sửa
            return View("_EditDH", model);
        }



        public ActionResult DeleteDH(string id)
        {
            // Chuyển đổi id từ string sang int (nếu IDDonHang là int)
            if (int.TryParse(id, out int idDonHang))
            {
                var customer = db.CHITIETDONHANGs.FirstOrDefault(k => k.IDDonHang == idDonHang);
                if (customer != null)
                {
                    db.CHITIETDONHANGs.Remove(customer);
                    db.SaveChanges();
                }

                var DH1 = db.DONHANGs.FirstOrDefault(p => p.IDDonHang == idDonHang);
                if (DH1 != null)
                {
                    db.DONHANGs.Remove(DH1);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            else
            {
                // Xử lý trường hợp id không phải là số hợp lệ
                return View("Error"); // Hoặc bạn có thể hiển thị thông báo lỗi
            }

        }
    }
}