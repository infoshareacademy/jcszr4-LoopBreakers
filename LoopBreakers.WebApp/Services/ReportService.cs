using LoopBreakers.ReportModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace LoopBreakers.WebApp.Services
{
    public class ReportService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string _apiUrl = "https://localhost:6001/api";

        public ReportService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TransferReportDTO> SendTransferReport(TransferReportDTO transfer)
        {
            return await SendResource<TransferReportDTO, TransferReportDTO>(transfer, $"{_apiUrl}/TransferReport");
        }

        public async Task<List<TransferReportDTO>> GetTransferReportByDate(DateTime dateFrom, DateTime dateTo)
        {
            return await GetResource<List<TransferReportDTO>>($"{_apiUrl}/TransferReport?dateFrom={dateFrom:dd-MM-yyyyZ}&dateTo={dateTo:dd-MM-yyyyZ}");
        }


        private async Task<TReturn> SendResource<TReturn, TInput>(TInput resource, string url)
        {

            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");

            var result = await client.SendAsync(request);

            var content = await result.Content.ReadAsStringAsync();

            var deserializedResource = JsonConvert.DeserializeObject<TReturn>(content);

            return deserializedResource;
        }

        private async Task<TReturn> GetResource<TReturn>(string url)
        {
            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await client.SendAsync(request);

            var content = await result.Content.ReadAsStringAsync();

            var deserializedResource = JsonConvert.DeserializeObject<TReturn>(content);

            return deserializedResource;
        }
    }
}
