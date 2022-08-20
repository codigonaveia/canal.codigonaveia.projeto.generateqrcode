using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace canal.codigonaveia.projeto.generateqrcode.UI.Controllers
{
    public class GenerateQrCodeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateQrCode()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateQrCode(string produtoNome)
        {
            
            using(MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qrGeneraor = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGeneraor.CreateQrCode(produtoNome, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);

                using(Bitmap qrCodeBitmap = qrCode.GetGraphic(20)){

                    qrCodeBitmap.Save(ms, ImageFormat.Png);
                    ViewBag.QrCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                    ViewBag.Produto = produtoNome;
                }


            }
            return View();
        }
    }
}
