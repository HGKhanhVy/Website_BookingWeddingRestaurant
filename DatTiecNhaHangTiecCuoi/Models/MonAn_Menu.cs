using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatTiecNhaHangTiecCuoi.Models
{
    public class MonAn_Menu
    {
        public List<MenuEntity> dsMenu { get; set; }
        public List<MonAnEntity> dsMonAn { get; set; }
        public List<MonAnTrongMenuEntity> dsMonTrongMenu { get; set; }
    }
}