//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyCuaHangBanSach.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw_DonHangChiTiet
    {
        public int IDDonHang { get; set; }
        public Nullable<int> IDKhachHang { get; set; }
        public string IDSach { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<System.DateTime> NgayDatHang { get; set; }
        public Nullable<decimal> DonGia { get; set; }
        public Nullable<decimal> TongTien { get; set; }
        public string TrangThai { get; set; }
        public string TenSach { get; set; }
        public Nullable<decimal> GiaBan { get; set; }
        public string HoTen { get; set; }
    }
}
