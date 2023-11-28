using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class HoaDonEntity 
    {
        public string MaHoaDon { get; set; }
        public string NgayLap { get; set; }
        public double TongTienPhuThu { get; set; }
        public double TongTienThanhToan { get; set; }
        public string MaTiec { get; set; }
    }
}
