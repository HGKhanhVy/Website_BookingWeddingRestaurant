using DatTiecNhaHangTiecCuoi.Models;
using Newtonsoft.Json;
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
    public class LichSanhController : Controller
    {
        public async Task<ActionResult> GetLichSanh()
        {
            //ngay = "12/01/2024";
            //ca = "17:00";
            //var ngay = Session["NgayToChuc"] as string;
            //var ca = Session["CaToChuc"] as string;
            try
            {
                using (var httpClient = new HttpClient())
                {
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
                                                             && l.NgayDienRa.Date == DateTime.Parse("24/08/2024").Date
                                                             && l.Ca == "17:30"))
                            .ToList();

                        // Hiển thị view với danh sách sảnh không có lịch
                        return View(sanhKhongCoLich);
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

        public async Task<ActionResult> GetAllSanh(DateTime? startDate)
        {
            var httpClient = new HttpClient();
            var response_LichSanh = await httpClient.GetAsync("https://localhost:7267/api/lich-sanh-tiec/get-all");
            response_LichSanh.EnsureSuccessStatusCode();

            var response_Sanh = await httpClient.GetAsync("https://localhost:7267/api/sanh/get-all");
            response_Sanh.EnsureSuccessStatusCode();


            var jsonString_Sanh = await response_Sanh.Content.ReadAsStringAsync();
            var jsonObject_Sanh = JObject.Parse(jsonString_Sanh);
            var danhSachSanh = jsonObject_Sanh["data"].ToObject<List<SanhEntity>>();

            var jsonString_LichSanh = await response_LichSanh.Content.ReadAsStringAsync();
            var jsonObject_LichSanh = JObject.Parse(jsonString_LichSanh);
            var danhSachLichSanh = jsonObject_LichSanh["data"].ToObject<List<LichSanhTiecEntity>>();


            if (startDate == null)
            {
                startDate = DateTime.Today;
            }
            DateTime firstDayOfWeek = GetFirstDayOfWeek(startDate.Value);
            DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);

            List<string> daysOfWeek = new List<string>();
            for (int i = 0; i < 7; i++)
            {
                DateTime currentDay = firstDayOfWeek.AddDays(i);
                daysOfWeek.Add(currentDay.ToString("dddd"));
            }

            ViewBag.StartDate = firstDayOfWeek.ToString("yyyy-MM-dd");
            ViewBag.EndDate = lastDayOfWeek.ToString("yyyy-MM-dd");
            ViewBag.DaysOfWeek = daysOfWeek;


            List_LichSanh_Sanh model = new List_LichSanh_Sanh();
            model.list_sanh = danhSachSanh;
            model.list_lichsanh = danhSachLichSanh;

            return View(model);
        }
        private DateTime GetFirstDayOfWeek(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        private DateTime GetLastDayOfWeek(DateTime date)
        {
            int diff = (7 - (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(diff).Date;
        }

    }
}