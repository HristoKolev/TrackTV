namespace TrackTv.WebServices.Infrastructure
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public class MishapService
    {
        private const string DefaultUrl = "https://mishap.hristo.tech";

        public MishapService(string apiKey)
        {
            if (!Guid.TryParse(apiKey, out Guid _))
            {
                throw new ArgumentException($"The ApiKey is invalid. ApiKey: '{apiKey ?? "null"}'");
            }

            this.ApiKey = apiKey;
            this.Url = DefaultUrl;
        }

        public MishapService(string apiKey, string url)
        {
            if (!Guid.TryParse(apiKey, out Guid _))
            {
                throw new ArgumentException($"The ApiKey is invalid. ApiKey: '{apiKey ?? "null"}'");
            }

            if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri _))
            {
                throw new ArgumentException($"The url is invalid. Url: '{url ?? "null"}'");
            }

            this.ApiKey = apiKey;
            this.Url = url;
        }

        private string ApiKey { get; }

        private string Url { get; }

        public async Task HandleErrorAsync(Exception exception)
        {
            var request = WebRequest.Create($"{this.Url}/records");
            request.Method = "POST";
            request.Headers["ApiKey"] = this.ApiKey;
            request.Headers["Content-Type"] = "application/json";

            using (var requestStream = await request.GetRequestStreamAsync().ConfigureAwait(false))
            using (var streamWriter = new StreamWriter(requestStream))
            {
                var record = new MishapRecord
                {
                    RecordType = "Error",
                    RecordTitle = exception.Message,
                    RecordDescription = exception.ToString()
                };

                string json = JsonConvert.SerializeObject(record);

                await streamWriter.WriteAsync(json).ConfigureAwait(false);
            }

            var response = await request.GetResponseAsync().ConfigureAwait(false);

            using (var responseStream = response.GetResponseStream())
            using (var streamReader = new StreamReader(responseStream))
            {
                string json = streamReader.ReadToEnd();
            }
        }

        private class MishapRecord
        {
            public string RecordContext { get; set; }

            public string RecordDescription { get; set; }

            public string RecordExtendedDescription { get; set; }

            public string RecordTitle { get; set; }

            public string RecordType { get; set; }
        }
    }
}