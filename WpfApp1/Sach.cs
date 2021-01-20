using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class TheLoai : IComparable<TheLoai>
    {
        public string TheLoaiID { get; set; }
        public string TenTheLoai { get; set; }
        public ICollection<Sach> DSSach { get; set; }
        public int CompareTo(TheLoai that)
        {
            return string.Compare(this.TheLoaiID, that.TheLoaiID);
        }
    }
    public class Sach : IComparable<Sach>
    {
        string sachID, tenSach;
        string soLuong, tacGia;
        string nXB, namXB;
        public Sach() { }
        public Sach(string sachID, string tenSach, string tacGia, string soLuong, string nXB, string namXB, string theLoaiID)
        {
            this.SachID = sachID; this.TenSach = tenSach; this.SoLuong = soLuong; this.TacGia = tacGia;
            NXB = nXB;NamXB = namXB; TheLoaiID = theLoaiID;
        }
        public string SachID { get; set; }
        public string TenSach { get; set; }
        public string TacGia { get; set; }
        public string SoLuong { get; set; }
        public string NXB { get; set; }
        public string NamXB { get; set; }
        public string TheLoaiID { get; set; }
        public TheLoai TheLoai { get; set; }
        public int CompareTo(Sach that)
        {
            return string.Compare(this.SachID, that.SachID);
        }
    }
    public class User : IComparable<User>
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        //public string Role { get; set; }
        public int CompareTo(User that)
        {
            if (this.UserID > that.UserID) return 1;
            if (this.UserID < that.UserID) return -1;
            return 0;
        }
    }
    public class KhachHang
    {
        public string KhachHangID { get; set; }
        public string TenKhachHang { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public int CompareTo(KhachHang that)
        {
            return string.Compare(this.KhachHangID, that.KhachHangID);
        }
    }
    public class HoaDon
    {
        public string HoaDonID { get; set; }
        public string KhachHangID { get; set; }
        public DateTime NgayHoaDon { get; set; }
        public int TongTien { get; set; }
        public int UserID { get; set; }
        public int CompareTo(HoaDon that)
        {
            return string.Compare(this.HoaDonID, that.HoaDonID);
        }
        public KhachHang Khachhang { get; set; }

    }
    public class ChiTietHoaDon
    {
        public string ChiTietHoaDonID { get; set; }
        public string HangHoaID { get; set; }
        public string HoaDonID { get; set; }
        public int SoLuong { get; set; }
        public int ThanhTien { get; set; }
        public int CompareTo(ChiTietHoaDon that)
        {
            return string.Compare(this.ChiTietHoaDonID, that.ChiTietHoaDonID);
        }
        public Sach Sach { get; set; }
        public HoaDon HoaDon { get; set; }
    }
    public class MyEntity : DbContext
    {
        public DbSet<TheLoai> DSTheLoai { get; set; }
        public DbSet<Sach> DSSach { get; set; }
        public DbSet<User> DSUser { get; set; }
        public DbSet<ChiTietHoaDon> DSCTHD { get; set; }
        public DbSet<HoaDon> DSHD { get; set; }
        public DbSet<KhachHang> DSKhanhHang { get; set; }
    }
}
