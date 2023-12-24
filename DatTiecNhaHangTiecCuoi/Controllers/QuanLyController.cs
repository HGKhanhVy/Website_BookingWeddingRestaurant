using DatTiecNhaHangTiecCuoi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DatTiecNhaHangTiecCuoi.Controllers
{
    public class QuanLyController : Controller
    {
        // GET: QuanLy

        public ActionResult Home()
        {
            return View();
        }

        public async Task<ActionResult> QuanLyDichVu()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl_DichVu = "https://localhost:7267/api/dich-vu/get-all";

                    var responseDichVu = await httpClient.GetAsync(apiBaseUrl_DichVu);

                    if (responseDichVu.IsSuccessStatusCode)
                    {
                        var jsonDataDichVu = await responseDichVu.Content.ReadAsStringAsync();
                        var jObjectDichVu = JObject.Parse(jsonDataDichVu);

                        // Chuyển đổi dữ liệu JSON thành danh sách các đối tượng
                        var dvList = jObjectDichVu["data"].ToObject<List<DichVuEntity>>();

                        return View(dvList);
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

        [HttpPost]
        public async Task<ActionResult> XoaDichVu(string maDichVu)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl_DV = $"https://localhost:7267/api/dich-vu/delete/{maDichVu}";

                    var responseDV = await httpClient.DeleteAsync(apiBaseUrl_DV);

                    if (responseDV.IsSuccessStatusCode)
                    {
                        return View("QuanLyDichVu", "QuanLy");
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

        public async Task<ActionResult> QuanLyTiec()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl_Tiec = "https://localhost:7267/api/dat-tiec/get-all";
                    var responseTiec = await httpClient.GetAsync(apiBaseUrl_Tiec);

                    string apiBaseUrl_KH = "https://localhost:7267/api/khach-hang/get-all";
                    var responseKH = await httpClient.GetAsync(apiBaseUrl_KH);

                    if (responseTiec.IsSuccessStatusCode && responseKH.IsSuccessStatusCode)
                    {
                        var jsonDataTiec = await responseTiec.Content.ReadAsStringAsync();
                        var jObjectTiec = JObject.Parse(jsonDataTiec);
                        var tiecList = jObjectTiec["data"].ToObject<List<DatTiecEntity>>();

                        var jsonDataKH = await responseKH.Content.ReadAsStringAsync();
                        var jObjectKH = JObject.Parse(jsonDataKH);
                        var khList = jObjectKH["data"].ToObject<List<KhachHangEntity>>();
                        ViewBag.ListKH = khList;

                        return View(tiecList);
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

        [HttpPost]
        public async Task<ActionResult> XoaTiec(string maTiec)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl_Tiec = $"https://localhost:7267/api/dat-tiec/delete/{maTiec}";

                    var responseTiec = await httpClient.DeleteAsync(apiBaseUrl_Tiec);

                    if (responseTiec.IsSuccessStatusCode)
                    {
                        return View("QuanLyDichVu", "QuanLy");
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

        public async Task<ActionResult> QuanLyKhachHang()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl_KH = "https://localhost:7267/api/khach-hang/get-all";

                    var responseKH = await httpClient.GetAsync(apiBaseUrl_KH);

                    if (responseKH.IsSuccessStatusCode)
                    {
                        var jsonDataKH = await responseKH.Content.ReadAsStringAsync();
                        var jObjectKH = JObject.Parse(jsonDataKH);

                        // Chuyển đổi dữ liệu JSON thành danh sách các đối tượng
                        var khList = jObjectKH["data"].ToObject<List<KhachHangEntity>>();

                        return View(khList);
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

        [HttpPost]
        public async Task<ActionResult> XoaKhachHang(string maKhachHang)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl_KH = $"https://localhost:7267/api/khach-hang/delete/{maKhachHang}";
                    string apiBaseUrl_Tiec = $"https://localhost:7267/api/dat-tiec/deleteByAnotherID/{maKhachHang}";

                    var responseKH = await httpClient.DeleteAsync(apiBaseUrl_KH);
                    var responseTiec = await httpClient.DeleteAsync(apiBaseUrl_Tiec);

                    if (responseKH.IsSuccessStatusCode && responseTiec.IsSuccessStatusCode)
                    {
                        return View("QuanLyDichVu", "QuanLy");
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

        public async Task<ActionResult> QuanLyNhanVien()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl_NV = "https://localhost:7267/api/nhan-vien/get-all";

                    var responseNV = await httpClient.GetAsync(apiBaseUrl_NV);

                    if (responseNV.IsSuccessStatusCode)
                    {
                        var jsonDataNV = await responseNV.Content.ReadAsStringAsync();
                        var jObjectNV = JObject.Parse(jsonDataNV);

                        // Chuyển đổi dữ liệu JSON thành danh sách các đối tượng
                        var nvList = jObjectNV["data"].ToObject<List<NhanVienEntity>>();

                        return View(nvList);
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

        [HttpGet]
        public ActionResult ThemNhanVien()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ThemNhanVien(NhanVienEntity model)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl = "https://localhost:7267/api/nhan-vien/add";
                    NhanVienEntity nv = new NhanVienEntity
                    {
                        MaNhanVien = "NV-" + Guid.NewGuid().ToString("N"),
                        TenNhanVien = model.TenNhanVien,
                        SoDienThoai = model.SoDienThoai,
                        NgaySinh = model.NgaySinh,
                        CCCD = model.CCCD,
                        Gmail = model.Gmail,
                        NgayVaoLam = DateTime.Now.ToString("dd/MM/yyyy"),
                        NgayNghiViec = "null",
                        TrangThai = "null",
                        MatKhau = model.MatKhau
                    };

                    string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(nv);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(apiBaseUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("QuanLyNhanVien", "QuanLy");
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

        // Lấy Nhân Viên
        private async Task<NhanVienEntity> GetNhanVienFromApi(string MaNhanVien)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://localhost:7267/api/nhan-vien/get-single/{MaNhanVien}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(jsonData);
                    var nvEntity = jObject["data"].ToObject<NhanVienEntity>();
                    return nvEntity;
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> SuaNhanVien(string MaNhanVien)
        {
            var nvEntity = await GetNhanVienFromApi(MaNhanVien);

            if (nvEntity == null)
            {
                return HttpNotFound();
            }
            return View(nvEntity);
        }
        [HttpPost]
        public async Task<ActionResult> SuaNhanVien(NhanVienEntity model)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    NhanVienEntity nv = new NhanVienEntity
                    {
                        MaNhanVien = model.MaNhanVien,
                        TenNhanVien = model.TenNhanVien,
                        SoDienThoai = model.SoDienThoai,
                        NgaySinh = model.NgaySinh,
                        CCCD = model.CCCD,
                        Gmail = model.Gmail,
                        NgayVaoLam = model.NgayVaoLam,
                        NgayNghiViec = model.NgayNghiViec,
                        TrangThai = model.TrangThai,
                        MatKhau = model.MatKhau
                    };

                    string apiBaseUrl = $"https://localhost:7267/api/nhan-vien/update/{model.MaNhanVien}";

                    var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(nv);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await httpClient.PutAsync(apiBaseUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("QuanLyNhanVien", "QuanLy");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> XoaNhanVien(string maNhanVien)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl_NV = $"https://localhost:7267/api/nhan-vien/delete/{maNhanVien}";

                    var responseNV = await httpClient.DeleteAsync(apiBaseUrl_NV);

                    if (responseNV.IsSuccessStatusCode)
                    {
                        return View("QuanLyDichVu", "QuanLy");
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

        public async Task<ActionResult> QuanLyMonAn()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl_Mon = "https://localhost:7267/api/mon-an/get-all";

                    var responseMon = await httpClient.GetAsync(apiBaseUrl_Mon);

                    if (responseMon.IsSuccessStatusCode)
                    {
                        var jsonDataMon = await responseMon.Content.ReadAsStringAsync();
                        var jObjectMon = JObject.Parse(jsonDataMon);

                        // Chuyển đổi dữ liệu JSON thành danh sách các đối tượng
                        var monList = jObjectMon["data"].ToObject<List<MonAnEntity>>();

                        return View(monList);
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

        private async Task<List<LoaiMonAnEntity>> GetLoaiMonAnFromApi()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:7267/api/loai-mon-an/get-all");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(jsonData);
                    var loaiMonEntity = jObject["data"].ToObject<List<LoaiMonAnEntity>>();
                    return loaiMonEntity;
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> ThemMonAn()
        {
            var loaiMonEntity = await GetLoaiMonAnFromApi();

            if (loaiMonEntity == null)
            {
                return HttpNotFound();
            }
            return View(loaiMonEntity);
        }
        [HttpPost]
        public async Task<ActionResult> ThemMonAn(MonAnEntity model, HttpPostedFileBase fileAnh)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl = "https://localhost:7267/api/mon-an/add";
                    MonAnEntity mon = new MonAnEntity();
                    if (fileAnh == null || fileAnh.ContentLength == 0)
                    {
                        mon = new MonAnEntity
                        {
                            MaMonAn = "MA-" + Guid.NewGuid().ToString("N"),
                            TenMonAn = model.TenMonAn,
                            DonGia = model.DonGia,
                            HinhAnh = "null",
                            DVT = model.DVT,
                            MaLoaiMonAn = model.MaLoaiMonAn,
                            TrangThai = "null"
                        };
                    }
                    else
                    {
                        mon = new MonAnEntity
                        {
                            MaMonAn = "MA-" + Guid.NewGuid().ToString("N"),
                            TenMonAn = model.TenMonAn,
                            DonGia = model.DonGia,
                            HinhAnh = fileAnh.FileName,
                            DVT = model.DVT,
                            MaLoaiMonAn = model.MaLoaiMonAn,
                            TrangThai = "null"
                        };
                    }

                    string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(mon);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(apiBaseUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var baseUrlTuongDoi = "/Image/AnhMonAn/";
                        var baseUrlTuyetDoi = Server.MapPath(baseUrlTuongDoi);
                        fileAnh.SaveAs(baseUrlTuyetDoi + fileAnh.FileName);

                        return RedirectToAction("QuanLyMonAn", "QuanLy");
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

        private async Task<MonAnEntity> GetMonAnFromApi(string MaMonAn)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://localhost:7267/api/mon-an/get-single/{MaMonAn}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(jsonData);
                    var monEntity = jObject["data"].ToObject<MonAnEntity>();
                    return monEntity;
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> SuaMonAn(string MaMonAn)
        {
            var monEntity = await GetMonAnFromApi(MaMonAn);

            if (monEntity == null)
            {
                return HttpNotFound();
            }
            var loaiMonEntity = await GetLoaiMonAnFromApi();

            if (loaiMonEntity == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.ListLoaiMonAn = loaiMonEntity;
            }
            return View(monEntity);
        }
        [HttpPost]
        public async Task<ActionResult> SuaMonAn(MonAnEntity model, HttpPostedFileBase fileAnh)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    MonAnEntity mon = new MonAnEntity();
                    if (fileAnh == null || fileAnh.ContentLength == 0)
                    {
                        mon = new MonAnEntity
                        {
                            MaMonAn = model.MaMonAn,
                            TenMonAn = model.TenMonAn,
                            DonGia = model.DonGia,
                            HinhAnh = model.HinhAnh,
                            DVT = model.DVT,
                            MaLoaiMonAn = model.MaLoaiMonAn,
                            TrangThai = model.TrangThai
                        };
                    }
                    else
                    {
                        mon = new MonAnEntity
                        {
                            MaMonAn = model.MaMonAn,
                            TenMonAn = model.TenMonAn,
                            DonGia = model.DonGia,
                            HinhAnh = fileAnh.FileName,
                            DVT = model.DVT,
                            MaLoaiMonAn = model.MaLoaiMonAn,
                            TrangThai = model.TrangThai
                        };
                        var baseUrlTuongDoi = "/Image/AnhMonAn/";
                        var baseUrlTuyetDoi = Server.MapPath(baseUrlTuongDoi);
                        fileAnh.SaveAs(baseUrlTuyetDoi + fileAnh.FileName);
                    }

                    string apiBaseUrl = $"https://localhost:7267/api/mon-an/update/{model.MaMonAn}";

                    var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(mon);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await httpClient.PutAsync(apiBaseUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("QuanLyMonAn", "QuanLy");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> XoaMonAn(string maMonAn)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl_Mon = $"https://localhost:7267/api/mon-an/delete/{maMonAn}";

                    var responseMon = await httpClient.DeleteAsync(apiBaseUrl_Mon);

                    if (responseMon.IsSuccessStatusCode)
                    {
                        return View("QuanLyDichVu", "QuanLy");
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

        public async Task<ActionResult> QuanLyNuocUong()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl_Nuoc = "https://localhost:7267/api/nuoc-uong/get-all";

                    var responseNuoc = await httpClient.GetAsync(apiBaseUrl_Nuoc);

                    if (responseNuoc.IsSuccessStatusCode)
                    {
                        var jsonDataNuoc = await responseNuoc.Content.ReadAsStringAsync();
                        var jObjectNuoc = JObject.Parse(jsonDataNuoc);

                        // Chuyển đổi dữ liệu JSON thành danh sách các đối tượng
                        var nuocList = jObjectNuoc["data"].ToObject<List<NuocEntity>>();

                        return View(nuocList);
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

        private async Task<List<LoaiNuocEntity>> GetLoaiNuocFromApi()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:7267/api/loai-nuoc/get-all");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(jsonData);
                    var loaiNuocEntity = jObject["data"].ToObject<List<LoaiNuocEntity>>();
                    return loaiNuocEntity;
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> ThemNuoc()
        {
            var loaiNuocEntity = await GetLoaiNuocFromApi();

            if (loaiNuocEntity == null)
            {
                return HttpNotFound();
            }
            return View(loaiNuocEntity);
        }
        [HttpPost]
        public async Task<ActionResult> ThemNuoc(NuocEntity model, HttpPostedFileBase fileAnh)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl = "https://localhost:7267/api/nuoc-uong/add";
                    NuocEntity nuoc = new NuocEntity();
                    if (fileAnh == null || fileAnh.ContentLength == 0)
                    {
                        nuoc = new NuocEntity
                        {
                            MaNuoc = "NUOC" + Guid.NewGuid().ToString("N"),
                            TenNuoc = model.TenNuoc,
                            DVT = model.DVT,
                            HinhAnh = "null",
                            DonGia = model.DonGia,
                            MaLoaiNuoc = model.MaLoaiNuoc,
                            TrangThai = "null"
                        };
                    }
                    else
                    {
                        nuoc = new NuocEntity
                        {
                            MaNuoc = "NUOC" + Guid.NewGuid().ToString("N"),
                            TenNuoc = model.TenNuoc,
                            DVT = model.DVT,
                            HinhAnh = "NuocUong/" + fileAnh.FileName,
                            DonGia = model.DonGia,
                            MaLoaiNuoc = model.MaLoaiNuoc,
                            TrangThai = "null"
                        };
                    }

                    string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(nuoc);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(apiBaseUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var baseUrlTuongDoi = "/Image/NuocUong/";
                        var baseUrlTuyetDoi = Server.MapPath(baseUrlTuongDoi);
                        fileAnh.SaveAs(baseUrlTuyetDoi + fileAnh.FileName);

                        return RedirectToAction("QuanLyNuocUong", "QuanLy");
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

        private async Task<NuocEntity> GetNuocFromApi(string MaNuoc)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://localhost:7267/api/nuoc-uong/get-single/{MaNuoc}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(jsonData);
                    var nuocEntity = jObject["data"].ToObject<NuocEntity>();
                    return nuocEntity;
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> SuaNuoc(string MaNuoc)
        {
            var nuocEntity = await GetNuocFromApi(MaNuoc);

            if (nuocEntity == null)
            {
                return HttpNotFound();
            }
            var loaiNuocEntity = await GetLoaiNuocFromApi();

            if (loaiNuocEntity == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.ListLoaiNuoc = loaiNuocEntity;
            }
            return View(nuocEntity);
        }
        [HttpPost]
        public async Task<ActionResult> SuaNuoc(NuocEntity model, HttpPostedFileBase fileAnh)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    NuocEntity nuoc = new NuocEntity();
                    if (fileAnh == null || fileAnh.ContentLength == 0)
                    {
                        nuoc = new NuocEntity
                        {
                            MaNuoc = model.MaNuoc,
                            TenNuoc = model.TenNuoc,
                            DVT = model.DVT,
                            HinhAnh = model.HinhAnh,
                            DonGia = model.DonGia,
                            MaLoaiNuoc = model.MaLoaiNuoc,
                            TrangThai = model.TrangThai
                        };
                    }
                    else
                    {
                        nuoc = new NuocEntity
                        {
                            MaNuoc = model.MaNuoc,
                            TenNuoc = model.TenNuoc,
                            DVT = model.DVT,
                            HinhAnh = "NuocUong/" + fileAnh.FileName,
                            DonGia = model.DonGia,
                            MaLoaiNuoc = model.MaLoaiNuoc,
                            TrangThai = model.TrangThai
                        };
                        var baseUrlTuongDoi = "/Image/NuocUong/";
                        var baseUrlTuyetDoi = Server.MapPath(baseUrlTuongDoi);
                        fileAnh.SaveAs(baseUrlTuyetDoi + fileAnh.FileName);
                    }

                    string apiBaseUrl = $"https://localhost:7267/api/nuoc-uong/update/{model.MaNuoc}";

                    var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(nuoc);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await httpClient.PutAsync(apiBaseUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("QuanLyNuocUong", "QuanLy");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> XoaNuocUong(string maNuoc)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl_Nuoc = $"https://localhost:7267/api/nuoc-uong/delete/{maNuoc}";

                    var responseNuoc = await httpClient.DeleteAsync(apiBaseUrl_Nuoc);

                    if (responseNuoc.IsSuccessStatusCode)
                    {
                        return View("QuanLyDichVu", "QuanLy");
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

        public async Task<ActionResult> QuanLySanh()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl_Sanh = "https://localhost:7267/api/sanh/get-all";

                    var responseSanh = await httpClient.GetAsync(apiBaseUrl_Sanh);

                    if (responseSanh.IsSuccessStatusCode)
                    {
                        var jsonDataSanh = await responseSanh.Content.ReadAsStringAsync();
                        var jObjectSanh = JObject.Parse(jsonDataSanh);

                        // Chuyển đổi dữ liệu JSON thành danh sách các đối tượng
                        var sanhList = jObjectSanh["data"].ToObject<List<SanhEntity>>();

                        return View(sanhList);
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

        [HttpPost]
        public async Task<ActionResult> XoaSanh(string maSanh)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl_Sanh = $"https://localhost:7267/api/sanh/delete/{maSanh}";

                    var responseSanh = await httpClient.DeleteAsync(apiBaseUrl_Sanh);

                    if (responseSanh.IsSuccessStatusCode)
                    {
                        return View("QuanLyDichVu", "QuanLy");
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

        [HttpGet]
        public ActionResult ThemSanh()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ThemSanh(SanhEntity model, HttpPostedFileBase fileAnh)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl = "https://localhost:7267/api/sanh/add";
                    SanhEntity s = new SanhEntity();
                    if (fileAnh == null || fileAnh.ContentLength == 0)
                    {
                        s = new SanhEntity
                        {
                            MaSanh = "SANH" + Guid.NewGuid().ToString("N"),
                            TenSanh = model.TenSanh,
                            HinhAnh = "null",
                            SucChuaToiThieu = model.SucChuaToiThieu,
                            SucChuaToiDa = model.SucChuaToiDa,
                            TrangThai = "null"
                        };
                    }
                    else
                    {
                        s = new SanhEntity
                        {
                            MaSanh = "SANH" + Guid.NewGuid().ToString("N"),
                            TenSanh = model.TenSanh,
                            HinhAnh = fileAnh.FileName,
                            SucChuaToiThieu = model.SucChuaToiThieu,
                            SucChuaToiDa = model.SucChuaToiDa,
                            TrangThai = "null"
                        };
                    }
                     
                    string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(s);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(apiBaseUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var baseUrlTuongDoi = "/Image/SanhTiec/";
                        var baseUrlTuyetDoi = Server.MapPath(baseUrlTuongDoi);
                        fileAnh.SaveAs(baseUrlTuyetDoi + fileAnh.FileName);

                        return RedirectToAction("QuanLySanh", "QuanLy");
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

        // Lấy Sảnh
        private async Task<SanhEntity> GetSanhFromApi(string MaSanh)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://localhost:7267/api/sanh/get-single/{MaSanh}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonDataSanh = await response.Content.ReadAsStringAsync();
                    var jObjectSanh = JObject.Parse(jsonDataSanh);
                    var sanhEntity = jObjectSanh["data"].ToObject<SanhEntity>();
                    return sanhEntity;
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> SuaSanh(string MaSanh)
        {
            var sanhEntity = await GetSanhFromApi(MaSanh);

            if (sanhEntity == null)
            {
                return HttpNotFound();
            }
            return View(sanhEntity);
        }
        [HttpPost]
        public async Task<ActionResult> SuaSanh(SanhEntity model, HttpPostedFileBase fileAnh)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    SanhEntity s = new SanhEntity();
                    if (fileAnh == null || fileAnh.ContentLength == 0)
                    {
                        s = new SanhEntity
                        {
                            MaSanh = model.MaSanh,
                            TenSanh = model.TenSanh,
                            HinhAnh = model.HinhAnh,
                            SucChuaToiThieu = model.SucChuaToiThieu,
                            SucChuaToiDa = model.SucChuaToiDa,
                            TrangThai = model.TrangThai
                        };
                    }
                    else
                    {
                        s = new SanhEntity
                        {
                            MaSanh = model.MaSanh,
                            TenSanh = model.TenSanh,
                            HinhAnh = fileAnh.FileName,
                            SucChuaToiThieu = model.SucChuaToiThieu,
                            SucChuaToiDa = model.SucChuaToiDa,
                            TrangThai = model.TrangThai
                        };
                        var baseUrlTuongDoi = "/Image/SanhTiec/";
                        var baseUrlTuyetDoi = Server.MapPath(baseUrlTuongDoi);
                        fileAnh.SaveAs(baseUrlTuyetDoi + fileAnh.FileName);
                    }

                    string apiBaseUrl = $"https://localhost:7267/api/sanh/update/{model.MaSanh}";

                    var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(s);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await httpClient.PutAsync(apiBaseUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("QuanLySanh", "QuanLy");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return View("Error");
            }
        }
    }
}