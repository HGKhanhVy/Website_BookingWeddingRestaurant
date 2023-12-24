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
    public class List_Loai_MonAn_NuocController : Controller
    {

        public async Task<ActionResult> List_Loai_MonAn_Nuoc()
        {
            var httpClient = new HttpClient();
            var response_LoaiMonAn = await httpClient.GetAsync("https://localhost:7267/api/loai-mon-an/get-all");
            var response_MonAn = await httpClient.GetAsync("https://localhost:7267/api/mon-an/get-all");
            var response_Nuoc = await httpClient.GetAsync("https://localhost:7267/api/nuoc-uong/get-all");
            var response_LoaiNuoc = await httpClient.GetAsync("https://localhost:7267/api/loai-nuoc/get-all");
            response_LoaiMonAn.EnsureSuccessStatusCode();
            response_LoaiNuoc.EnsureSuccessStatusCode();
            response_MonAn.EnsureSuccessStatusCode();
            response_Nuoc.EnsureSuccessStatusCode();

            var jsonString = await response_LoaiMonAn.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(jsonString);
            var danhSachLoaiMonAn = jsonObject["data"].ToObject<List<LoaiMonAnEntity>>();

            var jsonString_LoaiNuoc = await response_LoaiNuoc.Content.ReadAsStringAsync();
            var jsonObject_LoaiNuoc = JObject.Parse(jsonString_LoaiNuoc);
            var danhSachLoaiNuoc = jsonObject_LoaiNuoc["data"].ToObject<List<LoaiNuocEntity>>();

            var jsonString_Nuoc = await response_Nuoc.Content.ReadAsStringAsync();
            var jsonObject_Nuoc = JObject.Parse(jsonString_Nuoc);
            var danhSachNuoc = jsonObject_Nuoc["data"].ToObject<List<NuocEntity>>();

            var jsonString_MonAn = await response_MonAn.Content.ReadAsStringAsync();
            var jsonObject_MonAn = JObject.Parse(jsonString_MonAn);
            var danhSachMonAn = jsonObject_MonAn["data"].ToObject<List<MonAnEntity>>();


            List_Loai_MonAn_Nuoc model = new List_Loai_MonAn_Nuoc();

            model.list_loaimonan = danhSachLoaiMonAn;
            model.list_loainuoc = danhSachLoaiNuoc;
            model.list_monan=danhSachMonAn;
            model.list_nuoc = danhSachNuoc;

            return View(model);
        }
        public async Task<ActionResult> GetAllMonAnById(string maLoaiMonAn)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7267/api/mon-an/get-all");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(jsonString);
            var danhSachMonAn = jsonObject["data"].ToObject<List<MonAnEntity>>();
            List_Loai_MonAn_Nuoc model = new List_Loai_MonAn_Nuoc();
            List<MonAnEntity> lst_tam= new List<MonAnEntity>();
            foreach (var item in danhSachMonAn)
            {
                if (item.MaLoaiMonAn == maLoaiMonAn)
                {
                    lst_tam.Add(item);
                }                
            }
            model.list_monan = lst_tam;
            return View(model);
        }
        public async Task<ActionResult> GetAllNuocById(string maLoaiNuoc)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7267/api/nuoc-uong/get-all");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(jsonString);
            var danhSachNuoc = jsonObject["data"].ToObject<List<NuocEntity>>();
            List_Loai_MonAn_Nuoc model = new List_Loai_MonAn_Nuoc();
            List<NuocEntity> lst_tam = new List<NuocEntity>();
            foreach (var item in danhSachNuoc)
            {
                if (item.MaLoaiNuoc == maLoaiNuoc)
                {
                    lst_tam.Add(item);
                }
            }
            model.list_nuoc = lst_tam;
            return View(model);
        }


        public ActionResult Index()
        {
            return View();
        }
    }
}