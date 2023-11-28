using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class PhuThuEntity 
    {
        public string MaPhuThu { get; set; }
        public string LoaiPhuThu { get; set; }
        public string MoTaPhuThu { get; set; }
        public double TongTien { get; set; }
        public string MaHoaDon { get; set; }
    }
}
