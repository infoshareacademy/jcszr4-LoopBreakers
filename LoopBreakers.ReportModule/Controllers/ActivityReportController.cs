﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoopBreakers.DAL.Entities;
using LoopBreakers.ReportModule.Models;
using LoopBreakers.ReportModule.Services;


namespace LoopBreakers.ReportModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityReportController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReportService _reportService;

        public ActivityReportController(IMapper mapper, IReportService reportService)
        {
            _mapper = mapper;
            _reportService = reportService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportById(int id)
        {
            return Ok(await _reportService.GetActivityReportById(id));
        }

        [HttpGet()]
        public async Task<IActionResult> GetReport([FromQuery] string dateFrom, [FromQuery] string dateTo)
        {
            if (!DateTime.TryParse(dateFrom, out var apiDateFrom))
            {
                return Ok(await _reportService.GetAllActivityReports());
            };

            if (!DateTime.TryParse(dateTo, out var apiDateTo))
            {
                return Ok(await _reportService.GetAllActivityReports());
            };
            return Ok(await _reportService.GetActivityReportByDate(apiDateFrom, apiDateTo));
        }

        [HttpPost]
        public async Task<IActionResult> AddActivityReport([FromBody] ActivityReportDTO activity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var activityToCreate = _mapper.Map<ActivityReport>(activity);
            await _reportService.AddActivityReport(activityToCreate);
            return CreatedAtAction(nameof(GetReportById), new { id = activityToCreate.Id }, activityToCreate);
        }
        [HttpGet("LoginStatistics")]
        public async Task<IActionResult> GetLoginStatistics([FromQuery] string dateFrom,
         [FromQuery] string dateTo)
        {

            if (!DateTime.TryParse(dateFrom, out var apiDateFrom) || !DateTime.TryParse(dateTo, out var apiDateTo))
            {
                return Ok();
            };
            
            return Ok(await _reportService.GetLoginStatistics(apiDateFrom, apiDateTo));
        }
        [HttpGet("TransferStatistics")]
        public async Task<IActionResult> GetTransferStatistics([FromQuery] string dateFrom,
        [FromQuery] string dateTo)
        {

            if (!DateTime.TryParse(dateFrom, out var apiDateFrom) || !DateTime.TryParse(dateTo, out var apiDateTo))
            {
                return Ok();
            };

            return Ok(await _reportService.GetTransferStatistics(apiDateFrom, apiDateTo));
        }
        [HttpGet("RegisterStatistics")]
        public async Task<IActionResult> GetRegisterStatistics([FromQuery] string dateFrom,
      [FromQuery] string dateTo)
        {

            if (!DateTime.TryParse(dateFrom, out var apiDateFrom) || !DateTime.TryParse(dateTo, out var apiDateTo))
            {
                return Ok();
            };

            return Ok(await _reportService.GetRegisterStatistics(apiDateFrom, apiDateTo));
        }
        [HttpGet("MostCommonTransferHours")]
        public async Task<IActionResult> GetTransferStisticsByHours([FromQuery] string dateFrom,
       [FromQuery] string dateTo)
        {

            if (!DateTime.TryParse(dateFrom, out var apiDateFrom) || !DateTime.TryParse(dateTo, out var apiDateTo))
            {
                return Ok();
            };

            return Ok(await _reportService.GetTransferStisticsByHours(apiDateFrom, apiDateTo));
        }
    }
}
