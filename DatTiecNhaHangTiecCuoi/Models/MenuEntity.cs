using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatTiecNhaHangTiecCuoi.Models
{ 
    public class MenuEntity 
    {
        public string MaMenu { get; set; }
        public string TenMenu { get; set; }
        public double DonGiaMenu { get; set; }
    }
}
