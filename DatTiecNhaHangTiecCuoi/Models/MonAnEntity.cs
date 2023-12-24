using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class MonAnEntity 
    {
        public string MaMonAn { get; set; }
        public string TenMonAn { get; set; }
        public double DonGia { get; set; }
        public string HinhAnh { get; set; }
        public string DVT { get; set; }
        public string MaLoaiMonAn { get; set; }
        public string TrangThai { get; set; }
    }
}
