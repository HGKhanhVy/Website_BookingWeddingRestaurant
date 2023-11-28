using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class NuocEntity 
    {
        public string MaNuoc{ get; set; }
        public string TenNuoc { get; set; }
        public string DVT { get; set; }
        public string HinhAnh { get; set; }
        public double DonGia { get; set; }
        public string MaLoaiNuoc { get; set; }
    }
}
