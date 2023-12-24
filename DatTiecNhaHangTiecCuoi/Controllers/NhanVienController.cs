using DatTiecNhaHangTiecCuoi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace DatTiecNhaHangTiecCuoi.Controllers
{
    public class NhanVienController : Controller
    {
        // GET: NhanVien
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DangNhap(string Gmail, string MatKhau)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl = $"https://localhost:7267/api/nhan-vien/nv-login/{Gmail}-{MatKhau}";

                    var response = await httpClient.GetAsync(apiBaseUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Home", "QuanLy");
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
    }
}