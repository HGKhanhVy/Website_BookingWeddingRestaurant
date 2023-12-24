using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class ThongTinDatTiec
    {
        // Thông tin khách hàng
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }
        public string NgaySinh { get; set; }
        public string CCCD { get; set; }
        public string Email { get; set; }

        // Thông tin đặt tiệc
        public string LoaiHinhTiec { get; set; }
        public string NgayToChuc { get; set; }
        public string ThoiGianToChuc { get; set; }
        public int SoLuongBanChinhThuc { get; set; }
        public int SoLuongBanChay { get; set; }
        public int SoLuongBanDuPhong { get; set; }
        public int TongBanSetup { get; set; }
        public string LoaiBan { get; set; }
    }
}