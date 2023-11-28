using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class ChiTietPhuThuMonAnEntity 
    {
        public string MaPhuThu { get; set; }
        public string MaMonAn { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
    }
}
