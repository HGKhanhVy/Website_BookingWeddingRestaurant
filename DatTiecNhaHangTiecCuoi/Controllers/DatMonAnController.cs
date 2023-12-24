using DatTiecNhaHangTiecCuoi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DatTiecNhaHangTiecCuoi.Controllers
{
    public class DatMonAnController : Controller
    {
        public async Task<ActionResult> GetDatMon()
        {
            var httpClient = new HttpClient();
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


            List_Loai_MonAn_Nuoc model = new List_Loai_MonAn_Nuoc();

            model.list_monan = danhSachMonAn;
            model.list_nuoc = danhSachNuoc;
            model.list_loaimonan = danhSachLoaiMon;
            model.list_loainuoc = danhSachLoaiNuoc;


            return View(model);
        }
        public async Task<ActionResult> GetDatNuoc()
        {
            var httpClient = new HttpClient();
            var response_Nuoc = await httpClient.GetAsync("https://localhost:7267/api/nuoc-uong/get-all");
            response_Nuoc.EnsureSuccessStatusCode();

            var jsonString_Nuoc = await response_Nuoc.Content.ReadAsStringAsync();
            var jsonObject_Nuoc = JObject.Parse(jsonString_Nuoc);
            var danhSachNuoc = jsonObject_Nuoc["data"].ToObject<List<NuocEntity>>();

            List_Loai_MonAn_Nuoc model = new List_Loai_MonAn_Nuoc();
            model.list_nuoc = danhSachNuoc;

            return View(model);
        }
    }
}