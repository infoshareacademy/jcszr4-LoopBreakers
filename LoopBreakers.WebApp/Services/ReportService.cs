﻿using LoopBreakers.ReportModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LoopBreakers.WebApp.DTOs;
using Newtonsoft.Json;

namespace LoopBreakers.WebApp.Services
{
    public class ReportService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string ApiUrl = "https://localhost:44308/api";

        public ReportService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TransferReportDTO> SendTransferReport(TransferReportDTO transfer)
        {
            return await SendResource<TransferReportDTO, TransferReportDTO>(transfer, $"{ApiUrl}/TransferReport");
        }

        public async Task<List<TransferReportDTO>> GetTransferReportByDate(SearchViewModel filter)
        {
            if (filter.DateFrom.HasValue && filter.DateTo.HasValue)
            {
                return await GetResource<List<TransferReportDTO>>($"{ApiUrl}/TransferReport?dateFrom={filter.DateFrom:dd-MM-yyyy}&dateTo={filter.DateTo:dd-MM-yyyy}");
            }
            return await GetResource<List<TransferReportDTO>>($"{ApiUrl}/TransferReport");
        }

        public async Task<ActivityReportDTO> SendActivityReport(ActivityReportDTO activity)
        {
            return await SendResource<ActivityReportDTO, ActivityReportDTO>(activity, $"{ApiUrl}/ActivityReport");
        }

        public async Task<List<ActivityReportDTO>> GetActivityReportByDate(SearchViewModel filter)
        {
            if (filter.DateFrom.HasValue && filter.DateTo.HasValue)
            {
                return await GetResource<List<ActivityReportDTO>>($"{ApiUrl}/ActivityReport?dateFrom={filter.DateFrom:dd-MM-yyyy}&dateTo={filter.DateTo:dd-MM-yyyy}");
            }
            return await GetResource<List<ActivityReportDTO>>($"{ApiUrl}/ActivityReport");
        }

        public async Task<List<CurrencyStatisticsDTO>> GetCurrencyStatistics(SearchViewModel filter)
        {
            if (filter.DateFrom.HasValue && filter.DateTo.HasValue)
            {
                return await GetResource<List<CurrencyStatisticsDTO>>($"{ApiUrl}/TransferReport/CurrencyStatistics?dateFrom={filter.DateFrom:dd-MM-yyyy}&dateTo={filter.DateTo:dd-MM-yyyy}");
            }
            return await GetResource<List<CurrencyStatisticsDTO>>($"{ApiUrl}/TransferReport/CurrencyStatistics");
        }
        public async Task<LoginStatisticsDTO> GetLoginStatistics(SearchViewModel filter)
        {
            if (filter.DateFrom.HasValue && filter.DateTo.HasValue)
            {
                return await GetResource<LoginStatisticsDTO>($"{ApiUrl}/ActivityReport/LoginStatistics?dateFrom={filter.DateFrom:dd-MM-yyyy}&dateTo={filter.DateTo:dd-MM-yyyy}");
            }
            return await GetResource<LoginStatisticsDTO>($"{ApiUrl}/ActivityReport/LoginStatistics");
        }
        public async Task<TransferStatsDTO> GetTransferStatistics(SearchViewModel filter)
        {
            if (filter.DateFrom.HasValue && filter.DateTo.HasValue)
            {
                return await GetResource<TransferStatsDTO>($"{ApiUrl}/ActivityReport/TransferStatistics?dateFrom={filter.DateFrom:dd-MM-yyyy}&dateTo={filter.DateTo:dd-MM-yyyy}");
            }
            return await GetResource<TransferStatsDTO>($"{ApiUrl}/ActivityReport/TransferStatistics");
        }
        public async Task<RegisterStatsDTO> GetRegisterStatistics(SearchViewModel filter)
        {
            if (filter.DateFrom.HasValue && filter.DateTo.HasValue)
            {
                return await GetResource<RegisterStatsDTO>($"{ApiUrl}/ActivityReport/RegisterStatistics?dateFrom={filter.DateFrom:dd-MM-yyyy}&dateTo={filter.DateTo:dd-MM-yyyy}");
            }
            return await GetResource<RegisterStatsDTO>($"{ApiUrl}/ActivityReport/RegisterStatistics");
        }
        public async Task<List<MostCommonHoursDTO>> GetMostCommonTransferHoursStatistics(SearchViewModel filter)
        {
            if (filter.DateFrom.HasValue && filter.DateTo.HasValue)
            {
                return await GetResource<List<MostCommonHoursDTO>>($"{ApiUrl}/ActivityReport/MostCommonTransferHours?dateFrom={filter.DateFrom:dd-MM-yyyy-hh}&dateTo={filter.DateTo:dd-MM-yyyy-hh}");
            }
            return await GetResource<List<MostCommonHoursDTO>>($"{ApiUrl}/ActivityReport/MostCommonTransferHours");
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
