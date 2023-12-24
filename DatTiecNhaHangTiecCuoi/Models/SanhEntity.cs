using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class SanhEntity
    {
        public string MaSanh { get; set; }
        public string TenSanh { get; set; }
        public string HinhAnh { get; set; }
        public int SucChuaToiThieu { get; set;}
        public int SucChuaToiDa { get; set; }
        public string TrangThai { get; set; }
    }
}
