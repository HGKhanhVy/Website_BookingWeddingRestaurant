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
    public class MonAn_MenuController : Controller
    {
        public async Task<ActionResult> GetMonAn_Menu()
        {
            var httpClient = new HttpClient();
            var response_Menu = await httpClient.GetAsync("https://localhost:7267/api/menu/get-all");
            var response_monTrongMenu = await httpClient.GetAsync("https://localhost:7267/api/mon-an-trong-menu/get-all");
            var response_MonAn = await httpClient.GetAsync("https://localhost:7267/api/mon-an/get-all");

            response_Menu.EnsureSuccessStatusCode();
            response_monTrongMenu.EnsureSuccessStatusCode();
            response_MonAn.EnsureSuccessStatusCode();


            var jsonString = await response_Menu.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(jsonString);
            var danhSachMenu = jsonObject["data"].ToObject<List<MenuEntity>>();

            var jsonString1 = await response_monTrongMenu.Content.ReadAsStringAsync();
            var jsonObject1 = JObject.Parse(jsonString1);
            var danhSachMenu1 = jsonObject1["data"].ToObject<List<MonAnTrongMenuEntity>>();

            var jsonString_MonAn = await response_MonAn.Content.ReadAsStringAsync();
            var jsonObject_MonAn = JObject.Parse(jsonString_MonAn);
            var danhSachMonAn = jsonObject_MonAn["data"].ToObject<List<MonAnEntity>>();


            MonAn_Menu model = new MonAn_Menu();
            model.dsMenu = danhSachMenu;
            model.dsMonTrongMenu = danhSachMenu1;
            model.dsMonAn = danhSachMonAn;
            return View(model);
        }
    }
}