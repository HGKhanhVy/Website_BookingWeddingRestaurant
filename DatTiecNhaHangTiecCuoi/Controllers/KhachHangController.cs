using DatTiecNhaHangTiecCuoi.Models;
using Microsoft.Extensions.Logging;
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
using System.Web.UI.WebControls;

namespace DatTiecNhaHangTiecCuoi.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly ILogger<KhachHangController> _logger;
        private readonly HttpClient _client;
        private readonly string _baseURL = "https://localhost:7267/api/khach-hang/";
        public KhachHangController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_baseURL);
        }
        public KhachHangController(ILogger<KhachHangController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_baseURL);
        }

        // GET: KhachHang
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DangNhap(string SoDienThoai, string MatKhau)
        {
            try
            {
                // Gọi API đăng nhập
                HttpResponseMessage responseMessage = await _client.PostAsync($"kh-login/{SoDienThoai}-{MatKhau}", null);

                if (!responseMessage.IsSuccessStatusCode)
                {
                    // Xử lý khi API trả về lỗi
                    ViewBag.ErrorMessage = "Invalid phone number or password.";
                    return RedirectToAction("DangNhap", "KhachHang");
                }

                if (responseMessage.IsSuccessStatusCode)
                {
                    string responseData = await responseMessage.Content.ReadAsStringAsync();
                    dynamic apiResponse = JsonConvert.DeserializeObject(responseData);
                   
                    KhachHangEntity khachHang = JsonConvert.DeserializeObject<KhachHangEntity>(responseData);
                    TempData["KhachHang"] = khachHang;

                    return RedirectToAction("ThongTinKhachHang");
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                ViewBag.ErrorMessage = "An error occurred.";
                return View("Error");
            }

            return View();
        }

        public ActionResult ThongTinKhachHang()
        {
            return View();
        }

    }
}