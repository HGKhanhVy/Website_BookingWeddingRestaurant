using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class ListThongTinDatTiec
    {
        public List<MonAnEntity> list_monan { get; set; }
        public List<NuocEntity> list_nuoc { get; set; }
        public List<LoaiMonAnEntity> list_loaimonan { get; set; }
        public List<LoaiNuocEntity> list_loainuoc { get; set; }
        public List<LoaiDichVuEntity> list_loaidv { get; set; }
        public List<DichVuEntity> list_dichvu { get; set; }
        public List<DichVuTinhPhiEntity> list_dvtinhphi { get; set; }
        public List<DichVuUuDaiEntity> list_dvuudai { get; set; }
        public List<SanhEntity> list_sanh { get; set; }
        public List<LichSanhTiecEntity> list_lichsanh { get; set; }
    }
}