using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class List_LichSanh_Sanh
    {
        public List<SanhEntity> list_sanh { get; set; }
        public List<LichSanhTiecEntity> list_lichsanh { get; set; }
    }
}