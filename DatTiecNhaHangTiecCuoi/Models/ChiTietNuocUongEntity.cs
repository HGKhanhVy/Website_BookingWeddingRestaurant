using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class ChiTietNuocUongEntity
    {
        public string MaTiec { get; set; }
        public string MaNuoc { get; set; }
        public int SoLuong { get; set; }
        public double TongTien { get; set; }
        public string TrangThai { get; set; }
    }
}
