using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class NhanVienTrongTiecEntity 
    {
        public string MaTiec { get; set; }
        public string MaNhanVien { get; set; }
    }
}
