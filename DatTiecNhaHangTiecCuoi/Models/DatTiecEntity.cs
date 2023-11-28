using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class DatTiecEntity 
    {
        public string MaTiec { get; private set; }

        [Required(ErrorMessage = "Loai hinh tiec khong duoc de trong")]
        public string LoaiHinhTiec { get; set; }
        public DateTime NgayDatTiec { get; set; }

        [Required(ErrorMessage = "Ngay to chuc khong duoc to chuc")]
        [DataType(DataType.DateTime, ErrorMessage = "Ngay thang khong hop le")]
        public DateTime NgayToChuc { get; set; }

        [Required(ErrorMessage = "Thoi gian to chuc khong duoc to chuc")]
        [DataType(DataType.DateTime, ErrorMessage = "Thoi gian khong hop le")]
        public DateTime ThoiGianToChuc { get; set; }

        [Required(ErrorMessage = "So luong ban khong duoc de trong")]
        public int SoLuongBanChinhThuc { get; set; }
        public int SoLuongBanTang { get; set; }
        public int SoLuongBanChay { get; set; }
        public int SoLuongBanDuPhong { get; set; }
        public int TongBanSetup { get; set; }
        public string LoaiBan { get; set; }
        public double PhiDichVu { get; set; }
        public double TongTienDuKien { get; set; }
        public double TongTienGiam { get; set; }
        public double TongTienPhaiTra { get; set; }
        public double TienCocLan1 { get; set; }
        public double TienCocLan2 { get; set; }
        public string GhiChu { get; set; }
        public string MaKhachHang { get; set; }
        public string MaHoaDon { get; set; }
        public DatTiecEntity()
        {
            MaTiec = "TC" + Guid.NewGuid().ToString("N");
        }
    }
}
