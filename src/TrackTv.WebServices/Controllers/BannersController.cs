namespace TrackTv.WebServices.Controllers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using TrackTv.WebServices.Infrastructure;

    public class BannersController : Controller
    {
        [HttpGet("banners/{type}/{name}")]
        public async Task<IActionResult> Get(string type, string name)
        {
            string bannerPath = Path.Combine(Global.DataDirectory, "banners", type);

            string filePath = Path.Combine(bannerPath, name);

            if (System.IO.File.Exists(filePath))
            {
                return this.PhysicalFile(filePath, GetContentType(name));
            }

            if (!Directory.Exists(bannerPath))
            {
                Directory.CreateDirectory(bannerPath);
            }

            await DownloadFileAsync(type, name, filePath);

            return this.PhysicalFile(filePath, GetContentType(name));
        }

        private static async Task DownloadFileAsync(string type, string name, string filePath)
        {
            var request = WebRequest.Create($"https://thetvdb.com/banners/{type}/{name}");
            request.Method = "GET";
            var response = await request.GetResponseAsync();

            using (var responseStream = response.GetResponseStream())
            using (var fileStream = System.IO.File.OpenWrite(filePath))
            {
                await responseStream.CopyToAsync(fileStream);
            }
        }

        private static string GetContentType(string name)
        {
            string ext = Path.GetExtension(name);

            switch (ext)
            {
                case ".jpg" : return "image/jpeg";
                case ".jpeg" : return "image/jpeg";
                case ".png" : return "image/png";
                case ".gif" : return "image/gif";

                default : throw new InvalidOperationException($"File extension not supported ({ext})");
            }
        }
    }
}