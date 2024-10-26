using System.Drawing;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace SelfServiceSupportPortal.Controllers
{
    public class QRCodeController : Controller
    {
        private readonly QRCodeDecoder qrCodeDecoder;

        public QRCodeController()
        {
            qrCodeDecoder = new QRCodeDecoder("AIzaSyDgsgt9SAckSnc7kzj2bB7M3CbHQx6cE3E");
        }
        public IActionResult ScanQRCode()
        {
            return View();
        }



        //[HttpPost]
        //public async Task<IActionResult> ScanQRCode(IFormFile file)
        //{
        //    try
        //    {
        //        if (file != null)
        //        {
        //            string imagePath = Path.GetTempFileName();

        //            using (var stream = new FileStream(imagePath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }

        //            string qrCodeData = await qrCodeDecoder.DecodeQRCodeAsync(imagePath);
        //            if (!string.IsNullOrEmpty(qrCodeData))
        //            {
        //                return Ok(qrCodeData);
        //            }
        //            else
        //            {
        //                return NotFound("QR code not found or couldn't be decoded.");
        //            }
        //            // You can then pass `decodedText` to your view or further process it as needed.
        //        }
        //        else
        //        {
        //            return BadRequest("No file was provided for QR code decoding.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}
      
        [HttpPost]
        public IActionResult ScanQRCode(/*IFormFile qrCodeImage */string qrCodeImage)
        {
            if (!string.IsNullOrEmpty(qrCodeImage))
            {
                try
                {
                    string base64Data = qrCodeImage.Replace("data:image/jpeg;base64,", ""); // Remove the data URI header

                    // Convert the base64 data to bytes
                    byte[] imageData = Convert.FromBase64String(base64Data);
                    //  byte[] imageData = Convert.FromBase64String(qrCodeImage);

                    using (var stream = new MemoryStream(imageData))
                    {
                        var bitmap = (Bitmap)Bitmap.FromStream(stream);

                        var reader = new ZXing.Windows.Compatibility.BarcodeReader();
                        var result = reader.Decode(bitmap);

                        if (result != null)
                        {
                            string decodedText = result.Text;
                            // Process the decoded QR code text as needed.
                            ViewBag.QrCodeResult = decodedText;
                            return Ok(result);
                        }
                        else
                        {
                            ViewBag.QrCodeResult = "QR code could not be decoded.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.QrCodeResult = "Error decoding QR code: " + ex.Message;
                }
            }
            return View();
            //if (qrCodeImage != null && qrCodeImage.Length > 0)
            //{
            //    try
            //    {
            //        using (var stream = new MemoryStream())
            //        {
            //            qrCodeImage.CopyTo(stream);
            //            var bitmap = (Bitmap)Bitmap.FromStream(stream);

            //            var reader = new ZXing.Windows.Compatibility.BarcodeReader();
            //            var result = reader.Decode(bitmap);

            //            if (result != null)
            //            {
            //                string decodedText = result.Text;
            //                // Process the decoded QR code text as needed.
            //                ViewBag.QrCodeResult = decodedText;
            //            }
            //            else
            //            {
            //                ViewBag.QrCodeResult = "QR code could not be decoded.";
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        ViewBag.QrCodeResult = "Error decoding QR code: " + ex.Message;
            //    }
            //}
            //return View();
        }


    }

    public class QRCodeDecoder
    {
        private readonly string apiKey;
        private readonly string apiUrl = "https://vision.googleapis.com/v1/images:annotate?key=";

        public QRCodeDecoder(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<string> DecodeQRCodeAsync(string imageUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                var requestData = new
                {
                    requests = new[]
                    {
                    new
                    {
                        image = new
                        {
                            source = new
                            {
                                imageUri = imageUrl
                            }
                        },
                        features = new[]
                        {
                            new
                            {
                                type = "TEXT_DETECTION"
                            }
                        }
                    }
                }
                };

                var jsonRequest = JsonSerializer.Serialize(requestData);
                var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl + apiKey, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<GoogleVisionResponse>(responseContent);
                    if (result.responses.Length > 0)
                    {
                        return result.responses[0].fullTextAnnotation.text;
                    }
                }

                return null;
            }
        }
    }

    public class GoogleVisionResponse
    {
        public AnnotatedResponse[] responses { get; set; }
    }

    public class AnnotatedResponse
    {
        public TextAnnotation fullTextAnnotation { get; set; }
    }

    public class TextAnnotation
    {
        public string text { get; set; }
    }


}
