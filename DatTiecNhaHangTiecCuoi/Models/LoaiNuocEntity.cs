﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatTiecNhaHangTiecCuoi.Models
{ 
    public class LoaiNuocEntity 
    {
        public string MaLoaiNuoc { get; set; }
        public string TenLoaiNuoc { get; set; }
    }
}
