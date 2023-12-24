using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class NhanVienEntity 
    {
        public string MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public string SoDienThoai { get; set; }
        public string NgaySinh { get; set; }
        public string CCCD { get; set; }
        public string Gmail { get; set; }
        public string NgayVaoLam { get; set; }
        public string NgayNghiViec { get; set; }
        public string MatKhau { get; set; }
        public string TrangThai { get; set; }

    }
}
