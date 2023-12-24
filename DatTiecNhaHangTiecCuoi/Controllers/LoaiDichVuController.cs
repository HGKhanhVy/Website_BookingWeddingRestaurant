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
    public class LoaiDichVuController : Controller
    {
        public async Task<ActionResult> GetAll()
        {
            var httpClient = new HttpClient();
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


            List_LoaiDichVu_DichVu model = new List_LoaiDichVu_DichVu();
            List<string> madvtinh=new List<string>();
            List<LoaiDichVuEntity> list_loaidv=new List<LoaiDichVuEntity>();
            List<string> list_loai_dv = new List<string>();

            // lấy mã dich vụ từ dịch vụ tính phí
            foreach (var tinhphi in danhSachDVTinhPhi)
            {
                madvtinh.Add(tinhphi.MaDichVu);
            }

            // lấy mã loại dich vụ từ dịch vụ
            foreach (var maloaidv in danhSachDV)
            {
                foreach(var item in madvtinh)
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
                    if (item1==item.MaLoaiDichVu)
                    {
                        if(count==0)
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
        //public async Task<ActionResult> DV_GoiTrangTri()
        //{
        //    var httpClient = new HttpClient();
        //    var response_DV = await httpClient.GetAsync("https://localhost:7267/api/dich-vu/get-all");
        //    response_DV.EnsureSuccessStatusCode();

        //    var jsonString_DV = await response_DV.Content.ReadAsStringAsync();
        //    var jsonObject_DV = JObject.Parse(jsonString_DV);
        //    var danhSachDV = jsonObject_DV["data"].ToObject<List<DichVuEntity>>();

        //    List_LoaiDichVu_DichVu model = new List_LoaiDichVu_DichVu();
        //    model.list_dichvu = danhSachDV;
        //    return View(model);
        //}
        //public async Task<ActionResult> DV_Khac()
        //{
        //    var httpClient = new HttpClient();
        //    var response_DV = await httpClient.GetAsync("https://localhost:7267/api/dich-vu-tinh-phi/get-all");
        //    response_DV.EnsureSuccessStatusCode();

        //    var jsonString_DV = await response_DV.Content.ReadAsStringAsync();
        //    var jsonObject_DV = JObject.Parse(jsonString_DV);
        //    var danhSachDVTinhPhi = jsonObject_DV["data"].ToObject<List<DichVuTinhPhiEntity>>();

        //    List_LoaiDichVu_DichVu model = new List_LoaiDichVu_DichVu();
        //    model.list_dvtinhphi = danhSachDVTinhPhi; ;
        //    return View(model);
        //}
    }
}