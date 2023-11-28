using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class KhachHangEntity 
    {
        public string MaKhachHang { get; set; }

        [Required(ErrorMessage = "Ho ten khong duoc de trong")]
        public string TenKhachHang { get; set; }

        [Required(ErrorMessage = "So dien thoai khong duoc de trong")]
        [RegularExpression(@"^\d+$", ErrorMessage = "So dien thoai khong hop le")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Ngay sinh khong duoc de trong")]
        [DataType(DataType.DateTime, ErrorMessage = "Ngay thang khong hop le")]
        public string NgaySinh { get; set; }
        public string CCCD { get; set; }

        [Required(ErrorMessage = "Email khong duoc de trong")]
        [EmailAddress(ErrorMessage = "Email khong hop le")]
        public string Gmail { get; set; }
    }
}
