namespace TrackTv.WebServices.Controllers.Public
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class BannersController : Controller
    {
        public BannersController(IHostingEnvironment hostingEnvironment)
        {
            this.HostingEnvironment = hostingEnvironment;
        }

        private IHostingEnvironment HostingEnvironment { get; }

        [HttpGet("banners/{type}/{name}")]
        public async Task<IActionResult> Get(string type, string name)
        {
            string bannerPath = Path.Combine(this.HostingEnvironment.ContentRootPath, "wwwroot", "banners", type);

            string filePath = Path.Combine(bannerPath, name);

            if (System.IO.File.Exists(filePath))
            {
                return this.PhysicalFile(filePath, GetContentType(name));
            }

            if (!Directory.Exists(bannerPath))
            {
                Directory.CreateDirectory(bannerPath);
            }

            await DownloadFileAsync(type, name, filePath).ConfigureAwait(false);

            return this.PhysicalFile(filePath, GetContentType(name));
        }

        private static async Task DownloadFileAsync(string type, string name, string filePath)
        {
            var request = WebRequest.Create($"https://thetvdb.com/banners/{type}/{name}");
            request.Method = "GET";
            var response = await request.GetResponseAsync().ConfigureAwait(false);

            using (Stream responseStream = response.GetResponseStream(), fileStream = System.IO.File.OpenWrite(filePath))
            {
                await responseStream.CopyToAsync(fileStream).ConfigureAwait(false);
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