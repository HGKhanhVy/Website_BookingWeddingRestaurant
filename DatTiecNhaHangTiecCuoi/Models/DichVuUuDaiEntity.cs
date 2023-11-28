using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class DichVuUuDaiEntity 
    {
        public string MaDichVuUuDai { get; set; }
        public string TenDichVu { get; set; }
        public string DieuKienApDung { get; set; }
        public string MoTa { get; set; }
        public string HinhAnh { get; set; }
        public string MaDichVu { get; set; }
    }
}
