using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class List_LoaiDichVu_DichVu
    {
        public List<LoaiDichVuEntity> list_loaidv { get; set; }
        public List<DichVuEntity> list_dichvu { get; set; }
        public List<DichVuTinhPhiEntity> list_dvtinhphi { get; set; }
        public List<DichVuUuDaiEntity> list_dvuudai { get; set; }
    }
}