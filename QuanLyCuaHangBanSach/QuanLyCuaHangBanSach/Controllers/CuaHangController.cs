using QuanLyCuaHangBanSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCuaHangBanSach.Controllers
{
    public class CuaHangController : Controller
    {
        private QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: CuaHang
        public ActionResult CuaHang()
        {
            //var danhSachTheLoai = db.THELOAIs.ToList();
            var viewModel = db.THELOAIs.Include("SACHes").ToList();
            //var categories = db.THELOAIs.ToList();  // Get all categories
            //var books = db.SACHes.ToList();  // Get all books
            //var viewModel = categories.Select(category => new
            //{
            //    Category = category,
            //    Books = books.Where(book => book.IDTheLoai == category.IDTheLoai).ToList()
            //}).ToList();
            // Truyền dữ liệu xuống view
            return View(viewModel);
        }
    }
}