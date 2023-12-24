using DatTiecNhaHangTiecCuoi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace DatTiecNhaHangTiecCuoi.Controllers
{
    public class DatTiecController : Controller
    {
        // GET: DatTiec
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DatTiec()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> DatTiec(ThongTinDatTiec model)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl_KH = "https://localhost:7267/api/khach-hang/add";
                    KhachHangEntity kh = new KhachHangEntity
                    {
                        MaKhachHang = "KH" + Guid.NewGuid().ToString("N"),
                        TenKhachHang = model.TenKhachHang,
                        SoDienThoai = model.SoDienThoai,
                        NgaySinh = model.NgaySinh,
                        CCCD = model.CCCD,
                        Gmail = model.Email,
                        MatKhau = "12345"
                    };
                    Session["ThongTinKhachHang"] = kh;

                    string apiBaseUrl_DT = "https://localhost:7267/api/dat-tiec/add";
                    DatTiecEntity dt = new DatTiecEntity
                    {
                        MaTiec = "TC" + Guid.NewGuid().ToString("N"),
                        LoaiHinhTiec = model.LoaiHinhTiec,
                        NgayDatTiec = DateTime.Now,
                        NgayToChuc = model.NgayToChuc,
                        ThoiGianToChuc = model.ThoiGianToChuc,
                        SoLuongBanChinhThuc = model.SoLuongBanChinhThuc,
                        SoLuongBanTang = (model.SoLuongBanChinhThuc >= 50) ? 1 : 0,
                        SoLuongBanChay = model.SoLuongBanChay,
                        SoLuongBanDuPhong = model.SoLuongBanDuPhong,
                        TongBanSetup = model.TongBanSetup,
                        LoaiBan = model.LoaiBan,
                        TrangThai = null,
                        PhiDichVu = 140.000,
                        TongTienDuKien = 0,
                        TongTienGiam = 0,
                        TongTienPhaiTra = 0,
                        TienCocLan1 = 0,
                        TienCocLan2 = 0,
                        GhiChu = null,
                        MaKhachHang = kh.MaKhachHang
                    };
                    Session["ThongTinTiec"] = dt;
                    Session["NgayToChuc"] = model.NgayToChuc;
                    Session["CaToChuc"] = model.ThoiGianToChuc;

                    // Chuyển đối tượng model thành chuỗi JSON
                    string jsonContentKH = Newtonsoft.Json.JsonConvert.SerializeObject(kh);
                    string jsonContentDT = Newtonsoft.Json.JsonConvert.SerializeObject(dt);

                    // Tạo nội dung request
                    var contentKH = new StringContent(jsonContentKH, Encoding.UTF8, "application/json");
                    var contentDT = new StringContent(jsonContentDT, Encoding.UTF8, "application/json");

                    // Gửi POST request đến API
                    var responseKH = await httpClient.PostAsync(apiBaseUrl_KH, contentKH);
                    var responseDT = await httpClient.PostAsync(apiBaseUrl_DT, contentDT);

                    if (responseKH.IsSuccessStatusCode && responseDT.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ThongTinDatTiec", "DatTiec");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }

        public async Task<ActionResult> ThongTinDatTiec()
        {
            ListThongTinDatTiec model = new ListThongTinDatTiec();
            var ngay = Session["NgayToChuc"] as string;
            var ca = Session["CaToChuc"] as string;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // SẢNH
                    string apiBaseUrl_TatCaSanh = "https://localhost:7267/api/sanh/get-all";
                    string apiBaseUrl_TatCaLichSanh = "https://localhost:7267/api/lich-sanh-tiec/get-all";

                    // Gọi API để lấy tất cả thông tin sảnh và lịch sảnh tiệc
                    var responseSanh = await httpClient.GetAsync(apiBaseUrl_TatCaSanh);
                    var responseLichSanh = await httpClient.GetAsync(apiBaseUrl_TatCaLichSanh);

                    if (responseSanh.IsSuccessStatusCode && responseLichSanh.IsSuccessStatusCode)
                    {
                        // Đọc dữ liệu JSON
                        var jsonDataSanh = await responseSanh.Content.ReadAsStringAsync();
                        var jObjectSanh = JObject.Parse(jsonDataSanh);

                        var jsonDataLichSanh = await responseLichSanh.Content.ReadAsStringAsync();
                        var jObjectLichSanh = JObject.Parse(jsonDataLichSanh);

                        // Chuyển đổi dữ liệu JSON thành danh sách các đối tượng
                        var sanhList = jObjectSanh["data"].ToObject<List<SanhEntity>>();
                        var lichSanhList = jObjectLichSanh["data"].ToObject<List<LichSanhTiecEntity>>();

                        // Lọc ra những sảnh không có lịch vào ngày và ca tổ chức
                        var sanhKhongCoLich = sanhList
                            .Where(s => !lichSanhList.Any(l => l.MaSanh == s.MaSanh
                                                             && l.NgayDienRa.Date == DateTime.Parse(ngay).Date
                                                             && l.Ca == ca))
                            .ToList();

                        model.list_sanh = sanhKhongCoLich;
                    }
                    else
                    {
                        return View("Error");
                    }

                    // MÓN ĂN - NƯỚC
                    var response_MonAn = await httpClient.GetAsync("https://localhost:7267/api/mon-an/get-all");
                    response_MonAn.EnsureSuccessStatusCode();
                    var response_Nuoc = await httpClient.GetAsync("https://localhost:7267/api/nuoc-uong/get-all");
                    response_Nuoc.EnsureSuccessStatusCode();
                    var response_LoaiMonAn = await httpClient.GetAsync("https://localhost:7267/api/loai-mon-an/get-all");
                    response_LoaiMonAn.EnsureSuccessStatusCode();
                    var response_LoaiNuoc = await httpClient.GetAsync("https://localhost:7267/api/loai-nuoc/get-all");
                    response_LoaiNuoc.EnsureSuccessStatusCode();

                    var jsonString_MonAn = await response_MonAn.Content.ReadAsStringAsync();
                    var jsonObject_MonAn = JObject.Parse(jsonString_MonAn);
                    var danhSachMonAn = jsonObject_MonAn["data"].ToObject<List<MonAnEntity>>();

                    var jsonString_Nuoc = await response_Nuoc.Content.ReadAsStringAsync();
                    var jsonObject_Nuoc = JObject.Parse(jsonString_Nuoc);
                    var danhSachNuoc = jsonObject_Nuoc["data"].ToObject<List<NuocEntity>>();

                    var jsonString_LoaiMon = await response_LoaiMonAn.Content.ReadAsStringAsync();
                    var jsonObject_LoaiMon = JObject.Parse(jsonString_LoaiMon);
                    var danhSachLoaiMon = jsonObject_LoaiMon["data"].ToObject<List<LoaiMonAnEntity>>();

                    var jsonString_LoaiNuoc = await response_LoaiNuoc.Content.ReadAsStringAsync();
                    var jsonObject_LoaiNuoc = JObject.Parse(jsonString_LoaiNuoc);
                    var danhSachLoaiNuoc = jsonObject_LoaiNuoc["data"].ToObject<List<LoaiNuocEntity>>();

                    model.list_monan = danhSachMonAn;
                    model.list_nuoc = danhSachNuoc;
                    model.list_loaimonan = danhSachLoaiMon;
                    model.list_loainuoc = danhSachLoaiNuoc;

                    // DỊCH VỤ
                    var response_LoaiDV = await httpClient.GetAsync("https://localhost:7267/api/loai-dich-vu/get-all");
                    var response_DV = await httpClient.GetAsync("https://localhost:7267/api/dich-vu/get-all");
                    var response_DVTinhPhi = await httpClient.GetAsync("https://localhost:7267/api/dich-vu-tinh-phi/get-all");
                    response_LoaiDV.EnsureSuccessStatusCode();
                    response_DV.EnsureSuccessStatusCode();
                    response_DVTinhPhi.EnsureSuccessStatusCode();

                    var jsonString = await response_LoaiDV.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(jsonString);
                    var danhSachLoaiDV = jsonObject["data"].ToObject<List<LoaiDichVuEntity>>();

                    var jsonString_DV = await response_DV.Content.ReadAsStringAsync();
                    var jsonObject_DV = JObject.Parse(jsonString_DV);
                    var danhSachDV = jsonObject_DV["data"].ToObject<List<DichVuEntity>>();

                    var jsonString_DVTinhPhi = await response_DVTinhPhi.Content.ReadAsStringAsync();
                    var jsonObject_DVTinhPhi = JObject.Parse(jsonString_DVTinhPhi);
                    var danhSachDVTinhPhi = jsonObject_DVTinhPhi["data"].ToObject<List<DichVuTinhPhiEntity>>();


                    List<string> madvtinh = new List<string>();
                    List<LoaiDichVuEntity> list_loaidv = new List<LoaiDichVuEntity>();
                    List<string> list_loai_dv = new List<string>();

                    // lấy mã dich vụ từ dịch vụ tính phí
                    foreach (var tinhphi in danhSachDVTinhPhi)
                    {
                        madvtinh.Add(tinhphi.MaDichVu);
                    }

                    // lấy mã loại dich vụ từ dịch vụ
                    foreach (var maloaidv in danhSachDV)
                    {
                        foreach (var item in madvtinh)
                        {
                            if (item.Equals(maloaidv.MaDichVu))
                                list_loai_dv.Add(maloaidv.MaLoaiDichVu);
                        }
                    }

                    foreach (var item in danhSachLoaiDV)
                    {
                        int count = 0;
                        foreach (var item1 in list_loai_dv)
                        {
                            if (item1 == item.MaLoaiDichVu)
                            {
                                if (count == 0)
                                {
                                    list_loaidv.Add(item);
                                    count++;
                                }
                            }

                        }
                    }
                    model.list_loaidv = list_loaidv;
                    model.list_dichvu = danhSachDV;
                    model.list_dvtinhphi = danhSachDVTinhPhi;

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }

        public void ClearCart()
        {
            Session["ThongTinKhachHang"] = null;
            Session["ThongTinTiec"] = null;
            Session["MonAnDaChon"] = null;
            Session["NuocDaChon"] = null;
            Session["DichVuTinhPhiDaChon"] = null;
            Session["TongTienMonAn"] = 0;
            Session["TongTienNuoc"] = 0;
            Session["TongTienDichVu"] = 0;
        }

        public ActionResult BackToDatTiec()
        {
            ClearCart();
            return RedirectToAction("DatTiec", "DatTiec");
        }
    }
}