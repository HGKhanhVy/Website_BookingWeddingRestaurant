using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class DichVuTinhPhiEntity 
    {
        public string MaDichVuTinhPhi { get; set; }
        public string TenDichVu { get; set; }
        public string MoTa { get; set; }
        public string HinhAnh { get; set; }
        public double DonGiaDichVu { get; set; }
        public string MaDichVu { get; set; }
    }
}
