using LoopBreakers.ReportModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LoopBreakers.WebApp.Services
{
    public class TransferReportService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TransferReportService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task SendReport(TransferReportDTO transfer)
        {
            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:6001/api/TransferReport");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = new StringContent(JsonConvert.SerializeObject(transfer), Encoding.UTF8, "application/json");

            var result = await client.SendAsync(request);

            var content = await result.Content.ReadAsStringAsync();

            var deserializedResource = JsonConvert.DeserializeObject<TransferReportDTO>(content);
        }

        //public async Task<>
    }
}
