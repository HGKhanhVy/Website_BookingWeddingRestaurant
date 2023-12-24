using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class List_Loai_MonAn_Nuoc
    {
        public List<MonAnEntity> list_monan { get; set; }
        public List<NuocEntity> list_nuoc { get; set; }
        public List<LoaiMonAnEntity> list_loaimonan { get; set; }
        public List<LoaiNuocEntity> list_loainuoc { get; set; }
    }
}