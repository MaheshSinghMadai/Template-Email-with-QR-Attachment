﻿using Microsoft.AspNetCore.Mvc;
using QRCodeGenerator.Models;
using System.Diagnostics;
using System.Drawing;
using ZXing;
using ZXing.QrCode;

namespace QRCodeGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormCollection formCollection)
        {
            var writer = new QRCodeWriter();
            var resultBit = writer.encode(formCollection["QRCodeString"], BarcodeFormat.QR_CODE, 200,200);
            var matrix = resultBit;
            int scale = 2;
            Bitmap result = new Bitmap(matrix.Width * scale , matrix.Height * scale);
            for(int x = 0; x < matrix.Height; x++)
            {
                for(int y= 0; y< matrix.Width; y++)
                {
                    Color pixel = matrix[x,y] ? Color.Black : Color.White;
                    for(int i = 0; i < scale; i++)
                    {
                        for(int j = 0; j < scale; j++)
                        {
                            result.SetPixel(x*scale + i, y*scale + j, pixel);   
                        }
                    }
                }
            }

            string webRootPath = _hostEnvironment.WebRootPath;
            result.Save(webRootPath + "\\Images\\QrCodeNew.png");
            ViewBag.URL = "\\Images\\QrCodeNew.png";
            return View();
        }

        public IActionResult ReadQRCode()
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            var path = webRootPath + "\\Images\\QrCodeNew.png";
            var reader = new BarcodeReaderGeneric();
            Bitmap image = (Bitmap)Image.FromFile(path);
            using (image)
            {
                LuminanceSource source;
                source = new ZXing.Windows.Compatibility.BitmapLuminanceSource(image);
                Result result = reader.Decode(source);
                ViewBag.Text = result.Text;
            }

            return View("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
