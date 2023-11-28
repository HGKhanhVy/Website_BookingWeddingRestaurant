using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class LichSanhTiecEntity 
    {
        public string MaTiec { get; set; }
        public string MaSanh { get; set; }
        public DateTime NgayDienRa { get; set; }
        public string Ca { get; set; } 
        public double TienPhuThu { get; set; }

    }
}
