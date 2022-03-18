using LoopBreakers.ReportModule.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LoopBreakers.WebApp.DTOs;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;

using LoopBreakers.WebApp.Helpers;

namespace LoopBreakers.WebApp.Services
{
    public class ReportService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string ApiUrl = "https://localhost:6001/api";

        public ReportService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TransferReportDTO> SendTransferReport(TransferReportDTO transfer)
        {
            return await SendResource<TransferReportDTO, TransferReportDTO>(transfer, $"{ApiUrl}/TransferReport");
        }

        public async Task<List<TransferReportDTO>> GetTransferReport(SearchViewModel filter)
        {
            return await GetResource<List<TransferReportDTO>>($"{ApiUrl}/TransferReport?dateFrom={filter.DateFrom:dd-MM-yyyy}&dateTo={filter.DateTo:dd-MM-yyyy}");
        }

        public async Task<ActivityReportDTO> SendActivityReport(ActivityReportDTO activity)
        {
            return await SendResource<ActivityReportDTO, ActivityReportDTO>(activity, $"{ApiUrl}/ActivityReport");
        }

        public async Task<List<ActivityReportDTO>> GetActivityReport(SearchViewModel filter)
        {
            return await GetResource<List<ActivityReportDTO>>($"{ApiUrl}/ActivityReport?dateFrom={filter.DateFrom:dd-MM-yyyy}&dateTo={filter.DateTo:dd-MM-yyyy}");
        }

        public async Task<List<CurrencyStatisticsDTO>> GetCurrencyStatistics(SearchViewModel filter)
        {
            return await GetResource<List<CurrencyStatisticsDTO>>($"{ApiUrl}/TransferReport/CurrencyStatistics?dateFrom={filter.DateFrom:dd-MM-yyyy}&dateTo={filter.DateTo:dd-MM-yyyy}");
        }
        public async Task<LoginStatisticsDTO> GetLoginStatistics(SearchViewModel filter)
        {
            return await GetResource<LoginStatisticsDTO>($"{ApiUrl}/ActivityReport/LoginStatistics?dateFrom={filter.DateFrom:dd-MM-yyyy}&dateTo={filter.DateTo:dd-MM-yyyy}");
        }
        public async Task<TransferStatsDTO> GetTransferStatistics(SearchViewModel filter)
        {
            return await GetResource<TransferStatsDTO>($"{ApiUrl}/ActivityReport/TransferStatistics?dateFrom={filter.DateFrom:dd-MM-yyyy}&dateTo={filter.DateTo:dd-MM-yyyy}");
        }
        public async Task<RegisterStatsDTO> GetRegisterStatistics(SearchViewModel filter)
        {
            return await GetResource<RegisterStatsDTO>($"{ApiUrl}/ActivityReport/RegisterStatistics?dateFrom={filter.DateFrom:dd-MM-yyyy}&dateTo={filter.DateTo:dd-MM-yyyy}");
        }
        public async Task<List<MostCommonHoursDTO>> GetMostCommonTransferHoursStatistics(SearchViewModel filter)
        {
            return await GetResource<List<MostCommonHoursDTO>>($"{ApiUrl}/ActivityReport/MostCommonTransferHours?dateFrom={filter.DateFrom:dd-MM-yyyy-hh}&dateTo={filter.DateTo:dd-MM-yyyy-hh}");
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
        public void SendEmail(SearchViewModel filter, ReportViewDTO report)
        {
            var fromAddress = new MailAddress("rafalszczerbaalarm@gmail.com", "Daily report");
            var toAddress = new MailAddress(filter.EmailAddress, "To Name");
            const string fromPassword = "Angelika29!";
            const string subject = "Daily Report LoopBreakers APP";
            string contentLoginActivity="";
            string contentRegisterActivity = "";
            string contentTransferActivity = "";
            if (filter.LoginActivity)
            {
                 contentLoginActivity = $"{report?.LoginCounter?.Name} : {report?.LoginCounter?.Count}";
            }
            if (filter.RegisterActivity)
            {
                contentRegisterActivity = $"{report?.RegisterCounter?.Name} : {report?.RegisterCounter?.Count}";
            }
            if (filter.TransferActivity)
            {
                contentTransferActivity = $"{report?.TransferCounter?.Name} : {report?.TransferCounter?.Count}";
            }
            string body = $"{contentLoginActivity} \r\n {contentRegisterActivity} \r\n {contentTransferActivity} ";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }           
        }

        public async Task CallMethodHelperForEmailSending(SearchViewModel filter)
        {
            ReportViewDTO reportModel = new ReportViewDTO();
            filter.DateFrom = DateTime.Now.AddHours(-DateTime.Now.Hour);
            filter.DateTo = DateTime.Now.AddDays(1);
            filter.LoginActivity = BackgroundJobsHelper.LoginActivity;
            filter.RegisterActivity = BackgroundJobsHelper.RegisterActivity;
            filter.TransferActivity = BackgroundJobsHelper.TransferActivity;
            filter.EmailAddress = BackgroundJobsHelper.EmailAddress;
            reportModel.Transfer = await GetTransferReport(filter);
            reportModel.Activity = await GetActivityReport(filter);
            reportModel.Currency = await GetCurrencyStatistics(filter);
            reportModel.LoginCounter = await GetLoginStatistics(filter);
            reportModel.TransferCounter = await GetTransferStatistics(filter);
            reportModel.RegisterCounter = await GetRegisterStatistics(filter);
            reportModel.MostCommonTransferHours = await GetMostCommonTransferHoursStatistics(filter);
            SendEmail(filter, reportModel);

        }
    }
}
