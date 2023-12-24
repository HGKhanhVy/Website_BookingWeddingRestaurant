using DatTiecNhaHangTiecCuoi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DatTiecNhaHangTiecCuoi.Controllers
{
    public class GioHangController : Controller
    {
        List<MonAnDaChon> listMonAnDaChon = new List<MonAnDaChon>();
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GioHang()
        {
            KhachHangEntity kh =  Session["ThongTinKhachHang"] as KhachHangEntity;
            if (kh != null)
            {
                ViewBag.TenKhachHang = kh.TenKhachHang;
                ViewBag.SoDienThoai = kh.SoDienThoai;
                ViewBag.NgaySinh = kh.NgaySinh;
                ViewBag.CCCD = kh.CCCD;
                ViewBag.Email = kh.Gmail;
            }

            DatTiecEntity dt = Session["ThongTinTiec"] as DatTiecEntity;
            if (dt != null)
            {
                ViewBag.MaTiec = dt.MaTiec;
                ViewBag.LoaiHinhTiec = dt.LoaiHinhTiec;
                ViewBag.NgayToChuc = dt.NgayToChuc;
                ViewBag.ThoiGianToChuc = dt.ThoiGianToChuc;
                ViewBag.SoLuongBanChinhThuc = dt.SoLuongBanChinhThuc;
                ViewBag.SoLuongBanTang = dt.SoLuongBanTang;
                ViewBag.SoLuongBanChay = dt.SoLuongBanChay;
                ViewBag.SoLuongBanDuPhong = dt.SoLuongBanDuPhong;
                ViewBag.TongBanSetup = dt.TongBanSetup;
                ViewBag.LoaiBan = dt.LoaiBan;
            }
            // MonAn
            List<MonAnDaChon> danhSachMonAn = Session["MonAnDaChon"] as List<MonAnDaChon>;
            if (danhSachMonAn == null)
            {
                danhSachMonAn = new List<MonAnDaChon>();
                Session["MonAnDaChon"] = danhSachMonAn;
            }
            ViewBag.DanhSachMonAn = danhSachMonAn;

            // ThucUong
            List<NuocDaChon> danhSachNuoc = Session["NuocDaChon"] as List<NuocDaChon>;
            if (danhSachNuoc == null)
            {
                danhSachNuoc = new List<NuocDaChon>();
                Session["NuocDaChon"] = danhSachNuoc;
            }
            ViewBag.DanhSachNuoc = danhSachNuoc;

            // DichVuTinhPhi
            List<DichVuTinhPhiDaChon> danhSachDVTP = Session["DichVuTinhPhiDaChon"] as List<DichVuTinhPhiDaChon>;
            if (danhSachDVTP == null)
            {
                danhSachDVTP = new List<DichVuTinhPhiDaChon>();
                Session["DichVuTinhPhiDaChon"] = danhSachDVTP;
            }
            ViewBag.DanhSachDVTP = danhSachDVTP;

            ViewBag.TongTienDuKien = (double)(Session["TongTienMonAn"] ?? 0) + (double)(Session["TongTienNuoc"] ?? 0) + (double)(Session["TongTienDichVu"] ?? 0);

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> LuuThongTinMonAn(List<MonAnDaChon> selectedItemsMonAn)
        {
            double donGiaMenu = 0;
            try
            {
                List<MonAnDaChon> danhSachMonAn = Session["MonAnDaChon"] as List<MonAnDaChon>;
                if (danhSachMonAn == null)
                {
                    danhSachMonAn = new List<MonAnDaChon>();
                }
                danhSachMonAn.AddRange(selectedItemsMonAn);
                Session["MonAnDaChon"] = danhSachMonAn;

                foreach (MonAnDaChon mon in danhSachMonAn)
                {
                    donGiaMenu = donGiaMenu + mon.DonGia;
                }
                Session["TongTienMonAn"] = donGiaMenu;

                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl_Menu = "https://localhost:7267/api/menu/add";
                    MenuEntity menu = new MenuEntity
                    {
                        MaMenu = "TD" + Guid.NewGuid().ToString("N"),
                        TenMenu = "THỰC ĐƠN " + Guid.NewGuid().ToString("N"),
                        DonGiaMenu = donGiaMenu,
                        TrangThai = "null"
                    };
                    TempData["ThongTinMenu"] = menu;
                    string jsonContentMenu = Newtonsoft.Json.JsonConvert.SerializeObject(menu);
                    var contentMenu = new StringContent(jsonContentMenu, Encoding.UTF8, "application/json");
                    var responseMenu = await httpClient.PostAsync(apiBaseUrl_Menu, contentMenu);

                    if (responseMenu.IsSuccessStatusCode)
                    {
                        var dt = Session["ThongTinTiec"] as DatTiecEntity;
                        var menuDaLuu = TempData["ThongTinMenu"] as MenuEntity;

                        // Mon AN Trong Menu
                        string apiBaseUrl_MonAnMenu = "https://localhost:7267/api/mon-an-trong-menu/add";
                        foreach (MonAnDaChon mon in danhSachMonAn)
                        {
                            MonAnTrongMenuEntity monMenu = new MonAnTrongMenuEntity
                            {
                                MaMenu = menuDaLuu.MaMenu,
                                MaMonAn = mon.MaMonAn,
                                SoLuongMon = dt.SoLuongBanChinhThuc,
                                DonGia = mon.DonGia,
                                TrangThai = "null"
                            };
                            string jsonContentMonMenu = Newtonsoft.Json.JsonConvert.SerializeObject(monMenu);
                            var contentMonMenu = new StringContent(jsonContentMonMenu, Encoding.UTF8, "application/json");
                            var responseMonMenu = await httpClient.PostAsync(apiBaseUrl_MonAnMenu, contentMonMenu);
                        }

                        // Chi Tiet Menu
                        string apiBaseUrl_CTMenu = "https://localhost:7267/api/chi-tiet-menu/add";
                        ChiTietMenuEntity ctMenu = new ChiTietMenuEntity
                        {
                            MaTiec = dt.MaTiec,
                            MaMenu = menuDaLuu.MaMenu,
                            SoLuongMenuCuaTiec = dt.SoLuongBanChinhThuc,
                            TongTien = menuDaLuu.DonGiaMenu,
                            TrangThai = "null"
                        };
                        string jsonContentCTMenu = Newtonsoft.Json.JsonConvert.SerializeObject(ctMenu);
                        var contentCTMenu = new StringContent(jsonContentCTMenu, Encoding.UTF8, "application/json");
                        var responseCTMenu = await httpClient.PostAsync(apiBaseUrl_CTMenu, contentCTMenu);

                        return Json(new { success = true, message = "Lưu menu thành công." });
                    }
                    else
                    {
                        return Json(new { success = true, message = "Lỗi khi lưu menu." });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return Json(new { success = true, message = "Đã có lỗi xảy ra." });
            }
        }

        [HttpPost]
        public async Task<JsonResult> LuuThongTinNuoc(List<NuocDaChon> selectedItemsNuoc)
        {
            double tongTien = 0;
            try
            {
                List<NuocDaChon> danhSachNuoc = Session["NuocDaChon"] as List<NuocDaChon>;
                if (danhSachNuoc == null)
                {
                    danhSachNuoc = new List<NuocDaChon>();
                }
                danhSachNuoc.AddRange(selectedItemsNuoc);
                foreach (NuocDaChon nuoc in danhSachNuoc)
                {
                    tongTien = tongTien + nuoc.DonGia;
                }
                Session["NuocDaChon"] = danhSachNuoc;
                Session["TongTienNuoc"] = tongTien;

                using (var httpClient = new HttpClient())
                {
                    var dt = Session["ThongTinTiec"] as DatTiecEntity;
                    string apiBaseUrl_CTNuoc = "https://localhost:7267/api/chi-tiet-nuoc-uong/add";
                    foreach (NuocDaChon nuoc in danhSachNuoc)
                    {
                        ChiTietNuocUongEntity ctNuoc = new ChiTietNuocUongEntity
                        {
                            MaTiec = dt.MaTiec,
                            MaNuoc = nuoc.MaNuoc,
                            SoLuong = dt.SoLuongBanChinhThuc,
                            TongTien = tongTien,
                            TrangThai = "null"
                        };
                        string jsonContentCTNuoc = Newtonsoft.Json.JsonConvert.SerializeObject(ctNuoc);
                        var contentCTNuoc = new StringContent(jsonContentCTNuoc, Encoding.UTF8, "application/json");
                        var responseCTNuoc = await httpClient.PostAsync(apiBaseUrl_CTNuoc, contentCTNuoc);
                    }
                    return Json(new { success = true, message = "Dữ liệu đã được lưu vào Session." });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return Json(new { success = true, message = "Đã có lỗi xảy ra." });
            }
        }

        [HttpPost]
        public async Task<JsonResult> LuuThongTinSanh(string selectedItemsSanh)
        {
            try
            {
                string maSanh = Session["MaSanh"] as string;
                if (maSanh == null)
                {
                    maSanh = "";
                }
                Session["MaSanh"] = selectedItemsSanh;

                using (var httpClient = new HttpClient())
                {
                    var dt = Session["ThongTinTiec"] as DatTiecEntity;
                    var maSanhDaLuu = Session["MaSanh"] as string;
                    string apiBaseUrl_LichSanh = "https://localhost:7267/api/lich-sanh-tiec/add";
                    LichSanhTiecEntity lich = new LichSanhTiecEntity
                    {
                        MaTiec = dt.MaTiec,
                        MaSanh = maSanhDaLuu,
                        NgayDienRa = DateTime.Parse(dt.NgayToChuc),
                        Ca = dt.ThoiGianToChuc,
                        TienPhuThu = 0,
                        TrangThai = "null"
                    };
                    string jsonContentLich = Newtonsoft.Json.JsonConvert.SerializeObject(lich);
                    var contentLich = new StringContent(jsonContentLich, Encoding.UTF8, "application/json");
                    var responseLich = await httpClient.PostAsync(apiBaseUrl_LichSanh, contentLich);
                    if (responseLich.IsSuccessStatusCode)
                    {
                        return Json(new { success = true, message = "Dữ liệu đã được lưu vào Session." });
                    }
                    else
                    {
                        return Json(new { success = true, message = "Đã có lỗi xảy ra." });
                    }    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return Json(new { success = true, message = "Đã có lỗi xảy ra." });
            }
        }

        [HttpPost]
        public async Task<JsonResult> LuuThongTinDichVu(List<DichVuTinhPhiDaChon> selectedItemsDV)
        {
            double tongTien = 0;
            try
            {
                List<DichVuTinhPhiDaChon> danhSachDV = Session["DichVuTinhPhiDaChon"] as List<DichVuTinhPhiDaChon>;
                if (danhSachDV == null)
                {
                    danhSachDV = new List<DichVuTinhPhiDaChon>();
                }
                danhSachDV.AddRange(selectedItemsDV);
                foreach (DichVuTinhPhiDaChon dv in danhSachDV)
                {
                    tongTien = tongTien + dv.GiaGiam30 + dv.GiaLe;
                }
                Session["DichVuTinhPhiDaChon"] = danhSachDV;
                Session["TongTienDichVu"] = tongTien;

                using (var httpClient = new HttpClient())
                {
                    var dt = Session["ThongTinTiec"] as DatTiecEntity;
                    string apiBaseUrl_DVTP = "https://localhost:7267/api/chi-tiet-dv-tinh-phi/add";
                    foreach (DichVuTinhPhiDaChon dv in danhSachDV)
                    {
                        ChiTietDichVuTinhPhiEntity dvtp = new ChiTietDichVuTinhPhiEntity
                        {
                            MaTiec = dt.MaTiec,
                            MaDichVuTinhPhi = dv.MaDichVuTinhPhi,
                            TrangThai = "null"
                        };
                        string jsonContentDV = Newtonsoft.Json.JsonConvert.SerializeObject(dvtp);
                        var contentDV = new StringContent(jsonContentDV, Encoding.UTF8, "application/json");
                        var responseDV = await httpClient.PostAsync(apiBaseUrl_DVTP, contentDV);
                    }
                    return Json(new { success = true, message = "Dữ liệu đã được lưu vào Session." });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return Json(new { success = true, message = "Đã có lỗi xảy ra." });
            }
        }

        public ActionResult HopDong()
        {
            return View();
        }
    }
}