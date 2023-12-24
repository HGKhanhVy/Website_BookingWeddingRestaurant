using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class DanhSachDaChon
    {
        public List<MonAnDaChon> DanhSachMonAn { get; set; }
        public List<NuocDaChon> DanhSachNuoc { get; set; }
        public DanhSachDaChon()
        {
            DanhSachMonAn = new List<MonAnDaChon>();
            DanhSachNuoc = new List<NuocDaChon>();
        }
    }
}